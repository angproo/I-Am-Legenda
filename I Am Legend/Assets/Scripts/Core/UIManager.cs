using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IThreatObserver
{
    public TextMeshProUGUI threatText;
    
    [Tooltip("Arrastra aquí el Slider de la interfaz")]
    public Slider sliderMiedo; 

    private void Start()
    {
        // Registro clásico en el ThreatManager
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) 
        {
            tm.RegisterObserver(this);
        }

        // Inicializamos los límites del slider
        if (sliderMiedo != null)
        {
            sliderMiedo.minValue = 0f;
            sliderMiedo.maxValue = 100f;
            sliderMiedo.value = 0f;
        }
    }

    // El método de la interfaz ahora SOLO actualiza el texto decorativo
    public void OnThreatChanged(int currentThreat)
    {
        // El texto te sigue mostrando el nivel actual del juego
        if (threatText != null)
        {
            threatText.text = $"AMENAZA: {currentThreat}%";
        }

    }
}
