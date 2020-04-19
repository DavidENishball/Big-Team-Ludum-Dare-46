using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;


public class SimonSaysButton : AxeSimpleLightButton
{
    public SignalHub SSSignals = SimonSaysPuzzle.SimonSaysHub;
    public Renderer ButtonRenderer;

    private float _flashTimer = 0;
    public float FlashTimeUp = .2f, FlashTimeDown = .1f;
    public float FlashCount = 0, TimesToFlash = 0;

    public bool Flashing, LightUp, LightDown;

    // Start is called before the first frame update
    void Start()
    {
        SSSignals.Get<CorrectLightSignal>().AddListener(CorrectAndLock);
        SSSignals.Get<IncorrectLightSignal>().AddListener(IncorrectAndLock);
        SSSignals.Get<ClearLightSignal>().AddListener(Reset);
        SSSignals.Get<FlashLightSignal>().AddListener(StartFlash);
        SSSignals.Get<LockLightSignal>().AddListener(Lock);
    }

    // Update is called once per frame
    void Update()
    {
        //flash logic
        if(Flashing)
        {
            _flashTimer += Time.deltaTime;
            if(LightUp)
            {
                if(_flashTimer > FlashTimeUp)
                {
                    ButtonRenderer.material = Dull;
                    LightUp = false;
                    LightDown = true;

                    _flashTimer = 0f;
                }
            }
            else if(LightDown)
            {
                if (_flashTimer > FlashTimeDown)
                {
                    FlashCount++;
                    //flash again
                    if (FlashCount < TimesToFlash)
                    {
                        ButtonRenderer.material = Yellow;
                        LightUp = true;
                        LightDown = false;
                    }
                    //done flashing
                    else
                    {
                        ClearFlash();
                        ButtonRenderer.material = Dull;
                    }

                    _flashTimer = 0f;
                }

            }
        }
    }


    public void Lock(int BID)
    {
        if (BID == ID)
        {
            interactable = false;
        }
    } 
    public void CorrectAndLock(int BID)
    {
        if (BID == ID)
        {
            ButtonRenderer.material = Green;
            interactable = false;
        }
    }

    public void IncorrectAndLock(int BID)
    {
        if (BID == ID)
        {
            ButtonRenderer.material = Red;
            interactable = false;
        }
    }

    public void HandleLightOff(int BID)
    {
        if(BID == ID)
        {
            ButtonRenderer.material = Dull;
        }
    }

    public void HandleLightOn(int BID)
    {
        if (BID == ID)
        {
            ButtonRenderer.material = Yellow;
        }
    }

    public void Reset(int BID)
    {
        if (BID == ID)
        {
            ClearFlash();
            interactable = true;
        }
    }

    public void StartFlash(int BID, int Count)
    {
        if (BID == ID)
        {
            InitFlash(Count);
        }
    }
    protected override void OnMouseDown()
    {
        if (interactable)
        {
            SSSignals.Get<SimonButtonPressedSignal>().Dispatch(ID);
        }
    }


    private void InitFlash(int Count)
    {
        ButtonRenderer.material = Yellow;
        TimesToFlash = Count;
        FlashCount = 0;
        _flashTimer = 0f;
        LightUp = true;
        LightDown = false;
        Flashing = true;
    }

    private void ClearFlash()
    {
        ButtonRenderer.material = Dull;
        TimesToFlash = 0;
        FlashCount = 0;
        _flashTimer = 0f;
        LightUp = false;
        LightDown = false;
        Flashing = false;
    }
}
