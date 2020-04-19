using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
using DG.Tweening;
public class PuzzleManager_Tuning : PuzzleManager_Base
{
    // Generates a 3D space.

        // Adds a target point in the 3D space
        // Generates a start point.
        // Each dial

    //

    public int PressesRequired = 2;

    public int PressesReceived = 0;

    public Material VictoryMaterial;
    public Material ReadyMaterial;
    public GameObject SuccessParticle;

    public GameObject PanelBase;

    public Knob[] KnobArray = new Knob[3];

    public override void PuzzleComplete()
    {
        base.PuzzleComplete();
        PanelBase.GetComponent<MeshRenderer>().material = VictoryMaterial; // Example of how to change visuals in response to a win.
    }

    void Start()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
        Signals.Get<NewStageStartingSignal>().AddListener(ResetWithSource);
    }

    public void ResetWithSource(State_LifeForm_Growing Source)
    {
        Reset();
    }


    public override void Reset()
    {
        base.Reset();
        IsCompleted = false;
        PressesReceived = 0;
        PanelBase.GetComponent<MeshRenderer>().material = ReadyMaterial;
    }

    public void ReceivedVerb(Component source, LifeformManager.EControlVerbs Verb, int data)
    {
        if (source.gameObject.transform.IsChildOf(this.transform))
        {
            this.transform.DOShakePosition(0.2f, 0.01f, 30);
            if (!IsCompleted)
            {
                PressesReceived++;

                Instantiate(SuccessParticle, source.transform);
                if (PressesReceived >= PressesRequired)
                {
                    PuzzleComplete();
                }
            }
            // It's my child.
        }
        
        // ignore if not my child.
    }
}
