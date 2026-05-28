using UnityEngine;
using UnityEngine.UI;

public class ControladorSlider : MonoBehaviour
{
    private Slider slider;
    private ThreatManager tm;
    private CameraFollow camFollow;
    private SistemaLuces sisLuces;
    private SistemaSonido sisSonido;

    private void Start()
    {
        slider = GetComponent<Slider>();
        tm = FindFirstObjectByType<ThreatManager>();
        camFollow = FindFirstObjectByType<CameraFollow>();
        sisLuces = FindFirstObjectByType<SistemaLuces>();
        sisSonido = FindFirstObjectByType<SistemaSonido>(); 

        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 100f;
            slider.onValueChanged.AddListener(AlCambiarSlider);
        }
    }

    private void AlCambiarSlider(float valor)
    {
        // (Spawner, UI de texto)
        if (tm != null)
        {
            tm.SetThreatManually(valor);
        }

        // Control Visual 
        if (camFollow != null)
        {
            camFollow.ActualizarEfectosPorSlider(valor);
        }

        // Control Visual 
        if (sisLuces != null)
        {
            sisLuces.ActualizarLuces(valor); 
        }

        // Control Auditivo
        if (sisSonido != null)
        {
            sisSonido.AjustarMusicaPorSlider(valor);
        }
    }
}
