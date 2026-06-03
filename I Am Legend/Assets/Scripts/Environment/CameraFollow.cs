using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Configuración de Cámara")]
    public float sizeInicial = 8f;       
    public float sizeMaximoMiedo = 6f;   

    [Header("Miedo Effects")]
    public float maxShakeIntensity = 0.5f;
    private float currentShake = 0f;
    private Camera cam;

    void Awake() 
    { 
        cam = GetComponent<Camera>(); 
        if (cam != null) cam.orthographicSize = sizeInicial;
    }

   
    public void ActualizarEfectosPorSlider(float valor) 
    {
        if (cam == null) return;

       
        if (valor < 50f)
        {
            currentShake = 0f; 
        }
        else
        {
          
            float factorMiedoAlto = (valor - 50f) / 50f;
            currentShake = factorMiedoAlto * maxShakeIntensity;
        }
        
      
        cam.orthographicSize = Mathf.Lerp(sizeInicial, sizeMaximoMiedo, valor / 100f);
    }

    void LateUpdate() 
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        
       
        if (currentShake > 0f) {
            Vector3 shakeOffset = (Vector3)Random.insideUnitCircle * currentShake;
            shakeOffset.z = 0;
            desiredPosition += shakeOffset;
        }
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
