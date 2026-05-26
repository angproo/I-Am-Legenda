using UnityEngine;

public class ZombieFactory : MonoBehaviour, IThreatObserver
{
    public GameObject zombieBasePrefab; 
    public SpawnTableSO tablaDeSpawn; 
    
    private ZombieData zombieActualConfigurado; 
    private Transform playerTransform;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); 
        if (playerObj != null) playerTransform = playerObj.transform;

        // Registro automático en el ThreatManager
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }

    // Al cambiar la amenaza, selecciona el nuevo ZombieData correspondiente
    public void OnThreatChanged(int currentThreat)
    {
        if (tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(currentThreat);
        }
    }

    public ZombieData GetZombieActual()
    {
        // Resguardo de seguridad si se solicita un enemigo al iniciar en 0%
        if (zombieActualConfigurado == null && tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(0);
        }
        return zombieActualConfigurado;
    }

    // Instancia el cascarón físico e inyecta el arte visual adjunto al ScriptableObject
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