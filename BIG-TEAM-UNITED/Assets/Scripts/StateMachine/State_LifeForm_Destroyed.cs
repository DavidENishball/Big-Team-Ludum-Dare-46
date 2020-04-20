using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State_LifeForm_Destroyed : IState
{
    LifeformManager owner;



    public State_LifeForm_Destroyed(LifeformManager owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering State_LifeForm_Destroyed");
        owner.TankReference.Lifeform.SetActive(false);

        // Clear all puzzles.
        LifeformManager.Instance.ClearAllPuzzles();

        owner.IsLifeFormDestroyed = true;
        GameObject.Instantiate(LifeformManager.Instance.SelfDestructParticle , owner.TankReference.Lifeform.transform.position, Random.rotation );

        owner.StartCoroutine(CoroutineDismissAfterABit());

    }

    public void Execute()
    {
        Debug.Log("updating State_LifeForm_Destroyed state");
    }

    public void Exit()
    {
        Debug.Log("exiting State_LifeForm_Destroyed state");
    }

    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {
        return false;
    }

    public IEnumerator CoroutineDismissAfterABit()
    {
        float WaitTime = 1.0f;

        yield return (new WaitForSeconds(WaitTime));

        if (owner.stateMachine.GetState() == this) // Do nothing if the state has changed already.
        {
            // What a fudging mess this is.
            State_Tank_Dismissed CurrentTankState = owner.TankReference.stateMachine.GetState() as State_Tank_Dismissed; // the "As" operator acts like a dynamic cast.

            if (CurrentTankState == null) // If not already dismissed.
            {
                owner.TankReference.stateMachine.ChangeState(new State_Tank_Dismissed(owner.TankReference));
            }
        }
    }
}