using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour, IThreatObserver
{
    public TextMeshProUGUI threatText;

    public void OnThreatChanged(int currentThreat)
    {
       
        threatText.text = $"AMENAZA: {currentThreat}%";
    }

    private void Start()
    {
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) tm.RegisterObserver(this);
    }
}
