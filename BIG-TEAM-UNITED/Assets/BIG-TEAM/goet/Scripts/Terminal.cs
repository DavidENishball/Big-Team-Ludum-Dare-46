using deVoid.Utils;
using System.Collections;
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public float secondsBetweenCharacters = 1f;

    public void Display(string message)
    {
        tmpro.text = message;
        StartCoroutine(WriteByLines());
    }

    private IEnumerator WriteByLines()
    {
        var totalLines = tmpro.text.Split('\n').Length;
        var revealedLines = 0;

        while (revealedLines < totalLines)
        {
            tmpro.maxVisibleLines = revealedLines;

            revealedLines++;

            yield return new WaitForSeconds(secondsBetweenCharacters);
        }

        tmpro.maxVisibleLines = totalLines;
    }
}