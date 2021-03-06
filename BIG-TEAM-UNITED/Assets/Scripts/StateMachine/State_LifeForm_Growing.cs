﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;

public class State_LifeForm_Growing : IState
{
    LifeformManager owner;

    public int StageNumber = 1;
    public int PuzzlesRequiredForNextStage = 1;
    public int PuzzlesCompleted = 0;
    public int PuzzleErrorsPerformed = 0;
    public int PuzzleErrorsAllowed = 0;

    public bool IsStageFailed = false;

    private Coroutine CachedTransitionCoroutine;

    public State_LifeForm_Growing(LifeformManager owner) { this.owner = owner; }

    public void Enter()
    {
        Debug.Log("entering State_LifeForm_Growing");
        owner.IsLifeFormDestroyed = false;
        Signals.Get<PuzzleComplete>().AddListener(HandlePuzzleCompleted);
        Signals.Get<PuzzleError>().AddListener(HandlePuzzleError);

        ResetCurrentStage();
    }

    public void Execute()
    {
        Debug.Log("updating State_LifeForm_Growing state");
    }

    public void Exit()
    {
        Debug.Log("exiting State_LifeForm_Growing state");
    }

    public void ResetCurrentStage()
    {
        PuzzlesCompleted = 0;
        PuzzlesRequiredForNextStage = owner.GetNumberOfPuzzlesRequiredForStage(StageNumber);
        PuzzleErrorsPerformed = 0;
        PuzzleErrorsAllowed = owner.GetNumberOfFailuresAllowedForStage(StageNumber);
        IsStageFailed = false;
        owner.SpawnNewPuzzles(StageNumber);
        Signals.Get<NewStageStartingSignal>().Dispatch(this);

    }

    public bool HandleVerb(Component Source, LifeformManager.EControlVerbs Verb, int Data)
    {

        if (Verb == LifeformManager.EControlVerbs.PUZZLE_COMPLETE)
        {
            HandlePuzzleCompleted(Source);
        }
        else if (Verb == LifeformManager.EControlVerbs.PUZZLE_ERROR)
        {
            HandlePuzzleError(Source);
        }
        return false;
    }

    public void HandlePuzzleCompleted(Component Source = null)
    {
        PuzzlesCompleted += 1;
        Debug.Log(string.Format("Stage {0} puzzle complete recorded.  {1} completions.  Maximum: {2}", StageNumber, PuzzlesCompleted, PuzzlesRequiredForNextStage));
        if (PuzzlesCompleted >= PuzzlesRequiredForNextStage)
        {
            Debug.Log(string.Format("Stage {0} complete with {1} completions.  Advancing.", StageNumber, PuzzlesCompleted));
            StageNumber += 1;
            //ResetCurrentStage();
            if (CachedTransitionCoroutine != null)
            {
                //CachedTransitionCoroutine;  // Reset it maybe?
            }
            owner.StartCoroutine(PlayEffectsAndAdvanceStage());
        }
    }


    public IEnumerator PlayEffectsAndAdvanceStage()
    {
        if (LifeformVisuals.Instance != null)
        {
            yield return (owner.StartCoroutine(LifeformVisuals.Instance.PlayEvolutionUpEffects()));
        }
        ResetCurrentStage();
    }

    public void HandlePuzzleError(Component Source = null)
    {
        if (!IsStageFailed)
        {
            PuzzleErrorsPerformed += 1;
            Debug.Log(string.Format("Stage {0} error recorded.  {1} errors.  Maximum: {2}", StageNumber, PuzzleErrorsPerformed, PuzzleErrorsAllowed));
            if (PuzzleErrorsPerformed >= PuzzleErrorsAllowed)
            {
                Debug.Log(string.Format("============= Stage {0} Failed.  {1} Errors", StageNumber, PuzzleErrorsPerformed));
                IsStageFailed = true;
                Signals.Get<StageFailed>().Dispatch(this);

            }
        }
    }


}