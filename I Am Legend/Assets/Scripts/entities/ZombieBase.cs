using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    private ZombieData data;
    public float speed = 2f;
    private Transform player;

    public void Initialize(ZombieData zombieData)
    {
        data = zombieData;
     
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    private void Update()
    { 
        
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            SpawnImpactEffect(collision.transform.position); // NUEVO
            Destroy(collision.gameObject);
            Die();
        }
    }
    
    private void SpawnImpactEffect(Vector3 position)
    {
        // Crear un pequeño efecto visual de impacto
        GameObject impact = new GameObject("ImpactEffect");
        impact.transform.position = position;
        var particles = impact.AddComponent<ParticleSystem>();
        // Configuración rápida de partículas...
        Object.Destroy(impact, 0.5f);
    }

    private void Die()
    {
        AutoWeapon weapon = FindFirstObjectByType<AutoWeapon>();
        if (weapon != null)
        {
            weapon.RegisterKill(); 
        }

        Destroy(gameObject);
    }
}
