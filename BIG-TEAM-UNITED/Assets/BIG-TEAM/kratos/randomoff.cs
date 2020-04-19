using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomoff : MonoBehaviour
{
    // Start is called before the first frame update
    Light thisLight;
    public float maxtime = 10F;
    public float mintime = 1F;
    public float offtimemin = 0.1f;
    public float offtimemax = 0.5f;
    public float lightintensity = 5F;
    private float randomtime;
    private float offtime;
    private bool isoff = false;
    private float timepassed = 0;
    void Start()
    {
        thisLight = GetComponent<Light>();
        randomtime = Random.Range(mintime, maxtime);
        offtime = Random.Range(offtimemin, offtimemax);
        thisLight.intensity = lightintensity;
    }

    // Update is called once per frame
    void Update()
    {
        timepassed += Time.deltaTime;


        if(timepassed >= randomtime && isoff == false)
        {
            isoff = true;
            timepassed = 0;
            randomtime = Random.Range(mintime, maxtime);
            thisLight.intensity = 0;
        }

        if(isoff == true)
        {
            if(timepassed >= offtime)
            {
                thisLight.intensity = lightintensity;
                timepassed = 0;
                isoff = false;
                offtime = Random.Range(offtimemin, offtimemax); //rest off time
            }


        }


    }
}
