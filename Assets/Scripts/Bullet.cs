using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.velocity = transform.up * bulletSpeed; // Move bullet forward
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Return bullet to the pool on collision
        objectPool?.Release(this);
    }

    void OnBecameInvisible()
    {
        // Return bullet to pool when it goes off-screen
        objectPool?.Release(this);
    }
}




