using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettingSlider : AxeSimpleSlider
{
    public float targetValue;
    public float resetSpeed;

    protected override void Update()
    {
        base.Update();

        if (pressed)
            return;

        Value = Mathf.MoveTowards(Value, targetValue, resetSpeed * Time.deltaTime);
        SliderKnob.localPosition = new Vector3(SliderKnob.localPosition.x, Mathf.Lerp(MinY,MaxY,Value), SliderKnob.localPosition.z);
        UpdateValueBasedOnPosition();
    }
}
