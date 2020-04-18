using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
using DG.Tweening;
public class PuzzleManager_Test_PushButton : PuzzleManager_Base
{
    // Override this class to make a new puzzle.  Put it on the root of a new prefab.

    // Start is called before the first frame update


    public int PressesRequired = 2;

    public int PressesReceived = 0;

    public Material VictoryMaterial;
    public GameObject SuccessParticle;

    public GameObject PanelBase;

    public override void PuzzleComplete()
    {
        base.PuzzleComplete();
        PanelBase.GetComponent<MeshRenderer>().material = VictoryMaterial; // Example of how to change visuals in response to a win.
    }

    void Start()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);

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
