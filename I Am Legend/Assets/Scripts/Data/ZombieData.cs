using UnityEngine;


[CreateAssetMenu(fileName = "NuevoZombieData", menuName = "Zombies/Zombie Data")]
public class ZombieData : ScriptableObject
{
   [Header("Configuración Básica")]
   public string zombieName;    
   public GameObject prefabVisual; 

   [Header("Variables de Mecánica")]
   public int threatValue = 5;    
    
  
   public float speed = 2f;        
}
