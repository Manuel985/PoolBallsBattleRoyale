using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject powerup_prefab;
    [SerializeField] GameObject safe_zone;
    [SerializeField] Transform palla_bianca;
    private const float tempo_spawn_powerup = 5;
    private const float tempo_spawn_safe_zone = 10;
    private const float confine_x = 5f;
    private const float confine_z = 1.7f;
    private const float altezza = 0.025f;

    void Start()
    {
        InvokeRepeating("NuovoPowerup", tempo_spawn_powerup, tempo_spawn_powerup);
        InvokeRepeating("NuovaSafeZone", tempo_spawn_safe_zone, tempo_spawn_safe_zone);
    }

    public static Vector3 PuntoCasuale()
    {
        return new Vector3(Random.Range(-confine_x, confine_x), altezza, Random.Range(-confine_z, confine_z));
    }

    private void NuovoPowerup()
    {
        Instantiate(powerup_prefab, PuntoCasuale(), powerup_prefab.transform.rotation);
    }

    private void NuovaSafeZone()
    {
        StartCoroutine(PioggiaPalleBianche());
    }

    IEnumerator PioggiaPalleBianche()
    {
        safe_zone.transform.position = PuntoCasuale();
        safe_zone.SetActive(true);
        for(int i=0; i<30; i++)
        {
            float offset_x = Random.Range(-0.8f, 0.8f);
            float offset_z = Random.Range(-0.8f, 0.8f);
            float altezza = Random.Range(5, 20);
            Vector3 offset = new Vector3(offset_x, altezza, offset_z);
            Instantiate(palla_bianca, offset + safe_zone.transform.position, palla_bianca.transform.rotation);
        }
        yield return new WaitForSeconds(2);
        safe_zone.SetActive(false);
    }
}
