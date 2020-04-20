using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State_Tank_Dismissed : IState
{
    LifeformTank owner;

    public State_Tank_Dismissed(LifeformTank owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering State_Tank_Dismissed");
        Signals.Get<DismissTankSignal>().Dispatch();
        owner.DismissTank();
    }

    public void Execute()
    {
        Debug.Log("updating State_Tank_Dismissed state");
    }

    public void Exit()
    {
        Debug.Log("exiting State_Tank_Dismissed state");
    }

    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {
        if (Verb == LifeformManager.EControlVerbs.READY_TANK)
        {
            owner.stateMachine.ChangeState(new State_Tank_Ready(owner));
            return true;
        }
        return false;
    }
}