using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour, IThreatObserver
{
    public ZombieFactory factory;
    
    public float baseSpawnRate = 2.0f;
    private float currentSpawnRate;
    private float timer;
    private Camera mainCamera;

    // CONTROL DE IDs (Tu lista de tracking única)
    private List<ZombieBase> misId = new List<ZombieBase>();
    private int contadorId = 0;

    // Propiedad para obtener la cantidad de elementos activos (Reemplaza al misId.length())
    public int MisId
    {
        get { return misId.Count; }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        currentSpawnRate = baseSpawnRate;

        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnRate)
        {
            SpawnZombieInCamera();
            timer = 0;
        }
    }

    private void SpawnZombieInCamera()
    {
        float edgeOffset = 0.1f;
        float randomX = Random.Range(0, 2) == 0 ? -edgeOffset : 1.0f + edgeOffset;
        float randomY = Random.Range(-edgeOffset, 1.0f + edgeOffset);

        if (Random.Range(0, 2) == 0)
        {
            randomX = Random.Range(-edgeOffset, 1.0f + edgeOffset);
            randomY = Random.Range(0, 2) == 0 ? -edgeOffset : 1.0f + edgeOffset;
        }

        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, Mathf.Abs(mainCamera.transform.position.z)));
        spawnPosition.z = 0;

        int nuevoId = contadorId++; // Generamos tu ID único incremental
        ZombieData dataParaSpawnear = factory.GetZombieActual();

        if (dataParaSpawnear != null)
        {
            // Creamos el zombie pasándole su ID y la referencia de este manager
            GameObject go = factory.CreateZombie(dataParaSpawnear, spawnPosition, nuevoId, this);
            
            if (go != null)
            {
                ZombieBase scriptZombie = go.GetComponent<ZombieBase>();
                if (scriptZombie != null)
                {
                    misId.Add(scriptZombie); // Lo anotamos en la lista de tracking
                }
            }
        }
    }

    // ELIMINAR POR ID: El método con el bucle foreach de tu dibujo
    public void RemoveById(int id)
    {
        ZombieBase encontrado = null;

        foreach (ZombieBase zb in misId)
        {
            if (zb != null && zb.InstanceID == id)
            {
                encontrado = zb;
                break;
            }
        }

        if (encontrado != null)
        {
            misId.Remove(encontrado);
        }
    }

    public void OnThreatChanged(int currentThreat)
    {
        // A mayor amenaza, menor tiempo entre spawns
        currentSpawnRate = Mathf.Max(0.2f, baseSpawnRate - (currentThreat * 0.02f));
    }
}