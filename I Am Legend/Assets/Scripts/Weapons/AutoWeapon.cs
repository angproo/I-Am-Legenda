using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float fireRange = 7f;
    private float fireTimer;

    private int killCount = 0;
    private ThreatManager threatManager;
    private IShootStrategy currentStrategy;

    private void Start()
    {
        threatManager = FindFirstObjectByType<ThreatManager>();
        currentStrategy = new SimpleShootStrategy();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                EvaluateStrategy();
                Shoot(target);
                fireTimer = 0f;
            }
        }
    }

    public void RegisterKill()
    {
        killCount++;
    }

    private void EvaluateStrategy()
    {
        if (threatManager == null || threatManager.CurrentThreat < 100)
        {
            currentStrategy = new SimpleShootStrategy();
        }
        else
        {
            currentStrategy = new BurstShootStrategy(killCount);
        }
    }

    private void Shoot(Transform target)
    {
        // Le pasamos "this" (el script AutoWeapon actual) como el componente MonoBehaviour (caller)
        currentStrategy.ExecuteShoot(target, this, this.transform, bulletPrefab);
    }

    private Transform FindClosestEnemy()
    {
        ZombieBase[] enemies = FindObjectsByType<ZombieBase>(FindObjectsSortMode.None);
        Transform closestEnemy = null;
        float closestDistance = fireRange;

        foreach (ZombieBase enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }
}
