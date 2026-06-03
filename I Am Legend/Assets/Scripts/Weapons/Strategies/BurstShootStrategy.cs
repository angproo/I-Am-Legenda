using UnityEngine;
using System.Collections;

public class BurstShootStrategy : IShootStrategy
{
  private int killCount;

    public BurstShootStrategy(int currentKills)
    {
        this.killCount = currentKills;
    }

    public void ExecuteShoot(Transform target, MonoBehaviour caller, Transform weaponTransform, GameObject bulletPrefab)
    {
       
        caller.StartCoroutine(RafagaDeTiempo(target, weaponTransform, bulletPrefab));
    }

    private IEnumerator RafagaDeTiempo(Transform target, Transform weaponTransform, GameObject bulletPrefab)
    {
        int cantidadDeDisparosEnRafaga = 3; // Cuántas balas seguidas tira la ráfaga

        for (int i = 0; i < cantidadDeDisparosEnRafaga; i++)
        {
           
            if (target == null) yield break;

            Vector3 directionToTarget = (target.position - weaponTransform.position).normalized;
            float baseAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

           
            BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, baseAngle, directionToTarget);

           
            if (killCount >= 5)
            {
                float angleBack = baseAngle + 180f;
                BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, angleBack, BulletHelper.CalcularDireccionPorAngulo(angleBack));
            }

           
            if (killCount >= 10)
            {
                float angleUp = baseAngle + 90f;
                BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, angleUp, BulletHelper.CalcularDireccionPorAngulo(angleUp));
            }

           
            if (killCount >= 15)
            {
                float angleDown = baseAngle - 90f;
                BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, angleDown, BulletHelper.CalcularDireccionPorAngulo(angleDown));
            }

           
            yield return new WaitForSeconds(0.05f);
        }
    }
}
