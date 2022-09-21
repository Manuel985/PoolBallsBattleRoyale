using UnityEngine;

public class EliminarePowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palla"))
        {
            Destroy(gameObject);
        }
    }
}
