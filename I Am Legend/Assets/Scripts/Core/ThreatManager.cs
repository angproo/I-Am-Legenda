using UnityEngine;
using System;
using System.Collections.Generic;

public class ThreatManager : MonoBehaviour
{
   [SerializeField] private int currentThreat = 0;
   public int CurrentThreat => currentThreat;
    
    private List<IThreatObserver> observers = new List<IThreatObserver>();
    public static event Action<int> OnThreatChangedAction;
    
    private float checkTimer;
    public float checkInterval = 0.5f; 
    
    
    public int threatPerZombie = 2;       
    public float threatPerMinute = 4f;   
    private float gameElapsedTime = 0f;

    public void RegisterObserver(IThreatObserver observer) 
    { 
        if (!observers.Contains(observer)) 
        {
            observers.Add(observer); 
        }
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
        if (currentThreat > 0)
        {
            return; 
        }

        // Si el slider está en 0, el juego calcula la amenaza de fondo normalmente
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

  
    public void SetThreatManually(float valorSlider)
    {
       
        int nuevoMiedo = Mathf.RoundToInt(valorSlider);
        nuevoMiedo = Mathf.Clamp(nuevoMiedo, 0, 100);

        if (currentThreat != nuevoMiedo)
        {
            currentThreat = nuevoMiedo;
            NotifyObservers();
        }
    }

    private void NotifyObservers()
    {
        foreach (IThreatObserver observer in observers) observer.OnThreatChanged(currentThreat);
        OnThreatChangedAction?.Invoke(currentThreat);
    }
}
