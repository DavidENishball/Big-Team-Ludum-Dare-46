using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PanningCameraControls : MonoBehaviour
{

    [Header("Movement and Zoom")]
    public float MoveSpeed;
    public float ZoomFOV;

    [Header("Limits")]
    public float MaximumHeight = 0.3f;
    public float MinimumHeight = -0.4f;
    [Space]
    public float MaximumLateral = 0.8f;
    public float MinimumLateral = -0.9f; //Gives a lot of lead on the left side but could fill it in with some alien bubble gum. Needs to go this far to access leftmost panel with zoom.

    private Vector3 startingPosition;
    private float startingFOV;
    private Camera cam;
    private Vector3 moveIntent;
    private Vector3 targetPos;

    void Start()
    {
        cam = GetComponent<Camera>();
        startingPosition = cam.gameObject.transform.position;
        startingFOV = cam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            cam.fieldOfView = ZoomFOV;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            cam.fieldOfView = startingFOV;
        }

        moveIntent = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        targetPos = cam.transform.position + moveIntent.normalized * MoveSpeed * Time.deltaTime;

        //Keep camera within bounds.
        targetPos.x = Mathf.Clamp(targetPos.x, startingPosition.x + MinimumLateral, startingPosition.x + MaximumLateral);
        targetPos.y = Mathf.Clamp(targetPos.y, startingPosition.y + MinimumHeight, startingPosition.y + MaximumHeight);

        cam.gameObject.transform.position = targetPos;
    }
}
