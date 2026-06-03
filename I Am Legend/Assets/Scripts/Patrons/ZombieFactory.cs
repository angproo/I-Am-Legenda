using UnityEngine;

public class ZombieFactory : MonoBehaviour, IThreatObserver
{
   [Header("Referencias de Prefabs")]
    public GameObject zombieBasePrefab; 
    public SpawnTableSO tablaDeSpawn; 
    
    private ZombieData zombieActualConfigurado; 
    private Transform playerTransform;

    private void Start()
    {
       
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); 
        if (playerObj != null) playerTransform = playerObj.transform;

       
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }

   
    public void OnThreatChanged(int currentThreat)
    {
        if (tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(currentThreat);
        }
    }

    public ZombieData GetZombieActual()
    {
       
        if (zombieActualConfigurado == null && tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(0);
        }
        return zombieActualConfigurado;
    }
    
    public GameObject CreateZombie(ZombieData data, Vector3 position, int id, SpawnManager manager)
    {
        if (data == null) return null;

      
        ZombieBuilder builder = new ZombieBuilder(zombieBasePrefab, position);
        
        GameObject newZombie = builder
            .SetSpeed(Random.Range(1.5f, 3.5f)) 
            .SetName("Zombie_" + data.zombieName) 
            .Build();
        
       
        if (data.prefabVisual != null)
        {
            Instantiate(data.prefabVisual, newZombie.transform);
        }

      
        ZombieBase zombieScript = newZombie.GetComponent<ZombieBase>();
        if (zombieScript != null)
        {
           
            zombieScript.Initialize(data, id, manager, playerTransform);
        }

        return newZombie;
    }
}
