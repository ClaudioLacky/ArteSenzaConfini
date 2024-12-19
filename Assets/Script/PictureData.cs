using UnityEngine;

/// <summary>
/// Classe che contiene i dati del quadro
/// </summary>
public class PictureData : MonoBehaviour
{
    public Sprite image;               // Immagine del quadro
    public string title;               // Titolo del quadro
    public string description;         // Descrizione del quadro
    public AudioClip audioClip;        // Audio clip da riprodurre
    public Vector2 size;               // Dimensione dell'immagine
}