﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
// Class for managing the overall state of the lifeform and acts as a router between controlpanel signals and the state of the character.
public class LifeformManager : MonoBehaviour
{

    public GameObject SelfDestructParticle; // Putting it here because we can't bake it into the Destroyed state.

    protected static LifeformManager static_instance;
    public static LifeformManager Instance
    {
        get
        {
            return static_instance;
        }
    }
    public enum ETankState
    {
        READY,
        DISMISSED,
        DESTROYED
    }
    // Verb list that the panel can send to the simulation.
    public enum EControlVerbs
    {
        READY_TANK,
        DISMISS_TANK,
        SELF_DESTRUCT_TANK,
        PUZZLE_COMPLETE,
        PUZZLE_ERROR,
        PUZZLE_PROGRESS,
        NEW_STAGE_STARTING,
    }

    public bool IsLifeFormDestroyed = false;


    public StateMachine stateMachine = new StateMachine();


    public LifeformTank TankReference;

    public int PuzzlesCompleted = 0;


    public bool TankIsReady;
    public ETankState TankState = ETankState.DISMISSED;


    private void Awake()
    {
        // Set up static instance.
        static_instance = this;
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
    }



    public void ReceivedVerb(Component source, EControlVerbs Verb, int data)
    {
        Debug.Log("LifeFormManager received verb " + Verb.ToString());

        stateMachine.HandleVerb(source, Verb, data);

        //if (Verb == EControlVerbs.READY_TANK)
        //{
        //    TankReference.SummonTank();
            
        //}
        //else if (Verb == EControlVerbs.DISMISS_TANK)
        //{
        //    TankReference.DismissTank();
        //}
        
    }

    // Start is called before the first frame update
    void Start()
    {
        TankReference = FindObjectOfType<LifeformTank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetCreatureStateVisuals(int StageNumber)
    {
        // TODO
    }

    public int GetNumberOfPuzzlesRequiredForStage(int StageNumber)
    {
        // TODO: make this a table.
        return Mathf.Max(1, StageNumber / 2);
    }

    public int GetNumberOfFailuresAllowedForStage(int StageNumber)
    {
        // TODO: make this a table.
        return Mathf.Max(2, 5 - StageNumber);
    }
}
