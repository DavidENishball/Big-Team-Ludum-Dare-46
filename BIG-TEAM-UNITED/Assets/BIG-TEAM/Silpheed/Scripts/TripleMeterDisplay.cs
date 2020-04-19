using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripleMeterDisplay : MonoBehaviour
{
    [Header("Applicable Meter Scale")]
    public float EffectiveScale = 0.95f;

    [Header("UI Parts")]
    public RectTransform Meter;
    public Image Background;
    [Space]
    public Image GreenNeedle;
    public Image BlueNeedle;
    public Image PinkNeedle;
    [Space]
    public Image TargetZone;

    private Transform greenTF;
    private Transform blueTF;
    private Transform PinkTF;

    private float effectiveRange;
    private float effectiveGoalRange;

    void Start()
    {
        TargetZone.gameObject.SetActive(false);
        greenTF = GreenNeedle.rectTransform.transform;
        blueTF = BlueNeedle.rectTransform.transform;
        PinkTF = PinkNeedle.rectTransform.transform;
        
        effectiveRange = (Meter.rect.width / 2f) * EffectiveScale;
        effectiveGoalRange = ((Meter.rect.width - TargetZone.rectTransform.rect.width) / 2f) * EffectiveScale;

        //Set the initial positions. Randomized on each reset.
        SetRandomNeedlePositions();
    }

    // Something that another random button elsewhere could call.
    public void SetRandomNeedlePositions() {
        Vector3 gp = greenTF.localPosition;
        Vector3 bp = blueTF.localPosition;
        Vector3 pp = PinkTF.localPosition;

        gp.x = Random.Range(-effectiveRange, effectiveRange);
        bp.x = Random.Range(-effectiveRange, effectiveRange);
        pp.x = Random.Range(-effectiveRange, effectiveRange);

        greenTF.localPosition = gp;
        blueTF.localPosition = bp;
        PinkTF.localPosition = pp;
    }

    void SetBackGroundColor(float normalizedColor)
    {
        normalizedColor = Mathf.Clamp01(normalizedColor);
        float colorVal = Mathf.Lerp(0, 255, normalizedColor);
        Background.color = new Color(colorVal, colorVal, colorVal);
    }

    public void SetGoalZonePosition(float normalizedPosition) {

        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        Vector3 gp = TargetZone.transform.localPosition;
        gp.x = Mathf.Lerp(-effectiveGoalRange, effectiveGoalRange, normalizedPosition);
        TargetZone.transform.localPosition = gp;
    }

    public void SetGreenNeedlePosition(float normalizedPosition) {

        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        Vector3 gp = greenTF.localPosition;
        gp.x = Mathf.Lerp(-effectiveRange, effectiveRange, normalizedPosition);
        greenTF.localPosition = gp;
    }

    public void SetBlueNeedlePosition(float normalizedPosition) {

        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        Vector3 bp = blueTF.localPosition;
        bp.x = Mathf.Lerp(-effectiveRange, effectiveRange, normalizedPosition);
        greenTF.localPosition = bp;
    }

    public void SetPinkNeedlePosition(float normalizedPosition) {

        normalizedPosition = Mathf.Clamp01(normalizedPosition);
        Vector3 pp = greenTF.localPosition;
        pp.x = Mathf.Lerp(-effectiveRange, effectiveRange, normalizedPosition);
        greenTF.localPosition = pp;
    }
}
