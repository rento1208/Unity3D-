using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 10f;

    [Header("ジャンプ")]
    public float jumpForce = 10f;

    [Header("落下")]
    public float fallMultiplier = 10.0f;

    private Rigidbody rb;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // P2コントローラーのAボタンでジャンプ
        if (Gamepad.all.Count > 1 &&
            Gamepad.all[1].aButton.wasPressedThisFrame &&
            isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 stickInput = Vector2.zero;

        // P2コントローラー
        if (Gamepad.all.Count > 1)
        {
            stickInput = Gamepad.all[1].leftStick.ReadValue();
        }

        Vector3 input = new Vector3(
            stickInput.x,
            0f,
            stickInput.y
        );

        input = input.normalized;

        Vector3 velocity = rb.linearVelocity;

        velocity.x = input.x * moveSpeed;
        velocity.z = input.z * moveSpeed;

        rb.linearVelocity = velocity;

        // 落下を速くする
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up *
                Physics.gravity.y *
                (fallMultiplier - 1) *
                Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void KnockBack(Vector3 direction, float power)
    {
        rb.AddForce(direction.normalized * power, ForceMode.Impulse);
    }
}