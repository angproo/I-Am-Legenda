using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleFlashEffect : MonoBehaviour
{
    private Light2D flashLight;
    private float timer;

    void Awake() {
        flashLight = GetComponentInChildren<Light2D>();
    }

    void OnEnable() {
        if (flashLight != null) flashLight.enabled = true;
        timer = 0.05f; // Duración de la luz
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0 && flashLight != null) {
            flashLight.enabled = false;
        }
    }
}
