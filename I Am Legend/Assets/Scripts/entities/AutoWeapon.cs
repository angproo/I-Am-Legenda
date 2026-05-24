using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
 public GameObject bulletPrefab;
    public float fireRate = 0.2f; 
    public float fireRange = 7f;
    private float fireTimer;

    private int killCount = 0;
    private ThreatManager threatManager;

    private void Start()
    {
        threatManager = FindFirstObjectByType<ThreatManager>();
    }

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

    public void RegisterKill()
    {
        killCount++;
    }

    private void Shoot(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // CASO 1: Si el slider NO está al máximo (100%), ignoramos los power-ups y solo tiramos 1 bala
        if (threatManager == null || threatManager.CurrentThreat < 100)
        {
            InstanciarBala(baseAngle, directionToTarget);
            return; // Cortamos la función acá
        }

        // CASO 2: ¡El Slider está al máximo! Desbloqueamos los Power-Ups según tus bajas acumuladas
        
        // Bala Principal (Siempre dispara al enemigo)
        InstanciarBala(baseAngle, directionToTarget);

        // Si tenés 5 bajas o más, se suma la segunda bala (Hacia atrás, 180° de diferencia)
        if (killCount >= 5)
        {
            float angleBack = baseAngle + 180f;
            Vector2 dirBack = CalcularDireccionPorAngulo(angleBack);
            InstanciarBala(angleBack, dirBack);
        }

        // Si tenés 10 bajas o más, se suma la tercera bala (Hacia arriba relativo, 90° de diferencia)
        if (killCount >= 10)
        {
            float angleUp = baseAngle + 90f;
            Vector2 dirUp = CalcularDireccionPorAngulo(angleUp);
            InstanciarBala(angleUp, dirUp);
        }

        // Si tenés 15 bajas o más, se suma la cuarta bala (Hacia abajo relativo, -90° de diferencia)
        if (killCount >= 15)
        {
            float angleDown = baseAngle - 90f;
            Vector2 dirDown = CalcularDireccionPorAngulo(angleDown);
            InstanciarBala(angleDown, dirDown);
        }
    }

    private void InstanciarBala(float angulo, Vector2 direccion)
    {
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * 15f;
        }
    }

    private Vector2 CalcularDireccionPorAngulo(float anguloEnGrados)
    {
        return new Vector2(Mathf.Cos(anguloEnGrados * Mathf.Deg2Rad), Mathf.Sin(anguloEnGrados * Mathf.Deg2Rad));
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
