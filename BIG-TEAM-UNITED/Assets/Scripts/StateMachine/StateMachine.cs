﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }


    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {
        if (currentState != null)
        {
            return currentState.HandleVerb(Source, Verb, Data);
        }
        return false;
    }

    public IState GetState()
    {
        return currentState;
    }
}