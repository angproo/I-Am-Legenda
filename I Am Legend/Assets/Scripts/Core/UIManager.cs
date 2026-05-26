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

    // Se ejecuta automáticamente cada vez que el ThreatManager cambia su valor
    public void OnThreatChanged(int currentThreat)
    {
        // 1. Actualiza el texto decorativo
        if (threatText != null)
        {
            threatText.text = $"AMENAZA: {currentThreat}%";
        }

        // 2. ¡AQUÍ ESTÁ LA SOLUCIÓN! Le asignamos el valor de la amenaza al Slider
        if (sliderMiedo != null)
        {
            sliderMiedo.value = currentThreat; 
        }
    }
}