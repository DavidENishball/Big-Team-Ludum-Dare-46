using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheValueLight : MonoBehaviour
{
    /// <summary>
    /// light ID
    /// </summary>
    public int ID;

    public int expectedValue;
    public Material onMaterial;

    public Material Off;
    public Renderer Bulb;

    void Awake()
    {
        Signals.Get<FindTheValueDiffLight>().AddListener(SetLight);
    }

    private void SetLight(Component source, int value)
    {
        if (this.gameObject.transform.IsChildOf(source.transform))
        {
            Bulb.material = value == expectedValue ? onMaterial : Off;
        }
    }

}
