using UnityEngine;

public class MovimentoGiocatore : MonoBehaviour
{
    private Rigidbody giocatore_corpo_rigido;
    private Transform giocatore;
    [SerializeField] private Transform focal_point;

    public static float velocita_movimento = 5;

    private void Awake()
    {
        giocatore = transform;
        giocatore_corpo_rigido = GetComponent<Rigidbody>();
    }

    void Start()
    {
        giocatore.position = GameManager.PuntoCasuale();
    }

    private void Update()
    {
        Movimento(focal_point.forward, focal_point.right, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }

    private void Movimento(Vector3 direzione_verticale, Vector3 direzione_orizzontale, float input_verticale, float input_orizzontale)
    {
        Vector3 avanti_indietro = direzione_verticale * input_verticale;
        Vector3 destra_sinistra = direzione_orizzontale * input_orizzontale;
        giocatore_corpo_rigido.AddForce((avanti_indietro + destra_sinistra).normalized * velocita_movimento, ForceMode.Force);
    }
}