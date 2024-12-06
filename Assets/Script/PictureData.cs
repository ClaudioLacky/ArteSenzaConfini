using UnityEngine;

public class PictureData : MonoBehaviour
{
    public Sprite image;               // Immagine del quadro
    public string title;               // Titolo del quadro
    public string description;         // Descrizione del quadro
    public AudioClip audioClip;        // Audio clip da riprodurre
    public Vector2 size;               // Dimensione dell'immagine

    void Start()
    {
        if (audioClip != null)
        {
            Debug.Log("AudioClip assegnato correttamente al quadro: " + gameObject.name + ", Audio: " + audioClip.name);
        }
        else
        {
            Debug.LogWarning("AudioClip NON assegnato al quadro: " + gameObject.name);
        }
    }

}