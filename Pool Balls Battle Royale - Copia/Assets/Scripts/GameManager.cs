using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject powerup_prefab;
    public static Vector3 offset_indicatore_powerup = new Vector3(0, -0.09f, 0);
    private const float tempo_spawn_powerup = 10;
    private const float confine_x = 5f;
    private const float confine_z = 1.7f;
    private const float altezza = 0.025f;
    
    void Start()
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
    }
}
