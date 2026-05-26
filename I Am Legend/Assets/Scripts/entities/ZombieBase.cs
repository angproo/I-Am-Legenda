using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    private ZombieData data;
    private float currentSpeed;
    private Transform player;

    // Control de tracking e ID único de tu diagrama manual
    public int InstanceID { get; private set; }
    private SpawnManager manager;

    public void Initialize(ZombieData zombieData, int id, SpawnManager spawnManager, Transform playerTransform)
    {
        data = zombieData;
        currentSpeed = zombieData.speed; 
        InstanceID = id;
        manager = spawnManager;
        player = playerTransform;
    }

    private void Update()
    { 
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destruye la bala
            Die();
        }
    }

    private void Die()
    {
        // Se borra de la lista del Manager usando su ID único antes de ser destruido
        if (manager != null)
        {
            manager.RemoveById(InstanceID);
        }
        Destroy(gameObject);
    }
}