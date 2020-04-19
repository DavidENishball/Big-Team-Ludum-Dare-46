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
    public float BonusTimePerStage = 30f;
    public float PenaltyPerError = 5f;
    public float DangerTimeKickinValue = 10f;
    public Timer timer;


    private void Awake()
    {
        // Set up static instance.
        static_instance = this;
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);

        // Set up timer
        timer = new Timer(TimerStartValue, BonusTimePerStage, PenaltyPerError, DangerTimeKickinValue);
        Signals.Get<PuzzleComplete>().AddListener(timer.AddBonus);
        Signals.Get<PuzzleError>().AddListener(timer.SubtractPenalty);
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
        TankReference = FindObjectOfType<LifeformTank>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick();
        Debug.Log(timer.Remaining);
    }
    
    public void SetCreatureStateVisuals(int StageNumber)
    {
        // TODO
    }

    public int GetNumberOfPuzzlesRequiredForStage(int StageNumber)
    {
        // TODO: make this a table.
        return Mathf.Max(1, StageNumber / 2);
    }

    public int GetNumberOfFailuresAllowedForStage(int StageNumber)
    {
        // TODO: make this a table.
        return Mathf.Max(2, 5 - StageNumber);
    }
}
