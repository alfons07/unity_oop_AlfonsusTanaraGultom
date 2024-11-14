using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private bool movingRight;
    private Vector3 screenBounds;

    public override void Awake()
    {
        base.Awake();

        if (mainCamera != null)
        {
            
            movingRight = Random.value > 0.5f;

            
            float spawnY = Random.Range(Screen.height / 2, Screen.height-50);
            float spawnX = movingRight ? Screen.width + 50 : -50;
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(spawnX, spawnY, mainCamera.transform.position.z));
            transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        }
        else
        {
            Debug.LogError(this + " tidak ada MainCamera");
        }
    }

    public void Start()
    {
        if (mainCamera != null)
        {
            
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }
        else
        {
            Debug.LogError(this + " tidak ada MainCamera");
        }
    }

    public override void Move()
    {
        if (mainCamera == null) return; 

        
        rb.velocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.velocity.y);

        
        if (transform.position.x > screenBounds.x)
        {
            movingRight = false;
        }
        else if (transform.position.x < -screenBounds.x)
        {
            movingRight = true;
        }
    }
}
