using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Daftar spawner di scene
    public float timer = 0; // Waktu untuk mengatur wave
    [SerializeField] private float waveInterval = 5f; // Interval waktu antar wave
    public int waveNumber = 1; // Nomor wave saat ini
    public int totalEnemies = 0; // Total musuh yang akan muncul di wave saat ini

    private bool isWaveActive = false; // Apakah wave sedang berjalan

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (!isWaveActive)
        {
            timer += Time.deltaTime;

            // Mulai wave baru jika interval tercapai
            if (timer >= waveInterval)
            {
                timer = 0;
                StartWave();
            }
        }
    }

    // Memulai wave baru
    private void StartWave()
    {
        isWaveActive = true;
        totalEnemies = CalculateTotalEnemies();

        Debug.Log($"Wave {waveNumber} dimulai! Total musuh: {totalEnemies}");

        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.defaultSpawnCount = waveNumber; // Menyesuaikan jumlah musuh per spawner
            spawner.StartSpawning();
        }

        StartCoroutine(CheckWaveCompletion());
    }

    // Menghentikan wave yang sedang berjalan
    private void EndWave()
    {
        isWaveActive = false;
        waveNumber++;

        Debug.Log($"Wave {waveNumber - 1} selesai!");
    }

    // Menghitung total musuh berdasarkan jumlah spawner dan wave number
    private int CalculateTotalEnemies()
    {
        int total = 0;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            total += spawner.defaultSpawnCount + spawner.spawnCountMultiplier;
        }
        return total;
    }

    // Coroutine untuk memonitor penyelesaian wave
    private IEnumerator CheckWaveCompletion()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int remainingEnemies = 0;

            // Hitung musuh yang masih ada di semua spawner
            foreach (EnemySpawner spawner in enemySpawners)
            {
                remainingEnemies += spawner.spawnCount;
            }

            if (remainingEnemies <= 0)
            {
                EndWave();
                break;
            }
        }
    }

    // Fungsi untuk mencatat kill dan mengurangi total musuh
    public void RegisterKill()
    {
        totalEnemies--;
        Debug.Log($"Enemy terbunuh! Sisa musuh: {totalEnemies}");

        // Jika semua musuh terbunuh, hentikan wave
        if (totalEnemies <= 0 && isWaveActive)
        {
            StopAllCoroutines();
            EndWave();
        }
    }
}

