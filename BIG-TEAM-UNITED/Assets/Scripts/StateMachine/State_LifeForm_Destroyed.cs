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
        Debug.Log("updating State_LifeForm_Destroyed state with verb " + Verb.ToString());
        return false;
    }
}