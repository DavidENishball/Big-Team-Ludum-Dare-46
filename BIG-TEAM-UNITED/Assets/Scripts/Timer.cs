using deVoid.Utils;
using UnityEngine;

public class Timer
{
    public float StartValue { get; set; }
    public float BonusPerStageCompletion { get; set; }
    public float PenaltyPerFailure { get; set; }
    public float DangerTime { get; set; }

    public float Remaining { get; set; }

    public Timer(float startValue, float bonusPerStageCompletion, float slowTickStartValue)
    {
        StartValue = startValue;
        BonusPerStageCompletion = bonusPerStageCompletion;
        DangerTime = slowTickStartValue;

        Remaining = StartValue;
    }

    public void AddBonus(Component source)
    {
        Remaining += BonusPerStageCompletion;
    }

    public void SubtractPenalty(Component source)
    {
        Remaining -= PenaltyPerFailure;
    }

    public void Tick()
    {
        if (Remaining > DangerTime)
            TickNormal();
        else
            TickSlow();

        Remaining = Mathf.Clamp(Remaining, 0f, float.PositiveInfinity);

        HandleTimeout();
    }

    private void TickNormal()
    {
        Remaining -= Time.deltaTime;
    }

    private void TickSlow()
    {
        Remaining -= (Time.deltaTime / 2);
    }

    private void HandleTimeout()
    {
        if (Remaining <= 0f)
            Signals.Get<TimeOut>().Dispatch();
    }
}