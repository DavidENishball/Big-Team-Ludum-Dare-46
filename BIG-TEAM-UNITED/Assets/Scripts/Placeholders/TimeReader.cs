using TMPro;
using UnityEngine;

public class TimeReader : MonoBehaviour
{
    public TextMeshProUGUI tmpro;

    private void Update()
    {
        tmpro.text = LifeformManager.Instance.timer.RemainingReadable;
    }
}