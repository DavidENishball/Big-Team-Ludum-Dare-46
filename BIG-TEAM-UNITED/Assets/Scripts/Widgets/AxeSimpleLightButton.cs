using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSimpleLightButton : MonoBehaviour
{
    protected bool interactable = true;
    public int ID = 0;
    public LifeformManager.EControlVerbs Command; // Using an enum so we can pick it in the editor.

    public Material Dull, Red, Yellow, Green;


    protected virtual void Awake()
    {
        Signals.Get<EnableButtonSignal>().AddListener(EnableButton);
        Signals.Get<DisableButtonSignal>().AddListener(DisableButton);
    }


    protected virtual void EnableButton(Component ArgButton, int BID)
    {
        if(ArgButton == this)
        {
            interactable = true;
        }
    }

    protected virtual void DisableButton(Component ArgButton, int BID)
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
            Signals.Get<ButtonPressedSignal>().Dispatch(this, ID);
            Signals.Get<PerformVerbSignal>().Dispatch(this, Command, ID);
        }
    }

}
