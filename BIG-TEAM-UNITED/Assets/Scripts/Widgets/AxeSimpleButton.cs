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
    public LifeformManager.EControlVerbs Command; // Using an enum so we can pick it in the editor.


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


    protected virtual void OnMouseDown()
    {
        if(interactable)
        {
            if (Controller != null)
            {
                Controller.SetTrigger("PlayClick");
            }
            Signals.Get<ButtonPressedSignal>().Dispatch(this, ID);
            Signals.Get<PerformVerbSignal>().Dispatch(this, Command, ID);
        }
    }

}
