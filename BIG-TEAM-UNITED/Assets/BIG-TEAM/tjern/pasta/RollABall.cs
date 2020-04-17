using UnityEngine;
public class RollABall : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public Rigidbody Rb;

    private void Awake()
    {
        Rb.maxAngularVelocity = 14f;
    }
    private void FixedUpdate()
    {

        var moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        var cameraForward = Camera.main.transform.transform.TransformDirection(Vector3.right);
        cameraForward.y = 0;
        cameraForward.Normalize();

        var moveDir = moveVector.y * cameraForward + moveVector.x * -Camera.main.transform.forward;
        moveDir = Vector3.ClampMagnitude(moveDir, 1);
        moveDir.y = 0f;
        Rb.AddTorque(moveDir * MoveSpeed, ForceMode.Impulse);

        var jump = Input.GetButtonDown("Jump");
        if (jump == true) //liq does't like it when I do == true :)))))))))))))))))))
        {
            Rb.velocity += Vector3.up * 5f;
        }

        if (transform.position.y < -10f)
        {
            Rb.velocity = Vector3.zero;
            transform.position = new Vector3(0, 10, 0);
        }
    }
}