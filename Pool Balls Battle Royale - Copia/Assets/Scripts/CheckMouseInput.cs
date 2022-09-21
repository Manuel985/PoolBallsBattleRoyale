using UnityEngine;

public class CheckMouseInput : MonoBehaviour
{
    [SerializeField] private Animator stecca_animator;
    [SerializeField] private Transform focal_point;
    private float velocita_rotazione = 400;

    void Update()
    {
        RuotaVisuale(Vector3.up, Input.GetAxis("Mouse X"));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stecca_animator.Play("Colpo");
        }
    }
    private void RuotaVisuale(Vector3 direzione, float input)
    {
        focal_point.Rotate(direzione * input * velocita_rotazione * Time.deltaTime);
    }
}
