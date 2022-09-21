using System.Collections;
using UnityEngine;

public class AttivareSuperpoteri : MonoBehaviour
{
    [SerializeField] private GameObject indicatore_powerup;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            indicatore_powerup.SetActive(true);
            StartCoroutine(PowerupCountDown());
        }
    }

    IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        indicatore_powerup.SetActive(false);
    }
}
