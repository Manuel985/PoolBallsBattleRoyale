using System.Linq;
using System.Collections;
using UnityEngine;

public class MovimentoNemico : MonoBehaviour
{
    private GameObject indicatore_powerup;
    private Animator stecca_animator;
    private Stecca stecca;
    private Transform focal_point, nemico;
    private Rigidbody nemico_corpo_rigido;
    private bool posseggo_superpotere;
    private float velocita_movimento, velocita_rotazione_visuale, raggio_rilevamento_avversari, raggio_rilevamento_buche, raggio_rilevamento_powerups;

    private void Awake()
    {
        nemico = transform;
        nemico_corpo_rigido = GetComponent<Rigidbody>();
        GameObject stecca_oggetto = GameObject.Find("Stecca " + name);
        stecca_animator = stecca_oggetto.GetComponent<Animator>();
        stecca = stecca_oggetto.GetComponent<Stecca>();
        indicatore_powerup = GameObject.Find("Indicatore " + name);
        indicatore_powerup.SetActive(false);
        focal_point = GameObject.Find("Focal Point " + name).transform;
    }

    private void Start()
    {
        posseggo_superpotere = false;
        raggio_rilevamento_avversari = 1;
        velocita_rotazione_visuale = Bottone.velocita_rotazione_visuale;
        raggio_rilevamento_buche = Bottone.raggio_rilevamento_buche;
        raggio_rilevamento_powerups = Bottone.raggio_rilevamento_powerups;
        velocita_movimento = GameManager.velocita;
        nemico.position = GameManager.PuntoCasuale();
    }

    private void FixedUpdate()
    {
        Vector3 vettore_spostamento;
        var avversari = Physics.OverlapSphere(nemico.position, raggio_rilevamento_avversari)
                        .Where(oggetto => oggetto.CompareTag("Palla") && !GameObject.ReferenceEquals(oggetto.gameObject, gameObject))
                        .Select(oggetto => oggetto.transform);
        var buche = Physics.OverlapSphere(nemico.position, raggio_rilevamento_buche)
                    .Where(oggetto => oggetto.CompareTag("Buca"))
                    .Select(oggetto => oggetto);
        var powerups = Physics.OverlapSphere(nemico.position, raggio_rilevamento_powerups)
                       .Where(oggetto => oggetto.CompareTag("Powerup") && !posseggo_superpotere)
                       .Select(oggetto => oggetto.transform);
        if (powerups.Any())
        {
            vettore_spostamento = powerups.First().position - nemico.position;
        }
        else if (buche.Any())
        {
            vettore_spostamento = Vector3.zero - nemico.position;
        }
        else if (avversari.Any())
        {
            Transform avversario_target = avversari.ElementAt(Random.Range(0, avversari.Count()));
            vettore_spostamento = avversario_target.position - nemico.position;
            Quaternion visuale = Quaternion.LookRotation(vettore_spostamento);
            Vector3 rotazione_visuale = Quaternion.Lerp(focal_point.rotation, visuale, Time.deltaTime * velocita_rotazione_visuale).eulerAngles;
            focal_point.rotation = Quaternion.Euler(0f, rotazione_visuale.y, 0f);
            stecca_animator.Play("Colpo");
        }
        else
        {
            vettore_spostamento = GameManager.PuntoCasuale();
        }
        nemico_corpo_rigido.AddForce(vettore_spostamento.normalized * velocita_movimento, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !posseggo_superpotere)
        {
            Destroy(other.gameObject);
            posseggo_superpotere = true;
            indicatore_powerup.SetActive(true);
            stecca.forza_colpo = GameManager.forza_potenziata;
            raggio_rilevamento_avversari = Bottone.raggio_rilevamento_avversari;
            StartCoroutine(PowerupCountDown());
        }
    }

    private IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(15);
        raggio_rilevamento_avversari = 1;
        stecca.forza_colpo = GameManager.forza;
        indicatore_powerup.SetActive(false);
        posseggo_superpotere = false;
    }
}
