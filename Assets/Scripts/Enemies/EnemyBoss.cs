using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : EnemyHorizontal
{
    public Bullet bullet;
    public Transform bulletSpawnPoint;
    public float shootInterval = 2f;

    private float shootTimer;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
}