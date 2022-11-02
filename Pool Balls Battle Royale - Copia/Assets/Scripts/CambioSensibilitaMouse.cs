using UnityEngine;
using UnityEngine.UI;

public class CambioSensibilitaMouse : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Sensibilita", slider.minValue);
        MovimentoGiocatore.velocita_rotazione = slider.value;
    }

    public void CambioSensibilita()
    {
        PlayerPrefs.SetFloat("Sensibilita", slider.value);
        MovimentoGiocatore.velocita_rotazione = slider.value;
    }
}
