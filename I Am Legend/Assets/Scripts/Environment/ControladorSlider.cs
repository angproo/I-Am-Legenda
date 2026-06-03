using UnityEngine;
using UnityEngine.UI;

public class ControladorSlider : MonoBehaviour
{
    private Slider slider;
    public GameMediator mediator; 

    private void Start() 
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        
        if (slider != null && mediator != null) {
            slider.onValueChanged.AddListener(AlCambiarSlider);
        }
    }

    private void AlCambiarSlider(float valor) 
    {
        mediator.NotifyThreatUpdate(valor);
    }
}
