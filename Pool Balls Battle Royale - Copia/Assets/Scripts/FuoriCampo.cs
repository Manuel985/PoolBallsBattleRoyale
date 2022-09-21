using UnityEngine;

public class FuoriCampo : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Palla"))
        {
            other.gameObject.SetActive(false);
            GameObject.Find("Focal Point " + other.name).SetActive(false);
        }
    }
}
