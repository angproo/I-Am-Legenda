using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IThreatObserver
{
    public TextMeshProUGUI threatText;
    public Slider sliderMiedo; 

    private void Start()
    {
        ThreatManager tm = FindFirstObjectByType<ThreatManager>();
        if (tm != null) 
        {
            tm.RegisterObserver(this);
        }

        if (sliderMiedo != null)
        {
            sliderMiedo.minValue = 0f;
            sliderMiedo.maxValue = 100f;
            sliderMiedo.value = 0f;
        }
    }

    public void OnThreatChanged(int currentThreat)
    {
        if (threatText != null)
        {
            threatText.text = $"AMENAZA: {currentThreat}%";
        }

        if (sliderMiedo != null)
        {
            sliderMiedo.value = currentThreat; 
        }
    }
}