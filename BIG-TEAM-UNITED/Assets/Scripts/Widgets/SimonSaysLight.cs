using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysLight : MonoBehaviour
{
    /// <summary>
    /// light ID
    /// </summary>
    public int ID;

    public Material Off, Red, Yellow, Green;
    public Renderer Bulb;

    public SignalHub SSSignals = null;

    private void Start()
    {
        SimonSaysPuzzle Parent = GetComponentInParent<SimonSaysPuzzle>();

        if (Parent)
        {
            SSSignals = Parent.SimonSaysHub;
        }
        SSSignals.Get<LightOnGreenSignal>().AddListener(OnGreen);
        SSSignals.Get<LightOnYellowSignal>().AddListener(OnYellow);
        SSSignals.Get<LightOnRedSignal>().AddListener(OnRed);
        SSSignals.Get<LightOffSignal>().AddListener(OffDull);
         
    }


    /// <summary>
    /// Turn the lights with assigned ID green
    /// </summary>
    /// <param name="LID">Light ID</param>
    public void OnGreen(int LID)
    {
        if (LID == ID)
        {
            Bulb.material = Green;
        }
    }
    /// <summary>
    /// Turn the lights with assigned ID yellow
    /// </summary>
    /// <param name="LID">Light ID</param>
    public void OnYellow(int LID)
    {
        if (LID == ID)
        {
            Bulb.material = Yellow;
        }
    }
    /// <summary>
    /// Turn the lights with assigned ID red
    /// </summary>
    /// <param name="LID">Light ID</param>
    public void OnRed(int LID)
    {
        if (LID == ID)
        {
            Bulb.material = Red;
        }
    }
    /// <summary>
    /// Turn the lights with assigned ID off
    /// </summary>
    /// <param name="LID">Light ID</param>
    public void OffDull(int LID)
    {
        if (LID == ID)
        {
            Bulb.material = Off;
        }
    }


}
