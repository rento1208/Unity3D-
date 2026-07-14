using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 30f;

    [Header("落下")]
    public float fallMultiplier = 2.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 移動入力
        Vector3 input = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed) input += Vector3.forward;
        if (Keyboard.current.downArrowKey.isPressed) input += Vector3.back;
        if (Keyboard.current.leftArrowKey.isPressed) input += Vector3.left;
        if (Keyboard.current.rightArrowKey.isPressed) input += Vector3.right;

        input = input.normalized;

        // XZ方向のみ移動し、Y方向は維持
        Vector3 velocity = rb.linearVelocity;
        velocity.x = input.x * moveSpeed;
        velocity.z = input.z * moveSpeed;
        rb.linearVelocity = velocity;

        // 落下中だけ重力を強くする
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    // ノックバック用
    public void KnockBack(Vector3 direction, float power)
    {
        rb.AddForce(direction.normalized * power, ForceMode.Impulse);
    }
}