using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SistemaLuces : MonoBehaviour
{
    public Light2D globalLight;

    public void ActualizarLuces(float valor) {
        if (globalLight == null) return;
        
        float t = valor / 100f;
        globalLight.intensity = Mathf.Lerp(1.0f, 0.4f, t);
        globalLight.color = Color.Lerp(Color.white, new Color(1f, 0.5f, 0.5f), t);
    }
}
