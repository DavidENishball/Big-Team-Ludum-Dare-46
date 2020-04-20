using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
// Class for managing the overall state of the lifeform and acts as a router between controlpanel signals and the state of the character.
public class LifeformManager : MonoBehaviour
{
    public GameObject SelfDestructParticle; // Putting it here because we can't bake it into the Destroyed state.

    protected static LifeformManager static_instance;
    public static LifeformManager Instance
    {
        get
        {
            return static_instance;
        }
    }
    public enum ETankState
    {
        READY,
        DISMISSED,
        DESTROYED
    }
    // Verb list that the panel can send to the simulation.
    public enum EControlVerbs
    {
        READY_TANK,
        DISMISS_TANK,
        SELF_DESTRUCT_TANK,
        PUZZLE_COMPLETE,
        PUZZLE_ERROR,
        PUZZLE_PROGRESS,
        NEW_STAGE_STARTING,
        GENERIC_INPUT
    }

    public bool IsLifeFormDestroyed = false;


    public StateMachine stateMachine = new StateMachine();


    public LifeformTank TankReference;

    public int PuzzlesCompleted = 0;


    public bool TankIsReady;
    public ETankState TankState = ETankState.DISMISSED;

    public float TimerStartValue = 120f;
    public float BonusTimePerStage = 0f;
    public float PenaltyPerError = 5f;
    public float DangerTimeKickinValue = 10f;
    public Timer timer;


    public List<PuzzleObjectSpawnPoint> ListPuzzleSpawnPoint = new List<PuzzleObjectSpawnPoint>();

    public List<PuzzleManager_Base> SpawnedPuzzles = new List<PuzzleManager_Base>();

    public List<GameObject> PossiblePuzzlePrefabs = new List<GameObject>();

    public int TotalPuzzlesSpawnedEver = 0;

    public int PuzzlesSpawnedThisStage = 0;

    private void Awake()
    {
        // Set up static instance.
        static_instance = this;
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
        Signals.Get<TimeOut>().AddListener(HandleTimeExpired);
        // Set up timer
        timer = new Timer(TimerStartValue, BonusTimePerStage, PenaltyPerError, DangerTimeKickinValue);
    }


    public void ReceivedVerb(Component source, EControlVerbs Verb, int data)
    {
        Debug.Log("LifeFormManager received verb " + Verb.ToString());

        stateMachine.HandleVerb(source, Verb, data);

        //if (Verb == EControlVerbs.READY_TANK)
        //{
        //    TankReference.SummonTank();
            
        //}
        //else if (Verb == EControlVerbs.DISMISS_TANK)
        //{
        //    TankReference.DismissTank();
        //}
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TankReference == null)
        {
            TankReference = FindObjectOfType<LifeformTank>();
        }

        if (ListPuzzleSpawnPoint.Count == 0)
        {
            // Search
            Debug.Log("No spawn points provided.  Searching for puzzle spawn points.");

            ListPuzzleSpawnPoint = new List<PuzzleObjectSpawnPoint>(FindObjectsOfType<PuzzleObjectSpawnPoint>());
        }

        Signals.Get<PuzzleComplete>().AddListener(timer.AddBonus);
        Signals.Get<PuzzleError>().AddListener(timer.SubtractPenalty);
        Signals.Get<DismissTankSignal>().AddListener(timer.DisableTimer);
        Signals.Get<ReadyTankSignal>().AddListener(timer.ResetAndEnable);
    }

    public void HandleTimeExpired()
    {
        if (!(stateMachine.GetState() is State_LifeForm_Destroyed))
        {
            stateMachine.ChangeState(new State_LifeForm_Destroyed(this));
        }
    }

    public void SpawnNewPuzzles(int StageNumber)
    {
        // To start, one puzzle per stage.

        int NumberOfPuzzlesRequired = GetNumberOfPuzzlesRequiredForStage(StageNumber);
        ClearAllPuzzles();

        for (int i = 0; i < NumberOfPuzzlesRequired && i < ListPuzzleSpawnPoint.Count; ++i)
        {
            PuzzleObjectSpawnPoint SpawnPoint = ListPuzzleSpawnPoint[(i + TotalPuzzlesSpawnedEver) % ListPuzzleSpawnPoint.Count];
            TotalPuzzlesSpawnedEver++; // Increment this count so the spawn points change.


            List<GameObject> ClonedVersion = PossiblePuzzlePrefabs;
            Shuffle(ClonedVersion);

            GameObject NewPuzzleObject = Instantiate(PossiblePuzzlePrefabs[i% PossiblePuzzlePrefabs.Count], SpawnPoint.transform.position, SpawnPoint.transform.rotation); // Randomize this later.
            
            PuzzleManager_Base Manager = NewPuzzleObject.GetComponent<PuzzleManager_Base>();

            if (Manager != null)
            {
                SpawnedPuzzles.Add(Manager);
            }
        }
    }

    public void ClearAllPuzzles()
    {
        foreach(PuzzleManager_Base Iter in SpawnedPuzzles)
        {
            if (Iter != null)
            {
                Destroy(Iter.gameObject);
                // Optionally put a particle here.
            }
        }
        SpawnedPuzzles.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick();
    }
    

    public int GetNumberOfPuzzlesRequiredForStage(int StageNumber)
    {
        // TODO: make this a table.
        float HalfOfStage = (float)(StageNumber) / 2.0f;

        
        return Mathf.Max(1, (int)Mathf.Ceil(HalfOfStage));
    }

    public int GetNumberOfFailuresAllowedForStage(int StageNumber)
    {
        // TODO: make this a table.
        return Mathf.Max(2, 5 - StageNumber);
    }

    // Ripped from the web.
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

}
