using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
[RequireComponent(typeof(MeshRenderer))]
public class Interact : MonoBehaviour
{
    public Color NeutralColor;
    public Color HoverColor;
    public Color ClickColor;

    public float FadeTime = 2.0f;

    protected Material MyMaterial;

    protected TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> CachedColorTween;

    private void Start()
    {
        MyMaterial = GetComponent<MeshRenderer>().material;
        CachedColorTween = MyMaterial.DOColor(HoverColor, FadeTime);
        CachedColorTween.SetAutoKill(false);
        CachedColorTween.Pause();
    }



    public UnityEvent interactEvent;
    private void OnMouseDown()
    {
        interactEvent.Invoke();
        MyMaterial.color = ClickColor;
    }

    private void OnMouseEnter()
    {
        MyMaterial.DOColor(HoverColor, FadeTime);
        //CachedColorTween.Play();
       // CachedColorTween.Restart();
    }

    private void OnMouseExit()
    {
        MyMaterial.DOColor(NeutralColor, FadeTime);
    }
}