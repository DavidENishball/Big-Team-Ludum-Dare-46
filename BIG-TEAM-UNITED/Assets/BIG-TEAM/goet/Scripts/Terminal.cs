using System.Collections;
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public float secondsBetweenLines = 1f;

    private TextMeshProUGUI tmpro;

    private void Awake()
    {
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
        Display("welcome\nthis is my\ncool\ntest\nstring");
    }

    public void Display(string message)
    {
        tmpro.text = message;
        StartCoroutine(WriteByLines());
    }

    private IEnumerator WriteByLines()
    {
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