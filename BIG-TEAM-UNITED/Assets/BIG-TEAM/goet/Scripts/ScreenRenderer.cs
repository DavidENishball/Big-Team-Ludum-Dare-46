using UnityEngine;

public class ScreenRenderer : MonoBehaviour
{
    public Camera renderCamera;
    public GameObject renderTarget;
    public Vector2Int renderTextureDimensions = new Vector2Int(480, 480);

    private RenderTexture renderTexture;

    private void Awake()
    {
        renderTexture = new RenderTexture(renderTextureDimensions.x, renderTextureDimensions.y, 24);
        renderCamera.targetTexture = renderTexture;
        renderTarget.GetComponent<Renderer>().material.mainTexture = renderTexture;
    }
}