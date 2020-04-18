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
        owner.SummonTank();
    }

    public void Execute()
    {
        Debug.Log("updating State_Tank_Ready state");
    }

    public void Exit()
    {
        Debug.Log("exiting State_Tank_Ready state");
    }
}