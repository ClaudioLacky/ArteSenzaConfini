using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PictureData : MonoBehaviour
{
    public Sprite image; // Immagine del quadro
    public Vector2 size; // Dimensioni da visualizzare
    public string text; // Testo del quadro
}
