using UnityEngine;
using System.Collections;

public class MovimentoGiocatore : MonoBehaviour
{
    private Transform focal_point;
    private Transform indicatore_powerup;
    private Rigidbody giocatore_corpo_rigido;
    private Transform giocatore;
    private bool posseggo_superpotere = false;
    public static float velocita_movimento = 5;
    [SerializeField] private GameObject indicatore_powerup_prefab;
    
    private void Awake()
    {
        giocatore = transform;
        giocatore_corpo_rigido = GetComponent<Rigidbody>();
        focal_point = GameObject.Find("Focal Point " + name).transform;
    }

    void Start()
    {
        giocatore.position = GameManager.PuntoCasuale();
    }

    private void FixedUpdate()
    {
        Vector3 avanti_indietro = focal_point.forward * Input.GetAxis("Vertical");
        Vector3 destra_sinistra = focal_point.right * Input.GetAxis("Horizontal");
        Vector3 risultante = avanti_indietro + destra_sinistra;
        giocatore_corpo_rigido.AddForce((risultante).normalized * velocita_movimento, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !posseggo_superpotere)
        {
            posseggo_superpotere = true;
            Destroy(other.gameObject);
            indicatore_powerup = Instantiate(indicatore_powerup_prefab).transform;
            indicatore_powerup.SetParent(focal_point);
            indicatore_powerup.localPosition = GameManager.offset_indicatore_powerup;
            StartCoroutine(PowerupCountDown());
        }
    }

    IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        Destroy(indicatore_powerup.gameObject);
        posseggo_superpotere = false;
    }
}