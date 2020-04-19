using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State_LifeForm_Victory : IState
{
    LifeformManager owner;

    public State_LifeForm_Victory(LifeformManager owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering State_LifeForm_Victory");
        owner.IsLifeFormDestroyed = false;
        // TODO: play end animation.
    }

    public void Execute()
    {
        Debug.Log("updating State_LifeForm_Victory state");
    }

    public void Exit()
    {
        Debug.Log("exiting State_LifeForm_Victory state");
    }

    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {
        Debug.Log("updating State_LifeForm_Egg state with verb " + Verb.ToString());
        return false;
    }
}