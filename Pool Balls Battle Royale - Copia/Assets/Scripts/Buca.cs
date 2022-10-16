using UnityEngine;
using System.Collections;
using TMPro;

public class Buca : MonoBehaviour
{
    [SerializeField] private AudioSource caduta;
    private TextMeshProUGUI numero_vivi;
    private TextMeshProUGUI etichetta_eliminati;
    private void Awake()
    {
        numero_vivi = GameObject.Find("Numero Vivi").GetComponent<TextMeshProUGUI>();
        etichetta_eliminati = GameObject.Find("Etichetta Eliminati").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palla"))
        {
            Destroy(other);
            caduta.Play();
            GameManager.numero_palline--;
            numero_vivi.text = "" + GameManager.numero_palline;
            etichetta_eliminati.SetText(etichetta_eliminati.text + "\n" + "- Player " + other.name + " Eliminated");
            StartCoroutine(CancellaEtichetta());
            Destroy(other.gameObject, 1.4f);
            Destroy(GameObject.Find("Focal Point " + other.name), 1.4f);
        }
    }

    private IEnumerator CancellaEtichetta()
    {
        yield return new WaitForSeconds(3);
        etichetta_eliminati.text = "";
    }
}
