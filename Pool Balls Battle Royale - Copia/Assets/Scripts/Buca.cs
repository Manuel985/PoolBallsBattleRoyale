using UnityEngine;

public class Buca : MonoBehaviour
{
    private ParticleSystem nebbia;
    private void Awake()
    {
        nebbia = GetComponent<ParticleSystem>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palla"))
        {
            other.gameObject.SetActive(false);
            GameObject.Find("Focal Point " + other.name).SetActive(false);
            nebbia.Play(true);
        }
    }
}
