using UnityEngine;
using System;
using System.Collections.Generic;

public class ThreatManager : MonoBehaviour
{
    [SerializeField] private int currentThreat = 0;
    
    // --- CANAL A: Patrón Observer Clásico (Con Interfaz) ---
    private List<IThreatObserver> observers = new List<IThreatObserver>();
    
    // --- CANAL B: Patrón Observer Moderno (Con Action) ---
    public static event Action<int> OnThreatChangedAction;
    
    private float checkTimer;
    public float checkInterval = 0.5f; 
    public int threatPerZombie = 5;    
    private float gameElapsedTime = 0f;
    public float threatPerMinute = 10f; 

    public void RegisterObserver(IThreatObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IThreatObserver observer)
    {
        observers.Remove(observer);
    }

    private void Update()
    {
        gameElapsedTime += Time.deltaTime;
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            CalculateDynamicThreat();
            checkTimer = 0f;
        }
    }

    private void CalculateDynamicThreat()
    {
        ZombieBase[] activeZombies = FindObjectsByType<ZombieBase>(FindObjectsSortMode.None);
        int zombieThreat = activeZombies.Length * threatPerZombie;

        float minutesPassed = gameElapsedTime / 60f;
        int timeThreat = Mathf.FloorToInt(minutesPassed * threatPerMinute);

        int newThreat = zombieThreat + timeThreat;
        newThreat = Mathf.Clamp(newThreat, 0, 100);

        if (newThreat != currentThreat)
        {
            currentThreat = newThreat;
            NotifyObservers(); 
        }
    }

    private void NotifyObservers()
    {
        // 1. Notificación al Canal Clásico (Interfaces)
        foreach (IThreatObserver observer in observers)
        {
            observer.OnThreatChanged(currentThreat);
        }

        // 2. Notificación al Canal Moderno (Action)
        OnThreatChangedAction?.Invoke(currentThreat);
    }
}
