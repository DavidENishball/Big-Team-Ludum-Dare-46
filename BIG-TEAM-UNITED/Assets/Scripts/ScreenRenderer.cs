using UnityEngine;

public class ScreenRenderer : MonoBehaviour
{
    public GameObject renderTarget;
    public string layer;
    public Vector2Int renderTextureDimensions = new Vector2Int(480, 480);

    private RenderTexture renderTexture;
    private Camera renderCamera;
    private Canvas canvas;

    private void Awake()
    {
        renderTexture = new RenderTexture(renderTextureDimensions.x, renderTextureDimensions.y, 24);

        renderCamera = GetComponentInChildren<Camera>(includeInactive: true);
        renderCamera.targetTexture = renderTexture;
        renderCamera.cullingMask = 1 << LayerMask.NameToLayer(layer);
        renderCamera.gameObject.SetActive(true);

        renderTarget.GetComponent<Renderer>().material.mainTexture = renderTexture;

        canvas = GetComponentInChildren<Canvas>(includeInactive: true);
        canvas.gameObject.SetActive(true);
    }
}