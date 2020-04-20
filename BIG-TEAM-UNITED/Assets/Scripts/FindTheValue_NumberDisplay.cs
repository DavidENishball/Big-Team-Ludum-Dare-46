using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindTheValue_NumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public FindTheValuePuzzle puzzle;

    // Update is called once per frame
    void Update()
    {
        tmpro.text = puzzle.currentValue.ToString("D3");
    }
}
