using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 30f;

[Header("ジャンプ")]
    public float jumpForce = 10f;

    [Header("落下")]
    public float fallMultiplier = 2.5f;

    private Rigidbody rb;

    // 地面に接地しているか
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 右Shiftキーでジャンプ
        if (Keyboard.current.rightShiftKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // 移動入力
        Vector3 input = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed)
            input += Vector3.forward;

        if (Keyboard.current.downArrowKey.isPressed)
            input += Vector3.back;

        if (Keyboard.current.leftArrowKey.isPressed)
            input += Vector3.left;

        if (Keyboard.current.rightArrowKey.isPressed)
            input += Vector3.right;

        input = input.normalized;

        // XZ方向のみ移動し、Y方向は維持
        Vector3 velocity = rb.linearVelocity;

        velocity.x = input.x * moveSpeed;
        velocity.z = input.z * moveSpeed;

        rb.linearVelocity = velocity;

        // 落下中だけ重力を強くする
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity +=
                Vector3.up *
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
