using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;

public class PuzzleManager_HiddenGoal : PuzzleManager_Base
{
    // Start is called before the first frame update
    void Start()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceivedVerb(Component source, LifeformManager.EControlVerbs Verb, int data)
    {
        if (source.gameObject.transform.IsChildOf(this.transform))
        {

        }
    }
}
