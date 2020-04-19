using deVoid.Utils;
using UnityEngine;

// A class for managing purely the visuals of the lifeform.
public class LifeformVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    public float BaseStageScale = 0.03F;

    public int deathStageNumber = 0;

    public GameObject[] StageObjects;

    public void Reset()
    {
        transform.localScale = Vector3.one;
        DeactivateAll();
    }

    private void Awake()
    {
        Signals.Get<NewStageStartingSignal>().AddListener(StageStarting);
        DeactivateAll();
    }

    public void StageStarting(State_LifeForm_Growing NewStage)
    {
        DeactivateAll();
        if (NewStage.StageNumber != deathStageNumber)
            SetObjectIfPossible(NewStage.StageNumber);
    }

    private void SetObjectIfPossible(int stage)
    {
        int stageIndex = Mathf.Clamp(stage - 1, 0, StageObjects.Length - 1);
        StageObjects[stageIndex].SetActive(true);
        StageObjects[stageIndex].transform.localScale = Vector3.one * BaseStageScale * stage;
    }

    private void DeactivateAll()
    {
        foreach (var go in StageObjects)
        {
            go.SetActive(false);
        }
    }
}