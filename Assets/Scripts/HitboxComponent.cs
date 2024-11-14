using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent healthComponent;

    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void Damage(int amount)
    {
        healthComponent.Subtract(amount);
    }

    public void Damage(Bullet bullet)
    {
        healthComponent.Subtract(bullet.damage);
    }
}
