using UnityEngine;

public class Stecca : MonoBehaviour
{
    private Animator stecca_animator;
    private Transform stecca;
    private float forza_colpo = 7;

    void Awake()
    {
        stecca_animator = GetComponent<Animator>();
        stecca = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Palla") && stecca_animator.GetCurrentAnimatorStateInfo(0).IsName("Colpo"))
        {
            other.GetComponent<Rigidbody>().AddForce(stecca.forward * forza_colpo, ForceMode.Impulse);
        }
    }
}
