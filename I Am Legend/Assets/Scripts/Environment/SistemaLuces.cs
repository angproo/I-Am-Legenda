using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SistemaLuces : MonoBehaviour
{
    private Light2D luzGlobal;

    private void Awake()
    {
        luzGlobal = GetComponent<Light2D>();
    }
    

  public void ActualizarLuces(float ValorSlider)
    {
        if (luzGlobal == null) return;

        float t = ValorSlider / 100f;

     
        luzGlobal.intensity = Mathf.Lerp(1.0f, 0.15f, t);

       
        luzGlobal.color = Color.Lerp(Color.white, new Color(0.5f, 0.0f, 0.0f), t);
    }
}
