using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    private ZombieData data;
    private float currentSpeed; // Guardamos la velocidad de forma interna
    private Transform player;

    // Añadimos el ID y el manager para el sistema de tracking que dibujaste
    public int InstanceID { get; private set; }
    private SpawnManager manager;

    public void Initialize(ZombieData zombieData, int id, SpawnManager spawnManager, Transform playerTransform)
    {
        data = zombieData;
        currentSpeed = zombieData.speed; // <-- Aquí es donde Unity se quejaba
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
        // Le avisa al manager que se borre de la lista usando su ID único antes de destruirse
        if (manager != null)
        {
            manager.RemoveById(InstanceID);
        }
        
        Destroy(gameObject);
    }
}