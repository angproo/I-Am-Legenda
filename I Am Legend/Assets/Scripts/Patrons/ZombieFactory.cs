using UnityEngine;

public class ZombieFactory : MonoBehaviour, IThreatObserver
{
    public GameObject zombieBasePrefab; 
    public SpawnTableSO tablaDeSpawn; // Vinculación con tu ScriptableObject tabla
    
    private ZombieData zombieActualConfigurado; 
    private Transform playerTransform;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); 
        if (playerObj != null) playerTransform = playerObj.transform;

        // Se registra automáticamente con el gestor de amenazas
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }

    // Este método cambia el tipo de zombie cuando mueves el slider
    public void OnThreatChanged(int currentThreat)
    {
        if (tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(currentThreat);
        }
    }

    public ZombieData GetZombieActual()
    {
        // Si por alguna razón arranca vacío, le da el del nivel 0 por seguridad
        if (zombieActualConfigurado == null && tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(0);
        }
        return zombieActualConfigurado;
    }

    public GameObject CreateZombie(ZombieData data, Vector3 position, int id, SpawnManager manager)
    {
        if (data == null) return null;

        GameObject newZombie = Instantiate(zombieBasePrefab, position, Quaternion.identity);
        
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