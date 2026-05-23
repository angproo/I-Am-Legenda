using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
   public GameObject bulletPrefab;
    [Tooltip("Tiempo en segundos entre disparos. Menos valor = más rápido.")]
    public float fireRate = 0.2f; 
    public float fireRange = 7f;
    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                Shoot(target);
                fireTimer = 0f;
            }
        }
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

    private void Shoot(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;

      
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
       
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle));

       
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
        
     
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * 15f;
        }
    }
}
