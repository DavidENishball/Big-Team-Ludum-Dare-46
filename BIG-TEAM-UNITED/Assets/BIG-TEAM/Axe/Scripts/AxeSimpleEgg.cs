using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AxeSimpleEgg : MonoBehaviour
{
    /// <summary>
    /// Angles per second the egg spins
    /// </summary>
    public float SpinSpeed;
    /// <summary>
    /// Cap value for speed
    /// </summary>
    public float MaxSpeed;

    private void Update()
    {
        var angle = 1f / SpinSpeed;
        transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + (SpinSpeed * Time.deltaTime), Vector3.up);
    }

    /// <summary>
    /// Initialisie all of the listeners
    /// </summary>
    void Awake()
    {
        Signals.Get<EggSpinUpSignal>().AddListener(SpinUp);
        Signals.Get<EggSpinDownSignal>().AddListener(SpinDown);
        Signals.Get<EggStopSignal>().AddListener(StopSpin);
    }


    /// <summary>
    /// Spin up the egg if its listening
    /// </summary>
    /// <param name="Val">asserted positive value added to current speed</param>
    public void SpinUp(float Val)
    {
        Assert.IsTrue(Val >= 0);
        SpinSpeed = Mathf.Min(SpinSpeed + Val, MaxSpeed);
    }

    /// <summary>
    /// Spin down the egg if its listening
    /// </summary>
    /// <param name="Val">asserted positive value subtracted from current speed</param>
    public void SpinDown(float Val)
    {
        Assert.IsTrue(Val >= 0);
        SpinSpeed = Mathf.Max(SpinSpeed - Val, 0);
    }

    /// <summary>
    /// Stop the egg if it's lsitening
    /// </summary>
    public void StopSpin()
    {
        SpinSpeed = 0;
    }

}
