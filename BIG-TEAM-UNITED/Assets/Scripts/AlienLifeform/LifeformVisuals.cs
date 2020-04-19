using deVoid.Utils;
using UnityEngine;
using System.Collections;
using DG.Tweening;
// A class for managing purely the visuals of the lifeform.
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

    public GameObject[] StageObjects;

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
        int stageIndex = Mathf.Clamp(stage - 1, 0, StageObjects.Length - 1);
        if (stageIndex < 0)
        {
            return;
        }
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

    public IEnumerator PlayEvolutionUpEffects()
    {
        // Play sound here.
        //
        Debug.Log("Starting evolution up effects");
        
        float ShakeDuration = this.transform.DOShakePosition(EvolutionUpShakeDuration, 0.4f, 30, 90, false, false).Duration();

        float ParticleStartPercentage = 0.5f;

        yield return new WaitForSeconds(ShakeDuration * ParticleStartPercentage);
        if (EvolutionUpCompleteParticle != null)
        {
            Instantiate(EvolutionUpCompleteParticle, this.transform);
        }
        yield return new WaitForSeconds(ShakeDuration * (1 - ParticleStartPercentage));

    }
}