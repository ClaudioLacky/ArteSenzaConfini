using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtGalleryManager : MonoBehaviour
{
    [System.Serializable]
    public class ArtPiece
    {
        public string title;  // Nome del quadro
        public Sprite image;  // Immagine del quadro
    }

    public List<ArtPiece> artPieces;  // Lista di quadri da configurare nell'Inspector
    public GameObject artPrefab;      // Il prefab da istanziare
    public Transform galleryParent;   // Il contenitore (ad esempio, uno ScrollView)

    void Start()
    {
        foreach (var art in artPieces)
        {
            // Crea un'istanza del prefab
            GameObject artObject = Instantiate(artPrefab, galleryParent);

            // Trova i componenti nell'istanza
            Image artImage = artObject.transform.Find("Image").GetComponent<Image>();
            Text artTitle = artObject.transform.Find("Title").GetComponent<Text>();

            // Assegna i dati
            artImage.sprite = art.image;
            artTitle.text = art.title;
        }
    }
}
