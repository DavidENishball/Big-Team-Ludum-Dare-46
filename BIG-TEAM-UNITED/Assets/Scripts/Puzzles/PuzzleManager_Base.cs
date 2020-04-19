using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;

public class PuzzleManager_Base : MonoBehaviour
{
    // Override this class to make a new puzzle.  Put it on the root of a new prefab.

    public bool IsCompleted = false;

    public virtual void Reset()
    {
        // This is only for debug.  New panels will be spawned fresh.
    }
    public virtual void PuzzleComplete()
    {
        IsCompleted = true;
        Signals.Get<PuzzleComplete>().Dispatch(this);
    }

    public virtual void PuzzleError()
    {
        Signals.Get<PuzzleError>().Dispatch(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Gather children?
        // Randomize?
    }

    // Update is called once per frame
    void Update()
    {
        // Randomize stuff?
        // Prompt the player?
    }
}
