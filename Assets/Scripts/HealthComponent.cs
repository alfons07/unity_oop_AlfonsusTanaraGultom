using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float health;

    public float Health => health;

    private void Start()
    {
        ResetHealth();
    }

    private void ResetHealth()
    {
        health = maxHealth;
    }

    public void Subtract(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
