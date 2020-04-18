using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State_LifeForm_Failed : IState
{
    LifeformManager owner;

    public State_LifeForm_Failed(LifeformManager owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering State_LifeForm_Failed");
        owner.IsLifeFormDestroyed = false;
        // TODO: play end animation.
    }

    public void Execute()
    {
    }

    public void Exit()
    {

    }

    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {
        return false;
    }
}