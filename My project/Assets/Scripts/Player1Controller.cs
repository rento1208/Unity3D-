using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controller : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 10f;

    [Header("ジャンプ")]
    public float jumpForce = 10f;

    [Header("落下")]
    public float fallMultiplier = 10.0f;

    private Rigidbody rb;

    // 地面に接地しているか
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // P1コントローラーのAボタンでジャンプ
        if (Gamepad.all.Count > 0 &&
            Gamepad.all[0].aButton.wasPressedThisFrame &&
            isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // P1コントローラーの左スティックから入力取得
        Vector2 stickInput = Vector2.zero;

        if (Gamepad.all.Count > 0)
        {
            stickInput = Gamepad.all[0].leftStick.ReadValue();
        }

        // XZ方向へ移動
        Vector3 input = new Vector3(
            stickInput.x,
            0f,
            stickInput.y
        );

        input = input.normalized;

        // XZだけ変更してYは維持
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

    // 地面に着いた
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // ノックバック用
    public void KnockBack(Vector3 direction, float power)
    {
        rb.AddForce(direction.normalized * power, ForceMode.Impulse);
    }
}