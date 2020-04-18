using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using deVoid.Utils;
public class LifeformTank : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TankGlass;
    public GameObject Lifeform;



    public Vector3 DismissedOffset;

    // These will be derived at runtime.
    public Vector3 DismissedPosition;
    public Vector3 ReadyPosition;

    public StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        Signals.Get<ReadyTankSignal>().AddListener(SummonTank);
        Signals.Get<DismissTankSignal>().AddListener(DismissTank);
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);
    }


    public void ReceivedVerb(Component source, LifeformManager.EControlVerbs Verb, int data)
    {
        Debug.Log("LifeFormTank received verb " + Verb.ToString());

        stateMachine.HandleVerb(source, Verb, data);
    }

    private void Start()
    {
        ReadyPosition = transform.position;
        DismissedPosition = ReadyPosition + DismissedOffset;

        stateMachine.ChangeState(new State_Tank_Ready(this));
    }


    public void Reset()
    {
        // TODO.  Fix glass, clean it, etc.
    }

    public void SummonTank()
    {
        Lifeform.SetActive(true);

        // TODO: LifeForm.Reset();
        TankGlass.transform.DOMove(ReadyPosition, 1.0f).SetEase(Ease.OutCirc);
    }

    public void DismissTank()
    {
        TankGlass.transform.DOMove(DismissedPosition, 1.0f).SetEase(Ease.InCirc).OnComplete(OnDismissFinished);
    }


    public void OnDismissFinished()
    {
        Lifeform.SetActive(false);
    }


}


