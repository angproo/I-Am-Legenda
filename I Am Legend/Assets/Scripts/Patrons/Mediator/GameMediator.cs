using UnityEngine;

public class GameMediator : MonoBehaviour
{
    public ThreatManager threatManager;
    public SistemaLuces luces;
    public SistemaSonido sonido;
    public CameraFollow cam;

    public void NotifyThreatUpdate(float value) 
    {
      
        if (threatManager) threatManager.SetThreatManually(value);
        if (luces) luces.ActualizarLuces(value); 
        if (sonido) sonido.AjustarMusicaPorSlider(value); 
        if (cam) cam.ActualizarEfectosPorSlider(value); 
        
        
        ZombieBase[] zombies = FindObjectsByType<ZombieBase>(FindObjectsSortMode.None);
        foreach (var z in zombies) z.ApplyThreatColor(value);
    }
}
