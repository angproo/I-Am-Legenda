using UnityEngine;
using UnityEngine.UI;

public class ControladorSlider : MonoBehaviour
{
    private Slider slider;
    private ThreatManager tm;
    private CameraFollow camFollow;

    private void Start()
    {
        slider = GetComponent<Slider>();
        tm = FindFirstObjectByType<ThreatManager>();
        camFollow = FindFirstObjectByType<CameraFollow>();

        if (slider != null)
        {
            // Forzamos los límites de la barra en la interfaz
            slider.minValue = 0f;
            slider.maxValue = 100f;
            
            // Escuchamos el evento cuando el jugador arrastra la barra con el mouse
            slider.onValueChanged.AddListener(AlCambiarSlider);
        }
    }

    private void AlCambiarSlider(float valor)
    {
        // 1. Le mandamos el valor al ThreatManager para los enemigos y el texto de UI
        if (tm != null)
        {
            tm.SetThreatManually(valor);
        }

        // 2. Le mandamos el valor directo a la cámara para que haga el shake y el zoom manual
        if (camFollow != null)
        {
            camFollow.ActualizarEfectosPorSlider(valor);
        }
    }
}
