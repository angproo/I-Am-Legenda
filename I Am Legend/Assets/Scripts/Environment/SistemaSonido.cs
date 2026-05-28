using UnityEngine;

public class SistemaSonido : MonoBehaviour
{
    private AudioSource musicaFondo;

    private void Awake()
    {
        musicaFondo = GetComponent<AudioSource>();
    }

    // El slider llamará directamente a esta función pública pasándole su valor float (0 a 100)
    public void AjustarMusicaPorSlider(float valorSlider)
    {
        if (musicaFondo == null) return;

        // Clasificación por rangos idéntica a tu diseño original pero usando el valor físico del slider
        if (valorSlider >= 76f) 
            musicaFondo.pitch = 1.4f;      // Caos sonoro constante (Modo Furia / Máximo)
        else if (valorSlider >= 51f) 
            musicaFondo.pitch = 1.2f; // Música intensa y sustos
        else if (valorSlider >= 26f) 
            musicaFondo.pitch = 1.05f;// Música ligera tensión
        else 
            musicaFondo.pitch = 1.0f;      // Ambiente tranquilo (¡Vuelve a la normalidad al llegar a 0!)
    }
}
