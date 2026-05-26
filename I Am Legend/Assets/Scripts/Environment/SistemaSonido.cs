using UnityEngine;

public class SistemaSonido : MonoBehaviour
{
    private AudioSource musicaFondo;

    private void Awake()
    {
        musicaFondo = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Nos sintonizamos al canal de Action
        ThreatManager.OnThreatChangedAction += AjustarMusica;
    }

    private void OnDisable()
    {
        ThreatManager.OnThreatChangedAction -= AjustarMusica;
    }

    private void AjustarMusica(int threat)
    {
        if (musicaFondo == null) return;

        // Reacciones de sonido según tu esquema técnico:
        if (threat >= 76) musicaFondo.pitch = 1.4f;      // Caos sonoro constante
        else if (threat >= 51) musicaFondo.pitch = 1.2f; // Música intensa y sustos
        else if (threat >= 26) musicaFondo.pitch = 1.05f;// Música ligera tensión
        else musicaFondo.pitch = 1.0f;                   // Ambiente tranquilo
    }
}
