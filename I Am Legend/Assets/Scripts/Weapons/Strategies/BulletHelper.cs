using UnityEngine;

public static class BulletHelper 
{
    public static void InstantiateBullet(GameObject prefab, Vector3 position, float angulo, Vector2 direccion)
    {
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        GameObject bullet = Object.Instantiate(prefab, position, bulletRotation);
        
        Object.Destroy(bullet, 3.0f);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * 15f; 
        }
    }

    public static Vector2 CalcularDireccionPorAngulo(float anguloEnGrados)
    {
        return new Vector2(Mathf.Cos(anguloEnGrados * Mathf.Deg2Rad), Mathf.Sin(anguloEnGrados * Mathf.Deg2Rad));
    }
}
