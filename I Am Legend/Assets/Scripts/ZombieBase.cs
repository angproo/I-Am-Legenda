using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    private ZombieData data;

   
    public void Initialize(ZombieData zombieData)
    {
        data = zombieData;
    }

  
    private void OnMouseDown()
    {
        Die();
    }

    private void Die()
    {
        ThreatManager tm = FindObjectOfType<ThreatManager>();
        if (tm != null && data != null)
        {
            tm.AddThreat(data.threatValue);
        }

        Destroy(gameObject); 
    }
}
