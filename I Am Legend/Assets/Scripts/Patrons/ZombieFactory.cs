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

        // Nos registramos para escuchar al Gestor de Miedo
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }

    // Cada vez que cambia el slider, este método elige el zombie de la tabla automáticamente
    public void OnThreatChanged(int currentThreat)
    {
        if (tablaDeSpawn != null)
        {
            zombieActualConfigurado = tablaDeSpawn.ObtenerZombiePorMiedo(currentThreat);
        }
    }

    public ZombieData GetZombieActual()
    {
        // Si por algún motivo empieza en 0 y no ha cambiado, le damos un valor por defecto seguro
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
        
        // Aquí se le pega el gráfico/arte diferente de cada monstruo
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