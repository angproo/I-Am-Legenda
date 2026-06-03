using UnityEngine;

public class SistemaSonido : MonoBehaviour
{
    private AudioSource source;

    void Awake() { source = GetComponent<AudioSource>(); }

    public void AjustarMusicaPorSlider(float valor) 
    {
        if (source == null) return;
        
      
        float t = valor / 100f;
        source.pitch = Mathf.Lerp(1f, 1.5f, t);
    }
}
