using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackComponent : MonoBehaviour
{
    public float damage = 10f;
    public Bullet bullet;

    private void OnTriggerEnter(Collider other)
    {
        // Jika objek yang bertabrakan memiliki tag yang sama, hentikan
        if (other.CompareTag(gameObject.tag))
            return;

        // Dapatkan komponen HitboxComponent dari objek yang bertabrakan
        HitboxComponent hitboxComponent = other.GetComponent<HitboxComponent>();

        // Jika objek memiliki komponen HitboxComponent, panggil metode Damage
        if (hitboxComponent != null)
        {
            hitboxComponent.Damage(bullet);
        }
    }
}
