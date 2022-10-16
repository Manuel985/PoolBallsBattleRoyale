using UnityEngine;
using System.Collections;

public class MovimentoGiocatore : MonoBehaviour
{
    [SerializeField] private AudioSource fine_superpotere;
    [SerializeField] private AudioSource inizio_superpotere;
    [SerializeField] private AudioSource suono_steccata;
    private GameObject indicatore_powerup;
    private Animator stecca_animator;
    private Stecca stecca;
    private Transform focal_point, giocatore;
    private Rigidbody giocatore_corpo_rigido;
    private bool posseggo_superpotere = false;
    private float velocita_movimento;
    private const float velocita_rotazione = 400;
    
    private void Awake()
    {
        giocatore = transform;
        giocatore_corpo_rigido = GetComponent<Rigidbody>();
        focal_point = GameObject.Find("Focal Point " + name).transform;
        indicatore_powerup = GameObject.Find("Indicatore " + name);
        indicatore_powerup.SetActive(false);
        GameObject stecca_oggetto = GameObject.Find("Stecca " + name);
        stecca = stecca_oggetto.GetComponent<Stecca>();
        stecca_animator = stecca_oggetto.GetComponent<Animator>();
    }

    private void Start()
    {
        velocita_movimento = GameManager.velocita;
        giocatore.position = GameManager.PuntoCasuale();
    }

    private void Update()
    {
        focal_point.Rotate(Vector3.up * Input.GetAxis("Mouse X") * velocita_rotazione * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stecca_animator.Play("Colpo");
            suono_steccata.Play();
        }
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
            Destroy(other.gameObject);
            posseggo_superpotere = true;
            inizio_superpotere.Play();
            indicatore_powerup.SetActive(true);
            stecca.forza_colpo = GameManager.forza_potenziata;
            StartCoroutine(PowerupCountDown());
        }
    }

    IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(10);
        stecca.forza_colpo = GameManager.forza;
        indicatore_powerup.SetActive(false);
        posseggo_superpotere = false;
        fine_superpotere.Play();
    }
}