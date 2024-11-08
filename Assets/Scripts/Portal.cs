using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed = 2f;         // Kecepatan gerak Portal
    [SerializeField] private float rotateSpeed = 100f; // Kecepatan rotasi Portal
    private LevelManager levelManager;                 // Referensi ke LevelManager
    private Vector2 newPosition;                       // Posisi tujuan baru yang acak

    void Start()
    {
        // Inisialisasi levelManager dan tetapkan posisi tujuan pertama
        levelManager = FindObjectOfType<LevelManager>();
        ChangePosition();
    }

    void Update()
    {
        // Mengecek jarak antara posisi saat ini dengan posisi tujuan
        float distanceToNewPosition = Vector3.Distance(transform.position, newPosition);
        if (distanceToNewPosition < 0.5f)
        {
            // Jika jarak kurang dari 0.5, pilih posisi tujuan baru
            ChangePosition();
        }

        // Gerakkan portal secara bertahap menuju posisi tujuan dengan Lerp untuk pergerakan halus
        transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);

        // Rotasi portal
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mengecek apakah portal bertabrakan dengan Player
        if (other.CompareTag("Player"))
        {
            // Memuat scene "Main" menggunakan LevelManager
            levelManager.LoadScene("Main");
        }
    }

    private void ChangePosition()
    {
        // Memilih posisi acak baru dalam batas tertentu
        newPosition = new Vector3(
            Random.Range(-10f, 10f),   // Rentang X
            Random.Range(-5f, 5f),     // Rentang Y
            0f                         // Rentang Z tetap 0 untuk 2D
        );
    }
}


