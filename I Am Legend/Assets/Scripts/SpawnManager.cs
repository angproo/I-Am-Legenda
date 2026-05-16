using UnityEngine;

public class SpawnManager : MonoBehaviour, IThreatObserver
{
    public ZombieFactory factory;
    public ZombieData zombieDataNormal; 
    
    public float baseSpawnRate = 2.0f;
    private float currentSpawnRate;
    private float timer;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        currentSpawnRate = baseSpawnRate;

        ThreatManager tm = FindObjectOfType<ThreatManager>();
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
        float margin = 0.1f;
        float randomX = Random.Range(margin, 1.0f - margin);
        float randomY = Random.Range(margin, 1.0f - margin);

        
        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, Mathf.Abs(mainCamera.transform.position.z)));
        spawnPosition.z = 0; 

       
        factory.CreateZombie(zombieDataNormal, spawnPosition);
    }

    public void OnThreatChanged(int currentThreat)
    {
        
        currentSpawnRate = Mathf.Max(0.4f, baseSpawnRate - (currentThreat * 0.02f));
    }
}
