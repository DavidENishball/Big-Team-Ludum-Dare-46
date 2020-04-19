using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MeterDisplay : MonoBehaviour {

    [Header("Debug")]
    public bool Debug;
    [Range(0.0f, 1.0f)]
    public float RotationNormalize;

    [Header("Needle")]
    public GameObject Needle;

    [Header("Extents")]
    public Vector2 LocalPositionExtents; // <Min, Max>
    //NOTE: Rotates on Z-axis.
    public Vector2 LocalRotationExtents; // <Min, Max>


    public void Update() {
        if(Debug) SetRotation(RotationNormalize);
    }

    public void SetPosition(float normalizedPosition) {
        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        Vector3 np = Needle.transform.localPosition;
        np.x = Mathf.Lerp(LocalPositionExtents.x, LocalPositionExtents.y, normalizedPosition);
        Needle.transform.localPosition = np;
    }

    public void SetRotation(float normalizedRotation) {
        normalizedRotation = Mathf.Clamp01(normalizedRotation);
        float rotation = Mathf.Lerp(LocalRotationExtents.x, LocalRotationExtents.y, normalizedRotation);
        Needle.transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public float GetPosition() {
        return Needle.transform.localPosition.x;
    }

    public float GetPositionNormalize() {
        return (Needle.transform.localPosition.x - LocalPositionExtents.x) / (LocalPositionExtents.y - LocalPositionExtents.x);
    }

    public float GetRotation() {
        return Needle.transform.localRotation.z;
    }

    public float GetRotationNormalized() {

        return (Needle.transform.localRotation.z - LocalRotationExtents.x) / (LocalRotationExtents.y - LocalRotationExtents.x);
    }
}
