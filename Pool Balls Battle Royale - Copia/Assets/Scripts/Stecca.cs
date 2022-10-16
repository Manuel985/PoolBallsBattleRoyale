using UnityEngine;

public class Stecca : MonoBehaviour
{
    private Animator stecca_animator;
    private Transform stecca;
    public float forza_colpo;

    private void Awake()
    {
        stecca_animator = GetComponent<Animator>();
        stecca = transform;
    }

    private void Start()
    {
        forza_colpo = GameManager.forza;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Palla") && stecca_animator.GetCurrentAnimatorStateInfo(0).IsName("Colpo"))
        {
            other.GetComponent<Rigidbody>().AddForce(stecca.forward * forza_colpo, ForceMode.Impulse);
        }
    }
}
