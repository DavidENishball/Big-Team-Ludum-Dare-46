using deVoid.Utils;
using UnityEngine;

/// <summary>
/// Controller for a knob you can rotate to set it between its min and max value.
/// </summary>
public class Knob : MonoBehaviour, IValueInteractable
{
    public float minValue = -1;
    public float maxValue = 1;
    public float value;
    public LifeformManager.EControlVerbs Command;

    public GameObject thingToRotate;
    public int ID = 0;

    private bool pressed = false;

    private const float maxRotation = 90f;
    private float rotation = 0f;

    private Vector3 lastMousepos = Vector3.zero;

    void Start()
    {
        RotateBasedOnValue();
    }

    // interface stuff
    public float GetValue()
    {
        return value;
    }

    public void SetValue(float newValue)
    {
        value = newValue;
        RotateBasedOnValue();
    }

    // maths
    private void RotateBasedOnMousePosition()
    {
        var delta = Input.mousePosition.x - lastMousepos.x;
        Rotate(delta);
        lastMousepos = Input.mousePosition;
    }

    private void Rotate(float delta)
    {
        rotation = Mathf.Clamp(rotation + delta, -maxRotation, maxRotation);
        thingToRotate.transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    private void UpdateValueBasedOnRotation()
    {
        var t = (rotation + maxRotation) / (2 * maxRotation);
        value = Mathf.Lerp(minValue, maxValue, t);

        Signals.Get<PerformVerbSignal>().Dispatch(this, Command, ID);
    }

    private void RotateBasedOnValue()
    {
        var t = Mathf.InverseLerp(minValue, maxValue, value);
        rotation = Mathf.Lerp(-maxRotation, maxRotation, t);
        Rotate(0f);
    }

    // responding to mouseclicks
    private void Update()
    {
        if (pressed)
        {
            if (Input.GetMouseButtonUp(0))
                pressed = false;

            RotateBasedOnMousePosition();
            UpdateValueBasedOnRotation();
        }
    }

    private void OnMouseDown()
    {
        pressed = true;
        lastMousepos = Input.mousePosition;
    }
}