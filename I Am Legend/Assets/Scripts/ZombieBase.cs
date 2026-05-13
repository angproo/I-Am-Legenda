using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    public int threatValue = 5;

    public void Die()
    {
        // cuando se muere se busca el maanger y suma el punto
        FindObjectOfType<ThreatManager>().AddThreat(threatValue);
        Destroy(gameObject);
    }

    
    private void OnMouseDown()
    {
        Die();
    }
}
