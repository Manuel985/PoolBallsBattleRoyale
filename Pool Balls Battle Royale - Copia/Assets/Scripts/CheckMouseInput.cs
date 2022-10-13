using UnityEngine;

public class CheckMouseInput : MonoBehaviour
{
    private Animator stecca_animator;
    private Transform focal_point;
    private const float velocita_rotazione = 400;

    private void Awake()
    {
        stecca_animator = GameObject.Find("Stecca " + name).GetComponent<Animator>();
        focal_point = GameObject.Find("Focal Point " + name).transform;
    }

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
