using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour, IThreatObserver
{
    [Header("Referencias de Fábrica")]
    public ZombieFactory factory;
    
    [Header("Configuración de Ritmo (Spawn)")]
    public float baseSpawnRate = 2.0f;
    private float currentSpawnRate;
    private float timer;
    private Camera mainCamera;

 
    private List<ZombieBase> misId = new List<ZombieBase>();
    private int contadorId = 0;

    
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

       
        int nuevoId = contadorId++; 
        
      
        ZombieData dataParaSpawnear = factory.GetZombieActual();

        if (dataParaSpawnear != null)
        {
           
            GameObject go = factory.CreateZombie(dataParaSpawnear, spawnPosition, nuevoId, this);
            
            if (go != null)
            {
                ZombieBase scriptZombie = go.GetComponent<ZombieBase>();
                if (scriptZombie != null)
                {
                   
                    misId.Add(scriptZombie); 
                }
            }
        }
    }

  
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
       
        currentSpawnRate = Mathf.Max(0.2f, baseSpawnRate - (currentThreat * 0.02f));
    }
}
