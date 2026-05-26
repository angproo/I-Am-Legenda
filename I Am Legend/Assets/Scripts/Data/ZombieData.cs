using UnityEngine;

[CreateAssetMenu(fileName = "NuevoZombieData", menuName = "Zombies/Zombie Data")]
public class ZombieData : ScriptableObject
{
   public string zombieName;
   public GameObject prefabVisual;
   public int threatValue = 5;
   public float speed = 2f; // <-- ¡Faltaba agregar esta línea para que compile!
}