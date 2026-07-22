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

    [Header("攻撃")]
    public float attackRange = 3f;

    [Header("チャージ")]
    public ChargeSystem2 chargeSystem;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip normalAttackSE;
    public AudioClip chargeAttackSE;
    public AudioClip chargeStartSE;

    private Rigidbody rb;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log("Gamepad数：" + Gamepad.all.Count);

        // P2コントローラーが接続されていない場合は処理しない
        if (Gamepad.all.Count <= 1)
        {
            return;
        }

        Gamepad gamepad = Gamepad.all[1];

        // Aボタンでジャンプ
        if (gamepad.aButton.wasPressedThisFrame &&
            isGrounded)
        {
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );

            isGrounded = false;
        }

        // Bボタンで通常攻撃
        if (gamepad.bButton.wasPressedThisFrame)
        {
            NormalAttack();
        }

        // Yボタンを押した瞬間
        if (gamepad.yButton.wasPressedThisFrame)
        {
            audioSource.PlayOneShot(chargeStartSE);
        }

        // Yボタンを押している間チャージ
        if (gamepad.yButton.isPressed)
        {
            chargeSystem.AddCharge(
                Time.deltaTime
            );
        }

        // Yボタンを離した瞬間にチャージ攻撃
        if (gamepad.yButton.wasReleasedThisFrame)
        {
            ChargeAttack();
        }
    }

    void FixedUpdate()
    {
        Vector2 stickInput = Vector2.zero;

        // P2コントローラー
        if (Gamepad.all.Count > 1)
        {
            stickInput =
                Gamepad.all[1].leftStick.ReadValue();
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

    // P2の通常攻撃
    void NormalAttack()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            attackRange
        );

        foreach (Collider hit in hits)
        {
            // P1に当たった
            if (hit.CompareTag("Player1"))
            {
                Player1Controller player1 =
                    hit.GetComponent<Player1Controller>();

                if (player1 != null)
                {
                    Vector3 direction =
                        player1.transform.position -
                        transform.position;

                    direction.y = 0f;

                    player1.KnockBack(
                        direction,
                        400f
                    );

                    // ★P1に当たった時だけSE
                    audioSource.PlayOneShot(
                        normalAttackSE
                    );
                }
            }
        }
    }

    // P2のチャージ攻撃
    void ChargeAttack()
    {
        // チャージ量に応じた威力
        float power =
            chargeSystem.GetKnockbackPower();

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            attackRange
        );

        foreach (Collider hit in hits)
        {
            // P1に当たった
            if (hit.CompareTag("Player1"))
            {
                Player1Controller player1 =
                    hit.GetComponent<Player1Controller>();

                if (player1 != null)
                {
                    Vector3 direction =
                        player1.transform.position -
                        transform.position;

                    direction.y = 0f;

                    player1.KnockBack(
                        direction,
                        power
                    );

                    // ★P1に当たった時だけSE
                    audioSource.PlayOneShot(
                        chargeAttackSE
                    );
                }
            }
        }

        // 攻撃後にチャージをリセット
        chargeSystem.ResetCharge();
    }

    // ノックバック用
    public void KnockBack(
        Vector3 direction,
        float power
    )
    {
        rb.AddForce(
            direction.normalized * power,
            ForceMode.Impulse
        );
    }
}