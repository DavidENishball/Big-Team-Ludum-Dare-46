using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSimpleSlider : MonoBehaviour
{
    protected bool interactable = true;
    public int ID = 0;
    public LifeformManager.EControlVerbs Command; // Using an enum so we can pick it in the editor.

    public float MinY, MaxY;

    public Transform SliderKnob;

    public enum Alignment
    {
        Horizontal = 0,
        Vertical = 1,
    }

    public Alignment ControlAlignment = 0;

    public enum Mode
    {
        Snapped = 0,
        Free = 1,
    }
    public Mode SnapMode = 0;

    public float deltamush = .005f;
    /// <summary>
    /// cache for the moouse position
    /// </summary>
    private Vector3 lastMousepos = Vector3.zero;

    /// <summary>
    /// used to keep track of being currently active
    /// </summary>
    protected bool pressed = false;
    /// <summary>
    /// ordered list of transforms to snal the slider to
    /// </summary>
    public List<Transform> Marks;

    public float Value, lastValue;

    public float tempValue;
    void Awake()
    {
        Signals.Get<EnableButtonSignal>().AddListener(EnableButton);
        Signals.Get<DisableButtonSignal>().AddListener(DisableButton);

        ///calc mush
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


    // responding to mouseclicks
    protected virtual void Update()
    {
        if (pressed)
        {
            if (Input.GetMouseButtonUp(0))
                pressed = false;

            MoveBasedOnMousePosition();
            UpdateValueBasedOnPosition();
        }
    }

    private void OnMouseDown()
    {
        pressed = true;
        lastMousepos = Input.mousePosition;
        lastValue = Value;
    }

    protected void UpdateValueBasedOnPosition()
    {

        //value = Mathf.Lerp(minValue, maxValue, t);

        Signals.Get<PerformVerbSignal>().Dispatch(this, Command, ID);
    }

    // maths
    private void MoveBasedOnMousePosition()
    {

        var delta = 0f;
        var range = MaxY - MinY;

        switch (ControlAlignment)
        {
            case (Alignment.Horizontal):
                delta = Input.mousePosition.x - lastMousepos.x;
                break;

            case (Alignment.Vertical):
                delta = Input.mousePosition.y - lastMousepos.y;
                break;
        }

        delta = delta * deltamush;


        switch (SnapMode)
        {
            case (Mode.Snapped):

                tempValue = lastValue + delta;
                tempValue = Mathf.Clamp(Round(tempValue, .33f), 0, 1);

                //fix that little bugger
                if (tempValue == .99f)
                {
                    tempValue = 1f;
                }

                var top = SliderKnob.transform.localPosition;
                top.y = MaxY;
                var bottom = SliderKnob.transform.localPosition;
                bottom.y = MinY;

                SliderKnob.localPosition = Vector3.Lerp(bottom,top, tempValue);
                Value = tempValue;
                break;

            case (Mode.Free):
                SliderKnob.localPosition = new Vector3(SliderKnob.localPosition.x, Mathf.Clamp(SliderKnob.localPosition.y + delta, MinY, MaxY), SliderKnob.localPosition.z);
                Value = (SliderKnob.localPosition.y - MinY) / range;
                lastMousepos = Input.mousePosition;
                break;
        }

        

        
    }


    float Round(float value, float factor)
    {
        return (float)Math.Round(value / factor) * factor;
    }

}
