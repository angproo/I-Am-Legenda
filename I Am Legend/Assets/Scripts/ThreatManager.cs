using UnityEngine;
using System.Collections.Generic;

public class ThreatManager : MonoBehaviour
{
    [SerializeField] private int currentThreat = 0;
    private List<IThreatObserver> observers = new List<IThreatObserver>();

   
    public void RegisterObserver(IThreatObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

   
    public void UnregisterObserver(IThreatObserver observer)
    {
        observers.Remove(observer);
    }

    public void AddThreat(int amount)
    {
        currentThreat = Mathf.Clamp(currentThreat + amount, 0, 100);
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (IThreatObserver observer in observers)
        {
            observer.OnThreatChanged(currentThreat);
        }
    }
}
