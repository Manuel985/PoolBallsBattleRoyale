using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource musica_prefab, suono_powerup_prefab;
    [SerializeField] private GameObject powerup_prefab;
    public static AudioSource musica, suono_powerup;
    public static float numero_palline;
    public static float velocita = 5;
    public static float forza = 7;
    public static float forza_potenziata = 20;
    private const float tempo_spawn_powerup = 13;
    private const float confine_x = 5f;
    private const float confine_z = 1.7f;
    private const float altezza = 0.025f;

    private void Awake()
    { 
        musica = musica_prefab;
        suono_powerup = suono_powerup_prefab;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        numero_palline = GameObject.FindGameObjectsWithTag("Palla").Length;
        StartCoroutine(PartenzaRitardata());
        InvokeRepeating("NuovoPowerup", tempo_spawn_powerup, tempo_spawn_powerup);
    }

    public static Vector3 PuntoCasuale()
    {
        return new Vector3(Random.Range(-confine_x, confine_x), altezza, Random.Range(-confine_z, confine_z));
    }

    private void NuovoPowerup()
    {
        Instantiate(powerup_prefab, PuntoCasuale(), powerup_prefab.transform.rotation);
        suono_powerup.Play();
    }

    private IEnumerator PartenzaRitardata()
    {
        Time.timeScale = 0;
        float pausa = Time.realtimeSinceStartup + 1.5f;
        while (Time.realtimeSinceStartup < pausa)
            yield return 0;
        Time.timeScale = 1;
        musica.Play();
    }
}
