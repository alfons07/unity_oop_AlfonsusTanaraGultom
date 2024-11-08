using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    private void Awake()
    {
        weapon = weaponHolder;
    }

    private void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false, weapon);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objek Player Memasuki trigger");

            // Menyesuaikan posisi dan parenting weapon
            weapon.transform.position = other.transform.position;
            weapon.transform.SetParent(other.transform);

            // Aktifkan weapon dan visualnya
            TurnVisual(true, weapon);

            // Hancurkan objek WeaponPickup
            Destroy(gameObject);
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}


