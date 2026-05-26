using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, IThreatObserver
{
    public ZombieFactory factory;
    public ZombieData zombieDataNormal; 
    
    public float baseSpawnRate = 2.0f;
    private float currentSpawnRate;
    private float timer;
    private Camera mainCamera;
    public static SpawnManager Instance { get; private set; }

    private List<int> ListZombie; 
    public int ListZombieCount => ListZombie.Count;
    private void Start()
    {
        mainCamera = Camera.main;
        currentSpawnRate = baseSpawnRate;

        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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

        GameObject go = factory.CreateZombie(zombieDataNormal, spawnPosition);
        ListZombie.Add(go.GetInstanceID());
    }

    public void OnThreatChanged(int currentThreat)
    {
        
        currentSpawnRate = Mathf.Max(0.2f, baseSpawnRate - (currentThreat * 0.02f));
    }
}   
