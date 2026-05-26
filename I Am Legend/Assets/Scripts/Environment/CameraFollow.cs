using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10); 

    [Header("Configuración de Cámara")]
    public float sizeInicial = 8f;       // Ahora el juego arranca con mucha más visión
    public float sizeMaximoMiedo = 6f;   // El zoom que se clava cuando ponés el slider al 100%

    [Header("Efectos de Shake")]
    private Camera cam;
    private float shakeIntensity = 0f;
    public float maxShakeIntensity = 0.3f; // Fuerza del temblor al llegar al 100%

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.orthographicSize = sizeInicial; // Inicializa en 8
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // 1. Seguimiento suavizado del jugador
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // 2. Aplicación del shake infinito (Solo si el slider pasó de 50)
            if (shakeIntensity > 0f)
            {
                Vector3 shakeOffset = (Vector3)Random.insideUnitCircle * shakeIntensity;
                shakeOffset.z = 0; 
                smoothedPosition += shakeOffset;
            }

            transform.position = smoothedPosition;
        }
    }

    // El slider llamará directamente a esta función pasándole su valor físico (0 a 100)
    public void ActualizarEfectosPorSlider(float valorSlider)
    {
        if (cam == null) return;

        // --- LÓGICA DE SHAKE SEGÚN EL SLIDER ---
        if (valorSlider < 50f)
        {
            shakeIntensity = 0f; // Menos de 50% no tiembla nada
        }
        else
        {
            // Mapea el rango de 50-100 del slider a un factor de 0.0 a 1.0
            float factorMiedoAlto = (valorSlider - 50f) / 50f; 
            shakeIntensity = factorMiedoAlto * maxShakeIntensity;
        }

     
        // Pasa de Size 8 (al estar en 0%) a Size 6 (al estar en 100%), acercando la pantalla
        cam.orthographicSize = Mathf.Lerp(sizeInicial, sizeMaximoMiedo, valorSlider / 100f);
    }
}
