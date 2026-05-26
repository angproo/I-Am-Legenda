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
    
    // REDUCIMOS los valores por defecto para que el juego base sea MÁS FÁCIL
    public int threatPerZombie = 2;       // Antes era 5, ahora cada zombie asusta menos
    public float threatPerMinute = 4f;    // Antes era 10, ahora el tiempo corre a tu favor
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
        ZombieBase[] activeZombies = FindObjectsByType<ZombieBase>(FindObjectsSortMode.None);
        int zombieThreat = activeZombies.Length * threatPerZombie;

        float minutesPassed = gameElapsedTime / 60f;
        int timeThreat = Mathf.FloorToInt(minutesPassed * threatPerMinute);

        int newThreat = zombieThreat + timeThreat;
        newThreat = Mathf.Clamp(newThreat, 0, 100);

        // Solo actualizamos si el cálculo natural es mayor al valor actual (para no pisar el slider)
        if (newThreat > currentThreat)
        {
            currentThreat = newThreat;
            NotifyObservers();
        }
    }

    // Esta función la llamará el Slider desde la UI para forzar el caos
    public void SetThreatManually(float valorSlider)
    {
        // Convertimos el float del slider (0.0 a 100.0) a un entero redondo (0 a 100)
        int nuevoMiedo = Mathf.RoundToInt(valorSlider);
    
        // Nos aseguramos de que no se pase de los límites
        nuevoMiedo = Mathf.Clamp(nuevoMiedo, 0, 100);

        if (currentThreat != nuevoMiedo)
        {
            currentThreat = nuevoMiedo;
            NotifyObservers(); // Le avisa a las luces, cámara, sonido, etc.
        }
    }

    private void NotifyObservers()
    {
        foreach (IThreatObserver observer in observers) observer.OnThreatChanged(currentThreat);
        OnThreatChangedAction?.Invoke(currentThreat);
    }
}
