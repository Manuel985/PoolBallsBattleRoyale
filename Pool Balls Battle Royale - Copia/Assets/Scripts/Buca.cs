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
            Destroy(other);
            nebbia.Play(true);
            Destroy(other.gameObject, 5);
            Destroy(GameObject.Find("Focal Point " + other.name), 5);
        }
    }
}
