using deVoid.Utils;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
// A class for managing purely the visuals of the lifeform.

[System.Serializable]
public struct EvolutionVisualsStruct
{
    public int StagesForThisStep;
    public GameObject CreatureVisuals;
    public float ScalePerStage;
    public float BaseScale;

}

public class LifeformVisuals : MonoBehaviour
{
    private static LifeformVisuals privateInstance = null;
    public static LifeformVisuals Instance { get
        {
            return privateInstance;
        } }

    // Start is called before the first frame update
    public float BaseStageScale = 0.03F;

    public GameObject EvolutionUpCompleteParticle;

    public int deathStageNumber = 0;

    public float EvolutionUpShakeDuration = 1.0f;

    public List<EvolutionVisualsStruct> VisualsStructList = new List<EvolutionVisualsStruct>();

    public void Reset()
    {
        transform.localScale = Vector3.one;
        DeactivateAll();
    }

    private void Awake()
    {
        Signals.Get<NewStageStartingSignal>().AddListener(StageStarting);
        privateInstance = this;
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
        int EvolutionIndex = 0;
        EvolutionVisualsStruct ChosenVisualsStruct = new EvolutionVisualsStruct();
        int StagesToStartThisEvolutionStep = 0;
        int StagesToFinishThisEvolutionStep = 0;
        for (EvolutionIndex = 0; EvolutionIndex < VisualsStructList.Count; EvolutionIndex++)
        {
            ChosenVisualsStruct = VisualsStructList[EvolutionIndex];
            StagesToFinishThisEvolutionStep += ChosenVisualsStruct.StagesForThisStep;

            if (StagesToFinishThisEvolutionStep >= stage)
            {
                break;
            }
            else
            {
                StagesToStartThisEvolutionStep = StagesToFinishThisEvolutionStep;
            }
        }


        EvolutionIndex = Mathf.Min(EvolutionIndex, EvolutionIndex, VisualsStructList.Count - 1);

        int stagesSinceThisEvolutionStarted = (stage - 1) - StagesToStartThisEvolutionStep;

        // This figure is a countdown.  I need it to count up.
        
        if (EvolutionIndex < 0)
        {
            return;
        }

        if (ChosenVisualsStruct.CreatureVisuals != null)
        {
            DeactivateAll();
            ChosenVisualsStruct.CreatureVisuals.SetActive(true);
            ChosenVisualsStruct.CreatureVisuals.transform.localScale = Vector3.one * (ChosenVisualsStruct.BaseScale + ChosenVisualsStruct.ScalePerStage * (stagesSinceThisEvolutionStarted + 1));
        } 
    }

    private void DeactivateAll()
    {
        foreach (EvolutionVisualsStruct go in VisualsStructList)
        {
            if (go.CreatureVisuals != null)
            {
                go.CreatureVisuals.SetActive(false);
            }
        }
    }

    public IEnumerator PlayEvolutionUpEffects()
    {
        // Play sound here.
        //
        Debug.Log("Starting evolution up effects");
        
        float ShakeDuration = this.transform.DOShakePosition(EvolutionUpShakeDuration, 20f, 60, 90, false, false).Duration();

        float ParticleStartPercentage = 0.5f;

        yield return new WaitForSeconds(ShakeDuration * ParticleStartPercentage);
        if (EvolutionUpCompleteParticle != null)
        {
            Instantiate(EvolutionUpCompleteParticle, this.transform);
        }
        yield return new WaitForSeconds(ShakeDuration * (1 - ParticleStartPercentage));

    }
}