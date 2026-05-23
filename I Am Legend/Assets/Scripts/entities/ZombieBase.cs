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
            Destroy(collision.gameObject); // Destruye la bala
            Die();
        }
    }

    private void Die()
    {
        
        
        Destroy(gameObject);
    }
}
