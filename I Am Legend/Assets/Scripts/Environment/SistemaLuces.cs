using UnityEngine;

public class SistemaLuces : MonoBehaviour
{
    private Light luzGlobal; // Si es 2D usás UnityEngine.Rendering.Universal.Light2D

    private void Awake()
    {
        luzGlobal = GetComponent<Light>();
    }

    private void OnEnable()
    {
        // Nos sintonizamos al canal de Action
        ThreatManager.OnThreatChangedAction += AjustarIluminacion;
    }

    private void OnDisable()
    {
        // Nos desvinculamos
        ThreatManager.OnThreatChangedAction -= AjustarIluminacion;
    }

    private void AjustarIluminacion(int threat)
    {
        if (luzGlobal == null) return;

        // Reacciones según tu esquema técnico:
        if (threat >= 76) // Oscuridad extrema
        {
            luzGlobal.intensity = 0.1f;
            luzGlobal.color = Color.red;
        }
        else if (threat >= 51) // Parpadeos y sombras
        {
            luzGlobal.intensity = 0.4f;
            luzGlobal.color = Color.gray;
        }
        else if (threat >= 26) // Luces tenues
        {
            luzGlobal.intensity = 0.7f;
            luzGlobal.color = Color.white;
        }
        else // Luces normales
        {
            luzGlobal.intensity = 1.0f;
            luzGlobal.color = Color.white;
        }
    }
}
