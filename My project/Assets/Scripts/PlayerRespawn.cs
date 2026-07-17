using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float deathY = -10f;


private Rigidbody rb;

    public int deathCount = 0;

    private bool isInvincible = false;

    public bool IsInvincible
    {
        get { return isInvincible; }
    }

    // プレイヤーの見た目
    private Renderer[] renderers;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // プレイヤー自身と子オブジェクトのRendererを取得
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    void Die()
    {
        deathCount++;

        if (CompareTag("Player1"))
        {
            ScoreManager.p2Score++;
        }
        else if (CompareTag("Player2"))
        {
            ScoreManager.p1Score++;
        }

        Debug.Log($"P1:{ScoreManager.p1Score} P2:{ScoreManager.p2Score}");

        // 速度をリセット
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // リスポーン
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        // 2秒間無敵＆点滅
        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float invincibleTime = 2f;
        float blinkInterval = 0.1f;

        float timer = 0f;

        while (timer < invincibleTime)
        {
            // 表示・非表示を切り替える
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = !renderer.enabled;
            }

            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval;
        }

        // 最後は必ず表示
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        isInvincible = false;
    }

}
