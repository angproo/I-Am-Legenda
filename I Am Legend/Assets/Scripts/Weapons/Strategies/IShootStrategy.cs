using UnityEngine;

public interface IShootStrategy
{
    void ExecuteShoot(Transform target, MonoBehaviour caller, Transform weaponTransform, GameObject bulletPrefab);
}
