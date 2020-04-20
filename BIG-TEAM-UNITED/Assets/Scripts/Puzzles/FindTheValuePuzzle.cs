using deVoid.Utils;
using System.Collections.Generic;
using UnityEngine;

public class FindTheValuePuzzle : PuzzleManager_Base
{
    public int maxValue = 999;
    public int minValue = 0;
    public int startingValue = 500;
    public int maximumDiffFromStart = 300;
    public int correctValue;
    public int currentValue;
    public Knob knob100;
    public Knob knob10;
    public Knob knob1;

    private void Awake()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ListenForKnobs);
        CreateNewValue();
        currentValue = startingValue;
    }

    private void OnDestroy()
    {
        Signals.Get<PerformVerbSignal>().RemoveListener(ListenForKnobs);
    }

    public void CreateNewValue()
    {
        correctValue = Random.Range(startingValue - maximumDiffFromStart, startingValue + maximumDiffFromStart);
    }

    public override void Reset()
    {
        base.Reset();
        IsCompleted = false;
        CreateNewValue();
        currentValue = startingValue;
    }

    public void ListenForKnobs(Component source, LifeformManager.EControlVerbs verb, int id)
    {
        if (source.gameObject.transform.IsChildOf(this.transform))
        {
            if (!IsCompleted)
            {
                CalculateValue();
                var diff = currentValue - correctValue;
                LightLamp(diff);

                Debug.Log("current value: " + currentValue);

                if (diff == 0f)
                {
                    IsCompleted = true;
                    SFXPlayer.Instance.PositiveSound(this.transform.position);
                    SFXPlayer.Instance.PlayHeavyButtonNoise(this.transform.position);
                    Signals.Get<PuzzleComplete>().Dispatch(this);
                }
            }
        }
    }

    private void CalculateValue()
    {
        currentValue = Mathf.RoundToInt(knob100.value) * 100 + Mathf.RoundToInt(knob10.value) * 10 + Mathf.RoundToInt(knob1.value);
    }

    private void LightLamp(int diff)
    {
        var absDiff = Mathf.Abs(diff);
        int sign = diff / (absDiff == 0 ? 1 : absDiff);

        if (100 <= absDiff)
            Signals.Get<FindTheValueDiffLight>().Dispatch(this, sign * 100);
        else if (10 <= absDiff)
            Signals.Get<FindTheValueDiffLight>().Dispatch(this, sign * 10);
        else if (1 <= absDiff)
            Signals.Get<FindTheValueDiffLight>().Dispatch(this, sign * 1);
        else
            Signals.Get<FindTheValueDiffLight>().Dispatch(this, 0);
    }
}