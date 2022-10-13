using System.Linq;
using System.Collections;
using UnityEngine;

public class MovimentoNemico : MonoBehaviour
{
    private Animator stecca_animator;
    private Transform focal_point, indicatore_powerup, nemico;
    private Rigidbody nemico_corpo_rigido;
    private Vector3 vettore_spostamento;
    private bool posseggo_superpotere = false;
    private float velocita_movimento;
    private const float velocita_rotazione_visuale = 5;
    private const float raggio_rilevamento_avversari = 1f;
    private const float raggio_rilevamento_buche = 1;
    private const float raggio_rilevamento_powerups = 2;
    [SerializeField] private GameObject indicatore_powerup_prefab;

    void Awake()
    {
        nemico = transform;
        nemico_corpo_rigido = GetComponent<Rigidbody>();
        stecca_animator = GameObject.Find("Stecca " + name).GetComponent<Animator>();
        focal_point = GameObject.Find("Focal Point " + name).transform;
    }

    private void Start()
    {
        velocita_movimento = MovimentoGiocatore.velocita_movimento;
        vettore_spostamento = GameManager.PuntoCasuale();
        nemico.position = GameManager.PuntoCasuale();
    }

    void FixedUpdate()
    {
        nemico_corpo_rigido.AddForce(vettore_spostamento.normalized * velocita_movimento, ForceMode.Force);
        vettore_spostamento = GameManager.PuntoCasuale();
        RilevamentoAvversari();
        RilevamentoBuche();
        RilevamentoPowerups();
    }

    private void RilevamentoAvversari()
    {
        var avversari = Physics.OverlapSphere(nemico.position, raggio_rilevamento_avversari)
                        .Where(oggetto => oggetto.CompareTag("Palla") && !GameObject.ReferenceEquals(oggetto.gameObject, gameObject))
                        .Select(oggetto => oggetto.transform);
        if(avversari.Any())
        {
            Transform avversario_target = avversari.ElementAt(Random.Range(0, avversari.Count()));
            vettore_spostamento = avversario_target.position - nemico.position;
            Quaternion visuale = Quaternion.LookRotation(vettore_spostamento);
            Vector3 rotazione_visuale = Quaternion.Lerp(focal_point.rotation, visuale, Time.deltaTime * velocita_rotazione_visuale).eulerAngles;
            focal_point.rotation = Quaternion.Euler(0f, rotazione_visuale.y, 0f);
            stecca_animator.Play("Colpo");
        }
    }

    private void RilevamentoBuche()
    {
        var buche = Physics.OverlapSphere(nemico.position, raggio_rilevamento_buche)
                    .Where(oggetto => oggetto.CompareTag("Buca"))
                    .Select(oggetto => oggetto);
        if(buche.Any())
        {
            vettore_spostamento = Vector3.zero - nemico.position;
        }
    }

    private void RilevamentoPowerups()
    {
        var powerups = Physics.OverlapSphere(nemico.position, raggio_rilevamento_powerups)
                       .Where(oggetto => oggetto.CompareTag("Powerup"))
                       .Select(oggetto => oggetto.transform);
        if(powerups.Any())
        {
            vettore_spostamento = powerups.First().position - nemico.position;
        }
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

    private IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        Destroy(indicatore_powerup.gameObject);
        posseggo_superpotere = false;
    }
}
