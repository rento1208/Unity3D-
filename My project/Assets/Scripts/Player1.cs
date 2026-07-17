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
        // Spaceキーでジャンプ
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // 移動入力
        Vector3 input = Vector3.zero;

        if (Keyboard.current.wKey.isPressed) input += Vector3.forward;
        if (Keyboard.current.sKey.isPressed) input += Vector3.back;
        if (Keyboard.current.aKey.isPressed) input += Vector3.left;
        if (Keyboard.current.dKey.isPressed) input += Vector3.right;

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