using UnityEngine;

public class FocalPoint : MonoBehaviour
{
    private Transform soggetto, focal_point;
    
    private void Awake()
    {
        focal_point = transform;
        soggetto = GameObject.Find(name.Substring(12)).transform;
    }

    private void Update()
    {
        focal_point.position = soggetto.position;
    }
}
