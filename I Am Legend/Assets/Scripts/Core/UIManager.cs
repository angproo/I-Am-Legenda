using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour, IThreatObserver
{
    public TextMeshProUGUI threatText;

    private void Start()
    {
        // REGISTRO CLÁSICO: Se anota en la lista de interfaces del ThreatManager
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) 
        {
            tm.RegisterObserver(this);
        }
    }

    // El método obligatorio de la interfaz que actualiza el Slider/Texto del Miedo
    public void OnThreatChanged(int currentThreat)
    {
        if (threatText != null)
        {
            threatText.text = $"AMENAZA: {currentThreat}%";
        }
    }
}
