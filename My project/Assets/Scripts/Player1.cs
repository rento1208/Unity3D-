using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controller : MonoBehaviour
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

        if (Keyboard.current.wKey.isPressed) move += Vector3.forward;
        if (Keyboard.current.sKey.isPressed) move += Vector3.back;
        if (Keyboard.current.aKey.isPressed) move += Vector3.left;
        if (Keyboard.current.dKey.isPressed) move += Vector3.right;

        rb.linearVelocity = new Vector3(move.x * moveSpeed,
                                        rb.linearVelocity.y,
                                        move.z * moveSpeed);
    }
}