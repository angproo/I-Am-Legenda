using UnityEngine;

public static class BulletHelper 
{
    public static void InstantiateBullet(GameObject prefab, Vector3 position, float angulo, Vector2 direccion)
    {
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        GameObject bullet = Object.Instantiate(prefab, position, bulletRotation);
        if (bullet.GetComponent<TrailRenderer>() == null)
        {
            TrailRenderer trail = bullet.AddComponent<TrailRenderer>();
            trail.time = 0.1f;
            trail.startWidth = 0.1f;
            trail.endWidth = 0f;
            trail.material = new Material(Shader.Find("Sprites/Default"));
            trail.startColor = Color.yellow;
            trail.endColor = new Color(1, 1, 0, 0);
        }
        
        Object.Destroy(bullet, 1.0f);
        
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
