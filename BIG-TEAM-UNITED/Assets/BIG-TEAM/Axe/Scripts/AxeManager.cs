using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeManager : MonoBehaviour
{

    public int CorrectButtonID = 0;

    public int CurrentCorrectSteps = 0;
    public bool Failed = false;


    public bool firstframe = true;

    private float _holdTimer = 0f;
    public float holdTime = 2f;

    private void Start()
    {
        Signals.Get<ButtonPressedSignal>().AddListener(HandleButton);
    }

    // Update is called once per frame
    void Update()
    {
        if(firstframe)
        {
            UpdateLights();
            firstframe = false;
        }


        //hold time should freeze up all inputs
        if(_holdTimer > 0)
        {
            _holdTimer = Mathf.Max(_holdTimer - Time.deltaTime, 0);
        }
        else
        {
            //next frame cleanups
            if (Failed)
            {
                Signals.Get<EggSpinDownSignal>().Dispatch(6f);
                CurrentCorrectSteps = 0;
                Failed = false;
                UpdateLights();
            }

            if (CurrentCorrectSteps == 3)
            {
                Signals.Get<EggSpinUpSignal>().Dispatch(12f);
                CurrentCorrectSteps = 0;
                UpdateLights();
            }
        }
        

       
    }

    public void HandleButton(int ID)
    {
        if(_holdTimer == 0)
        {
            if(!Failed)
            {
                if (ID == CorrectButtonID)
                {
                    CurrentCorrectSteps++;
                    if(CurrentCorrectSteps == 3)
                    {
                        _holdTimer = holdTime;
                    }
                       
                }
                else
                {
                    Failed = true;
                    _holdTimer = holdTime;
                }

            }

            UpdateLights();
        }
    }

    public void UpdateLights()
    {
        //loop over lights
        for (int i = 1; i <= 3; i++)
        {
            //if i have 2 correct inputs then 1 and 2 will be green, yellow pending input
            if (i <= CurrentCorrectSteps)
            {
                Signals.Get<LightOnGreenSignal>().Dispatch(i);
            }
            else
            {
                if(Failed)
                {
                    if(i == CurrentCorrectSteps + 1)
                    {
                        Signals.Get<LightOnRedSignal>().Dispatch(i);
                    }
                    else
                    {
                        Signals.Get<LightOffSignal>().Dispatch(i);
                    }
                    
                }
                else
                {
                    if (i == CurrentCorrectSteps + 1)
                    {
                        Signals.Get<LightOnYellowSignal>().Dispatch(i);
                    }
                    else
                    {
                        Signals.Get<LightOffSignal>().Dispatch(i);
                    }
                }

            }
        }
    }

}
