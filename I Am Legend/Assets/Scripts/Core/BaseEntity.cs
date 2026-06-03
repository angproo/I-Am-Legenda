using UnityEngine;
public abstract class BaseEntity : MonoBehaviour 
{
    [Header("Base Entity Stats")]
    public float health = 100f;
    public string entityName;

   
    public bool IsAlive => health > 0;
    
    public abstract void Die();

    
    public Transform FindChildRecursive(Transform parent, string nameToFind) 
    {
        if (parent.name == nameToFind) return parent;

        foreach (Transform child in parent) 
        {
            Transform result = FindChildRecursive(child, nameToFind);
            if (result != null) return result;
        }
        return null;
    }
}