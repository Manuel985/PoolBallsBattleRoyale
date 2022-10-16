using UnityEngine;
using System.Collections;
using TMPro;

public class Buca : MonoBehaviour
{
    private ParticleSystem nebbia;
    private TextMeshProUGUI numero_vivi;
    private TextMeshProUGUI etichetta_eliminati;
    private void Awake()
    {
        nebbia = GetComponent<ParticleSystem>();
        numero_vivi = GameObject.Find("Numero Vivi").GetComponent<TextMeshProUGUI>();
        etichetta_eliminati = GameObject.Find("Etichetta Eliminati").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palla"))
        {
            Destroy(other);
            nebbia.Play(true);
            GameManager.numero_palline--;
            numero_vivi.text = "" + GameManager.numero_palline;
            etichetta_eliminati.SetText(etichetta_eliminati.text + "\n" + "- Player " + other.name + " Eliminated");
            StartCoroutine(CancellaEtichetta());
            Destroy(other.gameObject, 5);
            Destroy(GameObject.Find("Focal Point " + other.name), 5);
        }
    }

    private IEnumerator CancellaEtichetta()
    {
        yield return new WaitForSeconds(3);
        etichetta_eliminati.text = "";
    }
}
