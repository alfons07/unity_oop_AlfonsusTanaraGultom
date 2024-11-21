using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; // Prefab enemy yang akan di-spawn

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; // Jumlah kill minimum untuk meningkatkan spawn count
    public int totalKill = 0; // Total kill secara global
    private int totalKillWave = 0; // Kill yang dihitung per wave

    [SerializeField] private float spawnInterval = 3f; // Interval waktu antar spawn

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Jumlah enemy yang aktif dalam wave saat ini
    public int defaultSpawnCount = 1; // Spawn count default di awal permainan
    public int spawnCountMultiplier = 1; // Faktor pengali jumlah enemy setiap wave baru
    public int multiplierIncreaseCount = 1; // Tambahan multiplier setiap wave baru

    public CombatManager combatManager; // Referensi ke CombatManager untuk mengontrol status
    public bool isSpawning = false; // Flag untuk menentukan apakah spawner sedang aktif

    private void Start()
    {
        StartSpawning();
    }

    // Memulai proses spawning
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    // Menghentikan proses spawning
    public void StopSpawning()
    {
        isSpawning = false;
        StopCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            for (int i = 0; i < defaultSpawnCount + spawnCountMultiplier; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitUntil(() => totalKillWave >= defaultSpawnCount + spawnCountMultiplier);

            // Setelah wave selesai, perbarui data wave
            totalKillWave = 0;
            spawnCountMultiplier += multiplierIncreaseCount;
        }
    }

    // Fungsi untuk melakukan spawn Enemy
    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
            spawnCount++;
        }
        else
        {
            Debug.LogWarning("SpawnedEnemy prefab belum diatur!");
        }
    }

    // Fungsi untuk mencatat kill
    public void RegisterKill()
    {
        totalKill++;
        totalKillWave++;

        // Cek apakah jumlah kill memenuhi syarat untuk meningkatkan spawn count
        if (totalKill % minimumKillsToIncreaseSpawnCount == 0)
        {
            spawnCountMultiplier++;
        }

        // Hapus enemy jika semua wave selesai
        spawnCount--;
    }
}
