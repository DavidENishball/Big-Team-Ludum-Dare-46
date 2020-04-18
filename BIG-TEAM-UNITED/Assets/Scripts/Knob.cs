using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    public float minValue = -1;
    public float maxValue = 1;
    public float value;
    public GameObject thingToRotate;
    public int ID = 0;
    public LifeformManager.EControlVerbs Command;

    private bool interactable = true;
    private bool pressed = false;

    private const float maxRotation = 90f;
    private float rotation = 0f;

    private Vector3 lastMousepos = Vector3.zero;

    private void Update()
    {
        if (pressed)
        {
            if (Input.GetMouseButtonUp(0))
                pressed = false;

            Rotate();
            UpdateValue();
        }
    }

    private void Rotate()
    {
        var delta = Input.mousePosition.x - lastMousepos.x;

        rotation = Mathf.Clamp(rotation + delta, -maxRotation, maxRotation);
        Debug.Log(rotation);

        thingToRotate.transform.localRotation = Quaternion.Euler(0, rotation, 0);

        lastMousepos = Input.mousePosition;
    }

    private void UpdateValue()
    {
        var t = (rotation + maxRotation) / (2 * maxRotation);
        value = Mathf.Lerp(minValue, maxValue, t);
    }

    private void OnMouseDown()
    {
        if (!interactable)
            return;

        pressed = true;
        lastMousepos = Input.mousePosition;
    }
}
