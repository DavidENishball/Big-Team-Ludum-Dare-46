using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
using DG.Tweening;
using UnityEngine.UI;
public class PuzzleManager_PickDifferent : PuzzleManager_Base
{

    // Generate a grid of buttons.
    // Pick all that are different.

    public int PressesRequired = 2;

    public int PressesReceived = 0;

    public Material VictoryMaterial;
    public Material ReadyMaterial;
    public Material ButtonOffMaterial;
    public GameObject SuccessParticle;
    public GameObject ErrorParticle;

    public GameObject PanelBase;

    public GameObject ButtonPrefab;

    private string allcharacters = "abcdefghijklmnopqrstuvwxyz";

    public BoxCollider SpawnArea;

    public List<GameObject> ButtonList = new List<GameObject>();

    public HashSet<GameObject> AlreadyPickedButtons = new HashSet<GameObject>();

    List<GameObject> MarkedButtons = new List<GameObject>();

    public bool GenerateButtonsAuto = false;

    public Vector2 PickSpawnDimensions()
    {
        return new Vector2(3, 3);
    }


    public char GetRandomChar()
    {
        return allcharacters[Random.Range(0, allcharacters.Length)];
    }


    public override void PuzzleComplete()
    {
        base.PuzzleComplete();
        PanelBase.GetComponent<MeshRenderer>().material = VictoryMaterial; // Example of how to change visuals in response to a win.
    }

    void Start()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);


        Vector2 GridToSpawn = PickSpawnDimensions();

        float xIncrement = SpawnArea.size.x / GridToSpawn.x;
        float yIncrement = SpawnArea.size.y / GridToSpawn.y;

        char regularChar = GetRandomChar();
        if (GenerateButtonsAuto)
        {
            foreach (GameObject OldButton in ButtonList)
            {
                Destroy(OldButton);
            }
            ButtonList.Clear();

            for (int i = 0; i < GridToSpawn.x; ++i)
            {
                for (int j = 0; j < GridToSpawn.y; ++j)
                {
                    Vector3 localSpawnVector = new Vector3(i * xIncrement, 0, j * yIncrement) + SpawnArea.transform.localPosition;



                    GameObject NewButton = Instantiate(ButtonPrefab);

                    ButtonList.Add(NewButton);
                    NewButton.transform.SetParent(SpawnArea.transform);
                    NewButton.transform.localPosition = localSpawnVector;
                    NewButton.transform.rotation = SpawnArea.transform.rotation;


                }
            }
        }
        foreach (GameObject IteratedButton in ButtonList)
        {
            // Mark them with alien script.
            Text FoundText = IteratedButton.GetComponentInChildren<Text>();
            if (FoundText)
            {
                FoundText.text = new string(regularChar, 1);
            }
        }

        // Randomize one of them.
        MarkedButtons.Clear();

        char uniqueChar = allcharacters[Random.Range(0, allcharacters.Length)];

        while (uniqueChar == regularChar)
        {
            // Dirty way to prevent duplicates.
            uniqueChar = allcharacters[Random.Range(0, allcharacters.Length)];
        }


        List<GameObject> ClonedButtonList = ButtonList;

        Shuffle(ClonedButtonList);

        for (int i = 0; i < PressesRequired; ++i)
        {
            GameObject MarkedObject = ButtonList[i];
            MarkedButtons.Add(MarkedObject);
            Text UniqueText = MarkedObject.GetComponentInChildren<Text>();
            if (UniqueText)
            {
                // Quick and dirty.
                UniqueText.text = new string(uniqueChar, 1);
            }
        }
    }

    public void ReceivedVerb(Component source, LifeformManager.EControlVerbs Verb, int data)
    {
        if (source != null && source.gameObject.transform.IsChildOf(this.transform))
        {

            if (!IsCompleted)
            {
                if (MarkedButtons.Contains(source.gameObject))
                {
                    if (AlreadyPickedButtons.Contains(source.gameObject) == false)
                    {
                        AlreadyPickedButtons.Add(source.gameObject);
                        PressesReceived++;
                        Instantiate(SuccessParticle, source.transform);
                        source.transform.DOShakePosition(0.2f, 0.01f, 30);
                        MeshRenderer FoundRenderer = source.gameObject.GetComponentInChildren<MeshRenderer>();
                        if (FoundRenderer) 
                        {
                            FoundRenderer.material = ButtonOffMaterial;
                        }

                        if (PressesReceived >= PressesRequired)
                        {
                            PuzzleComplete();
                        }
                    }
                   
                }
                else
                {
                    PuzzleError();
                    if (ErrorParticle != null)
                    {
                        Instantiate(ErrorParticle, source.transform);
                    }
                    source.transform.DOShakePosition(0.5f, 0.1f, 30);
                }


            }
            // It's my child.
        }

        // ignore if not my child.
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


    private void OnDestroy()
    {
        Signals.Get<PerformVerbSignal>().RemoveListener(ReceivedVerb);
    }

    // Ripped from the web.


}

