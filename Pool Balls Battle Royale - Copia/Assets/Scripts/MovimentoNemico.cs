using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoNemico : MonoBehaviour
{
    [SerializeField] Transform focal_point;
    [SerializeField] Animator stecca_animator;
    private Transform nemico;
    private float offset_centro_pallina = -1;
    private Rigidbody nemico_corpo_rigido;
    private Vector3 vettore_spostamento;
    private float velocita_movimento;
    private float velocita_rotazione_visuale = 5;
    private float raggio_rilevamento_avversari = 0.5f;
    private float raggio_rilevamento_buche = 1;
    private float raggio_rilevamento_powerups = 2;
    private Collider[] oggetti_vicini;
    IEnumerable<Collider> buche;
    IEnumerable<Transform> avversari;
    IEnumerable<Transform> powerups;

    void Awake()
    {
        nemico = transform;
        nemico_corpo_rigido = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        velocita_movimento = MovimentoGiocatore.velocita_movimento;
        vettore_spostamento = GameManager.PuntoCasuale();
        nemico.position = GameManager.PuntoCasuale();
    }

    void Update()
    {
        nemico_corpo_rigido.AddForce(vettore_spostamento.normalized * velocita_movimento, ForceMode.Force);
        vettore_spostamento = GameManager.PuntoCasuale();
        RilevamentoAvversari();
        RilevamentoBuche();
        RilevamentoPowerups();
    }

    private void RilevamentoAvversari()
    {
        oggetti_vicini = Physics.OverlapSphere(nemico.position, raggio_rilevamento_avversari);
        avversari = from oggetto in oggetti_vicini
                    where oggetto.CompareTag("Palla") && !GameObject.ReferenceEquals(oggetto.gameObject, gameObject)
                    select oggetto.transform;
        if(avversari.Any())
        {
            Transform avversario_target = avversari.First();
            vettore_spostamento = avversario_target.position - nemico.position;
            Quaternion visuale = Quaternion.LookRotation(vettore_spostamento);
            Vector3 rotazione_visuale = Quaternion.Lerp(focal_point.rotation, visuale, Time.deltaTime * velocita_rotazione_visuale).eulerAngles;
            focal_point.rotation = Quaternion.Euler(0f, rotazione_visuale.y + offset_centro_pallina, 0f);
            stecca_animator.Play("Colpo");
        }
    }

    private void RilevamentoBuche()
    {
        oggetti_vicini = Physics.OverlapSphere(nemico.position, raggio_rilevamento_buche);
        buche = from oggetto in oggetti_vicini
                where oggetto.CompareTag("Buca")
                select oggetto;
        if(buche.Any())
        {
            vettore_spostamento = Vector3.zero - nemico.position;
        }
    }

    private void RilevamentoPowerups()
    {
        oggetti_vicini = Physics.OverlapSphere(nemico.position, raggio_rilevamento_powerups);
        powerups = from oggetto in oggetti_vicini
                   where oggetto.CompareTag("Powerup")
                   select oggetto.transform;
        if(powerups.Any())
        {
            vettore_spostamento = powerups.First().position - nemico.position;
        }
    }
}
