using UnityEngine;


[CreateAssetMenu(fileName = "NuevoZombieData", menuName = "Zombies/Zombie Data")]
public class ZombieData : ScriptableObject
{
   public string zombieName;
   public GameObject prefabVisual;
   public int threatValue = 5;

}
