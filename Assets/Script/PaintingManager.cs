using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintingManager : MonoBehaviour
{
    public GameObject uiPanel; // Il pannello dell'interfaccia
    public Image paintingImage; // L'elemento UI per l'immagine
    public TextMeshProUGUI titleText; // L'elemento UI per il titolo
    public RectTransform paintingImageRect;

    void Start()
    {
        uiPanel.SetActive(false); // Nascondi il pannello all'inizio
    }

    public void ShowPaintingInfo(Sprite image, string title, float customWidth, float customHeight)
    {
        paintingImage.sprite = image;
        titleText.text = title;
        ResizeImage(customWidth, customHeight);
        uiPanel.SetActive(true);
        Time.timeScale = 0f; // Blocca il gioco
    }

    public void CloseUIPanel()
    {
        uiPanel.SetActive(false);
        Time.timeScale = 1f; // Riprendi il gioco
    }
    
   private void ResizeImage(float customWidth, float customHeight)
       {
       Debug.Log(customHeight + " " customWidth);
           if (paintingImageRect == null) return;

           // Usa le dimensioni personalizzate per l'immagine
           paintingImageRect.sizeDelta = new Vector2(customWidth, customHeight);
       }
}