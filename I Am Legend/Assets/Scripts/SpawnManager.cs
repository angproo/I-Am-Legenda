using UnityEngine;

public class SpawnManager : MonoBehaviour, IThreatObserver
{
    public float spawnRate = 2.0f;

    public void OnThreatChanged(int currentThreat)
    {
        // cuando suba la amenaza sera mas dificil el juego
        spawnRate = 2.0f - (currentThreat * 0.015f); 
        Debug.Log("Nueva tasa de spawn: " + spawnRate);
    }
    
    private void Start()
    {
        ThreatManager tm = FindObjectOfType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }
}
