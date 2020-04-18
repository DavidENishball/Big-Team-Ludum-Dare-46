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
}