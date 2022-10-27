using UnityEngine;
using UnityEngine.UI;

public class Risultati : MonoBehaviour
{
    [SerializeField] private Text giocate, vinte, perse;

    private void Start()
    {
        int vittorie = PlayerPrefs.GetInt("Vittorie", 0);
        int sconfitte = PlayerPrefs.GetInt("Sconfitte", 0);
        vinte.text = "Victories: " + vittorie;
        perse.text = "Eliminations: " + sconfitte;
        giocate.text = "Games: " + (vittorie + sconfitte);
    }
}
