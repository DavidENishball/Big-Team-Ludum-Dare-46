using deVoid.Utils;
using System.Collections.Generic;
using UnityEngine;


public class SimonSaysPuzzle : PuzzleManager_Base
{
    public SignalHub SimonSaysHub = new SignalHub();

    public List<SimonSaysButton> LightButtons;

    private int ButtonCount = 0;

    public List<int> TargetSequence = new List<int>();
    public int CorrectInputID = 0, CurrentHintID = 0;


    public enum SimonPuzzleLevel
    {
        Start = 0,
        Easy = 1, 
        Medium = 2, 
        Hard = 3,
        Complete = 4
    }

    public SimonPuzzleLevel Level = 0;

    public float _lastInputTimer = 0, _lastHintTimer = 0, _errorTimer = 0;
    public float InputWaitTime= 3f, HintTime = 1f;

    public bool Hinting = false, Guessing = false, ErrorPause = false;
    // Start is called before the first frame update
    void Start()
    {
        //get count for random
        ButtonCount = LightButtons.Count;
        
        for(int i = 0; i < ButtonCount; i++)
        {
            LightButtons[i].ID = i;
        }

        ClearLights();
        
        SimonSaysHub.Get<SimonButtonPressedSignal>().AddListener(HandleButtonPress);

        GenerateSequence();

        //debug to see sequence
        //PresentSequence();
    }

    // Update is called once per frame
    void Update()
    {
        //Q&D shortcircuit
        if (!IsCompleted)
        {
            //time out for error
            if (ErrorPause)
            {
                _errorTimer += Time.deltaTime;
                if(_errorTimer >+InputWaitTime)
                {
                    ClearLights();
                    GenerateSequence();
                    ErrorPause = false;
                }
            }
            else
            {

                _lastInputTimer += Time.deltaTime;

                if (_lastInputTimer > InputWaitTime && !Guessing && !Hinting)
                {
                    SimonSaysHub.Get<FlashLightSignal>().Dispatch(TargetSequence[CurrentHintID], GetFlashesForDifficulty());

                    SFXPlayer.Instance.Beep(transform.position);
                    Hinting = true;
                }

                if (Hinting)
                {
                    _lastHintTimer += Time.deltaTime;

                    if (_lastHintTimer > HintTime)
                    {
                        CurrentHintID++;
                        //wrap the hint
                        if (CurrentHintID >= TargetSequence.Count)
                        {
                            //very dodgy force loop to wait ans restart, -1 because it will increment back to 0 on the new loop
                            CurrentHintID = -1;
                            _lastHintTimer = -InputWaitTime;
                        }
                        else
                        {
                            SimonSaysHub.Get<FlashLightSignal>().Dispatch(TargetSequence[CurrentHintID], GetFlashesForDifficulty());
                            SFXPlayer.Instance.Beep(transform.position);
                            _lastHintTimer = 0;
                        }

                    }

                }
            }

        }
    }
    
    public void PresentSequence()
    {
        for (int i = 0; i < TargetSequence.Count; i++)
        {
            SimonSaysHub.Get<FlashLightSignal>().Dispatch(TargetSequence[i], GetFlashesForDifficulty());
        }
    }

    public override void PuzzleComplete()
    {
        base.PuzzleComplete();
        SFXPlayer.Instance.PositiveSound(transform.position);
    }

    public override void PuzzleError()
    {
        base.PuzzleError();
    }
    public override void Reset()
    {
        base.Reset();
    }

    public void ClearLights()
    {
        for (int i = 0; i < ButtonCount; i++)
        {
            SimonSaysHub.Get<ClearLightSignal>().Dispatch(i);
        }
    }

    public void FlashLights()
    {
        for (int i = 0; i < ButtonCount; i++)
        {
            SimonSaysHub.Get<FlashLightSignal>().Dispatch(i, GetFlashesForDifficulty());
        }
    }

