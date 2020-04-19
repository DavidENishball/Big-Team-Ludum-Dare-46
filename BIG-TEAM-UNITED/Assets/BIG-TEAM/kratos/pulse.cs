using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class pulse : MonoBehaviour
{
    public float duration = 1.0F;
    public float ampadjust = 1.0F;
    public float ampoffset = 0.5F;
    Light thisLight;
    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitude = Mathf.Abs(Mathf.Cos(phi)) * ampadjust + ampoffset;
        thisLight.intensity = Mathf.Clamp(amplitude , 2F, 10F); //clamp so it never goes "off"
    }
}
