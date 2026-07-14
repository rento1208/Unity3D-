using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 30f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed) move += Vector3.forward;
        if (Keyboard.current.downArrowKey.isPressed) move += Vector3.back;
        if (Keyboard.current.leftArrowKey.isPressed) move += Vector3.left;
        if (Keyboard.current.rightArrowKey.isPressed) move += Vector3.right;

        rb.linearVelocity = new Vector3(move.x * moveSpeed,
                                        rb.linearVelocity.y,
                                        move.z * moveSpeed);
    }
}