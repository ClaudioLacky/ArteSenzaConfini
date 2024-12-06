using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PictureInteraction : MonoBehaviour
{
    public GameObject panel; // Pannello che si apre
    public Image displayImage; // Immagine ingrandita nel pannello
    public TextMeshProUGUI displayText; // Testo nel pannello
    public TextMeshProUGUI descriptionText; // Descrizione nel pannello
    public float maxDistance = 5f;

    public string id = "Clickable"; // Tag per identificare il quadro

    private bool isPanelActive = false; // Stato del pannello
    private Camera mainCamera;

    private AudioManager audioManager; // Riferimento all'AudioManager

    void Start()
    {
        mainCamera = Camera.main;
        panel.SetActive(isPanelActive); // Nascondi il pannello all'inizio
        audioManager = FindObjectOfType<AudioManager>(); // Trova l'AudioManager nella scena
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPanelActive) // Click sinistro
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance)) // Cambia 10f con la distanza massima
            {
                if (hit.collider.CompareTag(id)) // Assicurati che il quadro abbia questo tag
                {
                    // Ottieni i dati del quadro
                    PictureData data = hit.collider.GetComponent<PictureData>();
                    if (data != null)
                    {
                        OpenPanel(data);
                    }
                }
            }
        }

        if (isPanelActive && Input.GetKeyDown(KeyCode.Escape)) // Chiudi pannello con ESC
        {
            ClosePanel();
        }
    }

    void OpenPanel(PictureData data)
    {
        // Imposta immagine, titolo, descrizione e dimensione dell'immagine
        displayImage.sprite = data.image;
        displayImage.rectTransform.sizeDelta = data.size;
        displayText.text = data.title;
        descriptionText.text = data.description;

        // Controlla se il PictureData ha un AudioClip e passa l'audio all'AudioManager
        if (data.audioClip != null)
        {
            audioManager.UpdateAudio(data.audioClip); // Passa l'audio clip del quadro all'AudioManager
        }
        else
        {
            audioManager.StopAudio(); // Se non c'è audio, ferma qualsiasi audio in riproduzione
        }

        // Mostra il pannello
        isPanelActive = true;
        panel.SetActive(isPanelActive);

        // Pausa il gioco
        Time.timeScale = 0f;
    }

    void ClosePanel()
    {
        // Nasconde il pannello e riprende il gioco
        isPanelActive = false;
        panel.SetActive(isPanelActive);
        Time.timeScale = 1f;  // Sblocca il gioco

        // Ferma l'audio
        audioManager.StopAudio();
    }
}




