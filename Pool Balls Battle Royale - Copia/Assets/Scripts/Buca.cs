using UnityEngine;
using System.Collections;
using TMPro;

public class Buca : MonoBehaviour
{
    [SerializeField] private AudioSource caduta, sconfitta, vittoria;
    [SerializeField] private GameObject pannello_sconfitta, pannello_vittoria, bottone_pausa;
    private TextMeshProUGUI numero_vivi, etichetta_eliminati;

    private void Awake()
    {
        numero_vivi = GameObject.Find("Numero Vivi").GetComponent<TextMeshProUGUI>();
        etichetta_eliminati = GameObject.Find("Etichetta Eliminati").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palla"))
        {
            GameManager.numero_palline--;
            string nome_player = other.name;
            Destroy(other);
            caduta.Play();
            numero_vivi.text = "" + GameManager.numero_palline;
            etichetta_eliminati.SetText(etichetta_eliminati.text + "\n" + "- Player " + nome_player + " Eliminated");
            StartCoroutine(CancelloEtichetta());
            if(nome_player != "8")
            {
                Destroy(other.gameObject, 0.3f);
                Destroy(GameObject.Find("Focal Point " + nome_player), 0.3f);
                if(GameManager.numero_palline == 1)
                {
                    PlayerPrefs.SetInt("Vittorie", PlayerPrefs.GetInt("Vittorie", 0) + 1);
                    StartCoroutine(Vittoria());
                }
            }
            else
            {
                PlayerPrefs.SetInt("Sconfitte", PlayerPrefs.GetInt("Sconfitte", 0) + 1);
                StartCoroutine(GameOver());
            }
        }
    }

    private IEnumerator CancelloEtichetta()
    {
        yield return new WaitForSeconds(3);
        etichetta_eliminati.text = "";
    }

    private IEnumerator GameOver()
    {
        bottone_pausa.SetActive(false);
        yield return new WaitForSeconds(3);
        StoppoGioco();
        pannello_sconfitta.SetActive(true);
        sconfitta.Play();
    }

    private IEnumerator Vittoria()
    {
        bottone_pausa.SetActive(false);
        yield return new WaitForSeconds(1);
        StoppoGioco();
        pannello_vittoria.SetActive(true);
        vittoria.Play();
    }

    private void StoppoGioco()
    {
        Time.timeScale = 0;
        caduta.volume = 0;
        GameManager.suono_powerup.volume = 0;
        GameManager.musica.volume = 0;
        MovimentoGiocatore.fine_superpotere.volume = 0;
        MovimentoGiocatore.suono_steccata.volume = 0;
    }
}
