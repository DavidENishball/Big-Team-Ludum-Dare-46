using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSimpleButton : MonoBehaviour
{
    public Animator Controller;
    protected bool interactable = true;
    public int ID = 0;
    public LifeformManager.EControlVerbs Command;

    void Awake()
    {
        Signals.Get<EnableButtonSignal>().AddListener(EnableButton);
        Signals.Get<DisableButtonSignal>().AddListener(DisableButton);
    }


    public void EnableButton(Component ArgButton, int BID)
    {
        if(ArgButton == this)
        {
            interactable = true;
        }
    }

    public void DisableButton(Component ArgButton, int BID)
    {
        if (ArgButton == this)
        {
            interactable = false;
        }
    }


    private void OnMouseDown()
    {
        if(interactable)
        {
            Controller.SetTrigger("PlayClick");
            Signals.Get<ButtonPressedSignal>().Dispatch(this, ID);
            Signals.Get<PerformVerbSignal>().Dispatch(this, Command, ID);
        }
    }

}
