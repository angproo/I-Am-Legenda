using UnityEngine;

public class ZombieFactory : MonoBehaviour
{
    public GameObject zombieBasePrefab; 

    public GameObject CreateZombie(ZombieData data, Vector3 position)
    {
       
        GameObject newZombie = Instantiate(zombieBasePrefab, position, Quaternion.identity);
        
       
        if (data.prefabVisual != null)
        {
            Instantiate(data.prefabVisual, newZombie.transform);
        }

      
        ZombieBase zombieScript = newZombie.GetComponent<ZombieBase>();
        if (zombieScript != null)
        {
            zombieScript.Initialize(data);
        }

        return newZombie;
    }
}
