using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bottone : MonoBehaviour
{
    [SerializeField] private AudioSource premo, sopra;
    [SerializeField] private GameObject barra, bottone_beginner, bottone_intermediate, bottone_proplayer;
    public static float velocita_rotazione_visuale, raggio_rilevamento_buche, raggio_rilevamento_powerups, raggio_rilevamento_avversari;

    public void BarraCaricamento()
    {
        bottone_beginner.SetActive(false);
        bottone_intermediate.SetActive(false);
        bottone_proplayer.SetActive(false);
        barra.SetActive(true);
    }

    public void CaricaPartita()
    {
        StartCoroutine(OffsetCaricamentoScena());
    }

    private IEnumerator OffsetCaricamentoScena()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("Partita");
    }

    public void CaricaMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void SuonoMouseClick()
    {
        premo.Play();
    }

    public void SuonoMouseOver()
    {
        sopra.Play();
    }

    public void DifficoltaSemplice()
    {
        velocita_rotazione_visuale = 0.5f;
        raggio_rilevamento_buche = 0.3f;
        raggio_rilevamento_powerups = 1;
        raggio_rilevamento_avversari = 1;
    }

    public void DifficoltaMedia()
    {
        velocita_rotazione_visuale = 2;
        raggio_rilevamento_buche = 0.6f;
        raggio_rilevamento_powerups = 2;
        raggio_rilevamento_avversari = 2;
    }

    public void DifficoltaAvanzata()
    {
        velocita_rotazione_visuale = 5;
        raggio_rilevamento_buche = 1;
        raggio_rilevamento_powerups = 3;
        raggio_rilevamento_avversari = 4;
    }
}
