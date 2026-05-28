using UnityEngine;

public class SimpleShootStrategy : IShootStrategy
{
    public void ExecuteShoot(Transform target, MonoBehaviour caller, Transform weaponTransform, GameObject bulletPrefab)
    {
        Vector3 directionToTarget = (target.position - weaponTransform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, baseAngle, directionToTarget);
    }
}
