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

        Debug.Log(gameObject.name + " の死亡回数：" + deathCount);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
}