using UnityEngine;

public class ZombieBuilder 
{
    private GameObject zombieObj;
    private ZombieBase script;

    public ZombieBuilder(GameObject prefab, Vector3 pos) 
    {
        zombieObj = Object.Instantiate(prefab, pos, Quaternion.identity);
        script = zombieObj.GetComponent<ZombieBase>();
    }

    public ZombieBuilder SetSpeed(float s) 
    {
        script.speed = s;
        return this;
    }

    public ZombieBuilder SetName(string name) 
    {
        zombieObj.name = name;
        return this;
    }

    public GameObject Build() => zombieObj;
}
