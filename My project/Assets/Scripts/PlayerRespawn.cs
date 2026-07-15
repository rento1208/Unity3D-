using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;   // リスポーン位置
    public float deathY = -10f;      // この高さより下で死亡

    private Rigidbody rb;

    public int deathCount = 0;       // 死亡回数

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
}