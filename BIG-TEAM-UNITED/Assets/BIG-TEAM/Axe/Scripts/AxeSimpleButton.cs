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

    void Awake()
    {
        Signals.Get<EnableButtonSignal>().AddListener(EnableButton);
        Signals.Get<DisableButtonSignal>().AddListener(DisableButton);
    }


    public void EnableButton(int BID)
    {
        if(BID == ID)
        {
            interactable = true;
        }
    }

    public void DisableButton(int BID)
    {
        if (BID == ID)
        {
            interactable = false;
        }
    }


    private void OnMouseDown()
    {
        if(interactable)
        {
            Controller.SetTrigger("PlayClick");
            Signals.Get<ButtonPressedSignal>().Dispatch(ID);
        }
    }

}
