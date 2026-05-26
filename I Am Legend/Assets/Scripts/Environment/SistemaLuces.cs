using UnityEngine;

public class SistemaLuces : MonoBehaviour
{
    private Light luzGlobal; // Si usás 2D: UnityEngine.Rendering.Universal.Light2D

    private void Awake()
    {
        luzGlobal = GetComponent<Light>();
    }

    private void OnEnable()
    {
        ThreatManager.OnThreatChangedAction += ActualizarLuces;
    }

    private void OnDisable()
    {
        ThreatManager.OnThreatChangedAction -= ActualizarLuces;
    }

    private void ActualizarLuces(int threat)
    {
        if (luzGlobal == null) return;

        float t = threat / 100f;

        // La intensidad baja del 100% (1.0) al 15% (0.15) según sube el slider
        luzGlobal.intensity = Mathf.Lerp(1.0f, 0.15f, t);

        // El color transiciona de blanco puro a un rojo oscuro de horda/alerta
        luzGlobal.color = Color.Lerp(Color.white, new Color(0.5f, 0.0f, 0.0f), t);
    }
}