    public void LockLights()
    {
        for (int i = 0; i < ButtonCount; i++)
        {
            SimonSaysHub.Get<LockLightSignal>().Dispatch(i);
        }
    }



    public int GetFlashesForDifficulty()
    {
        int count = 1;
        switch(Level)
        {
            case (SimonPuzzleLevel.Start):
                count = 1;
                break;
            case (SimonPuzzleLevel.Easy):
                count = 1;
                break;
            case (SimonPuzzleLevel.Medium):
                count = 1;
                break;
            case (SimonPuzzleLevel.Hard):
                count = 1;
                break;
            case (SimonPuzzleLevel.Complete):
                count = 1;
                break;
        }

        return count;
    }

    public float GetFlashDurationPerDifficulyLevel()
    {
        float duration = 0;
        switch (Level)
        {
            case (SimonPuzzleLevel.Start):
                duration = 0.3f;
                break;
            case (SimonPuzzleLevel.Easy):
                duration = 0.25f;
                break;
            case (SimonPuzzleLevel.Medium):
                duration = 0.2f;
                break;
            case (SimonPuzzleLevel.Hard):
                duration = 0.15f;
                break;
            case (SimonPuzzleLevel.Complete):
                duration = 0.0f;
                break;
        }

        return duration;
    }

    public int GetSequenceLengthForDifficulty()
    {
        int count = 0;
        switch (Level)
        {
            case (SimonPuzzleLevel.Start):
                count = 3;
                break;
            case (SimonPuzzleLevel.Easy):
                count = 3;
                break;
            case (SimonPuzzleLevel.Medium):
                count = 4;
                break;
            case (SimonPuzzleLevel.Hard):
                count = 5;
                break;
            case (SimonPuzzleLevel.Complete):
                count = 0;
                break;
        }

        return count;
    }


    public void GenerateSequence()
    {
       int[] ids = new int[ButtonCount];

        for(int i = 0; i < ButtonCount; i++)
        {
            ids[i] = i;
        }
        Shuffle<int>(ids);

        int length = GetSequenceLengthForDifficulty();

        int[] sequence = new int[length];

        for(int i = 0; i < length; i++)
        {
            sequence[i] = ids[i];
        }

        TargetSequence.Clear();
        TargetSequence.AddRange(sequence);
        CorrectInputID = 0;
        CurrentHintID = 0;
        _errorTimer = _lastHintTimer = _lastInputTimer = 0;
        Guessing = Hinting = ErrorPause = false;
    }

    static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            int r = i + Random.Range(0,n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }


    public void HandleButtonPress(int BID)
    {
        if(!ErrorPause)
        {

            _lastInputTimer = 0f;

            if(Hinting)
            {
                //clear and stop all flashing 
                ClearLights();
                Guessing = true;
                Hinting = false;
            }

            if (BID == TargetSequence[CorrectInputID])
            {
                SimonSaysHub.Get<CorrectLightSignal>().Dispatch(BID);
                CorrectInputID++;
                SFXPlayer.Instance.Beep(transform.position);
                if(CorrectInputID >= TargetSequence.Count)
                {
                    SimonSaysHub.Get<LightOnGreenSignal>().Dispatch(10 + (int)Level);
                    Level = Level + 1;
                    if(Level == SimonPuzzleLevel.Complete)
                    {
                        ClearLights();
                        PuzzleComplete();
                    }
                    else
                    {
                        ClearLights();
                        Guessing = false;
                        Hinting = false;
                        GenerateSequence();
                        SFXPlayer.Instance.PositiveSound(transform.position);
                    }
                
                }
            }
            else
            {
                SimonSaysHub.Get<IncorrectLightSignal>().Dispatch(BID);
                SFXPlayer.Instance.NegativeSound(transform.position);
                PuzzleError();

                Guessing = false;
                ErrorPause = true;
                _errorTimer = 0;
                LockLights();

                CorrectInputID = 0;
            }

        }
    }
}
