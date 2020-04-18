using deVoid.Utils;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// The main controller for displaying strings on the computer.
/// </summary>
public class Terminal : MonoBehaviour
{
    public float secondsBetweenLines = 1f;
    public float secondsClearTime = 0.5f;

    private TextMeshProUGUI tmpro;
    private Coroutine currentAction;

    private void Awake()
    {
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
        Signals.Get<DisplayTerminalMessageSignal>().AddListener(Display);
    }

    /// <summary>
    /// You should probably call Signals.Get<DisplayTerminalMessageSignal>().Dispatch(message) instead.
    /// </summary>
    public void Display(string message)
    {
        tmpro.text = message;

        if (currentAction != null)
            StopCoroutine(currentAction);

        currentAction = StartCoroutine(WriteByLines());
    }

    private IEnumerator WriteByLines()
    {
        // clear screen, wait a bit
        tmpro.maxVisibleLines = 0;
        yield return new WaitForSeconds(secondsClearTime);

        // start displaying things line by line
        var totalLines = tmpro.text.Split('\n').Length;
        var revealedLines = 1;

        while (revealedLines < totalLines)
        {
            tmpro.maxVisibleLines = revealedLines;

            revealedLines++;

            yield return new WaitForSeconds(secondsBetweenLines);
        }

        tmpro.maxVisibleLines = totalLines;
    }
}