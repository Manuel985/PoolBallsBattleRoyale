using UnityEngine;

public class Pioggia : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 1);
    }
}
