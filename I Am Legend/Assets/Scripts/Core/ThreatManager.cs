using UnityEngine;
using System;
using System.Collections.Generic;

public class ThreatManager : MonoBehaviour 
{
    [SerializeField] private int currentThreat = 0;
    public int CurrentThreat => currentThreat;

   
    private List<IThreatObserver> observers = new List<IThreatObserver>();

    public void RegisterObserver(IThreatObserver observer) 
    {
        if (!observers.Contains(observer)) observers.Add(observer);
    }

    public void UnregisterObserver(IThreatObserver observer) 
    {
        observers.Remove(observer);
    }

    public void SetThreatManually(float value) 
    {
        int newThreat = Mathf.RoundToInt(value);
        if (currentThreat != newThreat) 
        {
            currentThreat = Mathf.Clamp(newThreat, 0, 100);
            NotifyObservers();
        }
    }

    private void NotifyObservers() 
    {
        foreach (IThreatObserver observer in observers) 
        {
            observer.OnThreatChanged(currentThreat);
        }
       
    }
}
