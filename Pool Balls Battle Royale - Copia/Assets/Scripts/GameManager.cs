using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource suono_powerup;
    [SerializeField] private GameObject powerup_prefab;
    public static float numero_palline = 15; 
    public static float velocita = 5;
    public static float forza = 7;
    public static float forza_potenziata = 20;
    private const float tempo_spawn_powerup = 10;
    private const float confine_x = 5f;
    private const float confine_z = 1.7f;
    private const float altezza = 0.025f;
    
    private void Start()
    {
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
}
