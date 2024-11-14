using UnityEngine;

public class EnemyTargeting : Enemy
{
    public Transform player;

    public override void Awake()
    {
        base.Awake();

        GameObject playerObject = GameObject.FindWithTag("Player"); 
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError(this + " tidak ada Player");
        }

        
        if (mainCamera != null)
        {
            float spawnX = Random.Range(0, Screen.width);
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(spawnX, Screen.height, mainCamera.transform.position.z));
            transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        }
        else
        {
            Debug.LogError(this + " tidak ada MainCamera");
        }
    }

    public override void Move()
    {
        if (player == null) return;  

        
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Menghancurkan EnemyTargeting jika menabrak Player
        if (collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
