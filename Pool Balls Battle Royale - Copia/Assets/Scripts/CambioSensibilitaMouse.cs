using UnityEngine;
using UnityEngine.UI;

public class CambioSensibilitaMouse : MonoBehaviour
{
    public static Slider slider;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Sensibilita", slider.minValue);
    }

    public void CambioSensibilita()
    {
        PlayerPrefs.SetFloat("Sensibilita", slider.value);
        MovimentoGiocatore.velocita_rotazione = slider.value;
    }
}
