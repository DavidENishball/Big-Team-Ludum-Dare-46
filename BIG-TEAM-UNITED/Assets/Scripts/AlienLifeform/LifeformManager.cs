using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
// Class for managing the overall state of the lifeform and acts as a router between controlpanel signals and the state of the character.
public class LifeformManager : MonoBehaviour
{
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
        SELF_DESTRUCT_TANK
    }


    StateMachine stateMachine = new StateMachine();


    public LifeformTank TankReference;


    public bool TankIsReady;
    public ETankState TankState = ETankState.DISMISSED;


    private void Awake()
    {
        Signals.Get<ReadyTankSignal>().AddListener(ReceiveSummonTankCommand);
        Signals.Get<DismissTankSignal>().AddListener(ReceiveDismissTankCommand);
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
    }



    public void ReceivedVerb(Component source, EControlVerbs Verb, int data)
    {
        Debug.Log("LifeFormManager received verb " + Verb.ToString());
        if (Verb == EControlVerbs.READY_TANK)
        {
            TankReference.SummonTank();
            
        }
        else if (Verb == EControlVerbs.DISMISS_TANK)
        {
            TankReference.DismissTank();
        }
    }

    public void ReceiveSummonTankCommand()
    {
        TankReference.stateMachine.ChangeState(new State_Tank_Ready(TankReference));
    }

    public void ReceiveDismissTankCommand()
    {
        // Augh, state machines.
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
}
