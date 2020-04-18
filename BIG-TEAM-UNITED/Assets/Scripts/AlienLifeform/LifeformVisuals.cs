using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;


// A class for managing purely the visuals of the lifeform.
public class LifeformVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    public float BaseStageScale = 0.03F;

    public void Reset()
    {
        transform.localScale = Vector3.one;
    }

    void Start()
    {
        Signals.Get<NewStageStartingSignal>().AddListener(StageStarting);
    }


    public void StageStarting(State_LifeForm_Growing NewStage)
    {
        transform.localScale = Vector3.one * BaseStageScale * NewStage.StageNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
