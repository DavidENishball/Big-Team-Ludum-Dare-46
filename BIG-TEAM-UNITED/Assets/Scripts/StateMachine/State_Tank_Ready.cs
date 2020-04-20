using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State_Tank_Ready : IState
{
    LifeformTank owner;

    public State_Tank_Ready(LifeformTank owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering State_Tank_Ready");
        LifeformManager.Instance.IsLifeFormDestroyed = false;
        LifeformManager.Instance.stateMachine.ChangeState(new State_LifeForm_Growing(LifeformManager.Instance));
        Signals.Get<ReadyTankSignal>().Dispatch();
    }

    public void Execute()
    {
        Debug.Log("updating State_Tank_Ready state");
    }

    public void Exit()
    {
        Debug.Log("exiting State_Tank_Ready state");
    }

    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {
        if (Verb == LifeformManager.EControlVerbs.DISMISS_TANK && !owner.workingLock)
        {
            owner.stateMachine.ChangeState(new State_Tank_Dismissed(owner));
        }
        else if (Verb == LifeformManager.EControlVerbs.SELF_DESTRUCT_TANK)
        {
            if (LifeformManager.Instance.IsLifeFormDestroyed == false)
            {
                LifeformManager.Instance.stateMachine.ChangeState(new State_LifeForm_Destroyed(LifeformManager.Instance));
                return true;
            }
        }
        return false;
    }
}