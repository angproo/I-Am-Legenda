using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10); 

    [Header("Configuración de Cámara")]
    public float sizeInicial = 8f;      
    public float sizeMaximoMiedo = 6f;  

    [Header("Color de Fondo (Sky/Background)")]
    // Definís los dos colores para el degradado del fondo
    public Color colorFondoInicial = new Color(0.1f, 0.1f, 0.15f); // Un gris/azul oscuro por defecto
    public Color colorFondoMiedo = new Color(0.05f, 0.0f, 0.0f);    // Un rojo casi negro para el clímax

    [Header("Efectos de Shake")]
    private Camera cam;
    private float shakeIntensity = 0f;
    public float maxShakeIntensity = 0.3f; 

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.orthographicSize = sizeInicial;
            // Opcional: Asignás el color inicial desde el arranque
            cam.backgroundColor = colorFondoInicial;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            if (shakeIntensity > 0f)
            {
                Vector3 shakeOffset = (Vector3)Random.insideUnitCircle * shakeIntensity;
                shakeOffset.z = 0; 
                smoothedPosition += shakeOffset;
            }

            transform.position = smoothedPosition;
        }
    }

    public void ActualizarEfectosPorSlider(float valorSlider)
    {
        if (cam == null) return;

        float t = valorSlider / 100f;

        // 1. Lógica del Shake
        if (valorSlider < 50f)
        {
            shakeIntensity = 0f; 
        }
        else
        {
            float factorMiedoAlto = (valorSlider - 50f) / 50f; 
            shakeIntensity = factorMiedoAlto * maxShakeIntensity;
        }

        // 2. Lógica del Zoom (Tamaño de cámara)
        cam.orthographicSize = Mathf.Lerp(sizeInicial, sizeMaximoMiedo, t);

        // 3. NUEVO: Lógica del Color de Fondo (Skybox/Background Color)
        cam.backgroundColor = Color.Lerp(colorFondoInicial, colorFondoMiedo, t);
    }
}
