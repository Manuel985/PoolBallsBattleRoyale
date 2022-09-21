using UnityEngine;

public class FocalPoint : MonoBehaviour
{
    [SerializeField] private Transform soggetto;
    private Transform focal_point;
    
    private void Awake()
    {
        focal_point = transform;
    }

    void Update()
    {
        focal_point.position = soggetto.position;
    }
}
