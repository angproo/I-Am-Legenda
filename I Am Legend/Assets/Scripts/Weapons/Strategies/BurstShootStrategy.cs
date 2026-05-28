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
        // Iniciamos una corrutina de Unity a través del arma para manejar el tiempo de la ráfaga
        caller.StartCoroutine(RafagaDeTiempo(target, weaponTransform, bulletPrefab));
    }

    private IEnumerator RafagaDeTiempo(Transform target, Transform weaponTransform, GameObject bulletPrefab)
    {
        int cantidadDeDisparosEnRafaga = 3; // Cuántas balas seguidas tira la ráfaga

        for (int i = 0; i < cantidadDeDisparosEnRafaga; i++)
        {
            // Verificamos que el objetivo siga vivo durante la ráfaga
            if (target == null) yield break;

            Vector3 directionToTarget = (target.position - weaponTransform.position).normalized;
            float baseAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // 1. Bala Principal al Frente
            BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, baseAngle, directionToTarget);

            // 2. Bala Trasera (5+ kills)
            if (killCount >= 5)
            {
                float angleBack = baseAngle + 180f;
                BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, angleBack, BulletHelper.CalcularDireccionPorAngulo(angleBack));
            }

            // 3. Bala Superior (10+ kills)
            if (killCount >= 10)
            {
                float angleUp = baseAngle + 90f;
                BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, angleUp, BulletHelper.CalcularDireccionPorAngulo(angleUp));
            }

            // 4. Bala Inferior (15+ kills)
            if (killCount >= 15)
            {
                float angleDown = baseAngle - 90f;
                BulletHelper.InstantiateBullet(bulletPrefab, weaponTransform.position, angleDown, BulletHelper.CalcularDireccionPorAngulo(angleDown));
            }

            // TIEMPO ENTRE BALAS DE LA RÁFAGA: Espera 0.05 segundos antes de tirar la siguiente ráfaga
            yield return new WaitForSeconds(0.05f);
        }
    }
}
