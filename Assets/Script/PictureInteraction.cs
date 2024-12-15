using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PictureInteraction : MonoBehaviour
{
    public GameObject panel; // Pannello che si apre
    public UnityEngine.UI.Image displayImage; // Immagine ingrandita nel pannello
    public TextMeshProUGUI displayText; // Testo nel pannello
    public TextMeshProUGUI descriptionText; // Descrizione nel pannello
    public float maxDistance = 5f;
    public float wordDelay = 0.6f; // Ritardo tra una parola e l'altra

    public string id = "Clickable"; // Tag per identificare il quadro

    private bool isPanelActive = false; // Stato del pannello
    private Camera mainCamera;

    private AudioManager audioManager; // Riferimento all'AudioManager
    private Coroutine descriptionCoroutine; // Riferimento alla coroutine attiva

    //private GameObject pauseMenu;

    private UIDocument documentPause;

    void Start()
    {
        mainCamera = Camera.main;
        panel.SetActive(isPanelActive); // Nascondi il pannello all'inizio
        audioManager = FindObjectOfType<AudioManager>(); // Trova l'AudioManager nella scena

        documentPause = GameObject.FindGameObjectWithTag("Pause").GetComponent<UIDocument>();

        //pauseMenu.SetActive(false);
        documentPause.rootVisualElement.style.display = DisplayStyle.None;
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
        else if(Input.GetKeyDown(KeyCode.P))
        {
            // Pausa il gioco
            Time.timeScale = 0f;

            // Riabilita i cursori standard di Unity
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            //pauseMenu.SetActive(true);
            documentPause.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        if (isPanelActive && Input.GetKeyDown(KeyCode.Escape)) // Chiudi pannello con ESC
        {
            ClosePanel();
        }
    }

    void OpenPanel(PictureData data)
    {
        // Imposta immagine, titolo e dimensione dell'immagine
        displayImage.sprite = data.image;
        displayImage.rectTransform.sizeDelta = data.size;
        displayText.text = data.title;

        // Avvia la visualizzazione graduale della descrizione
        if (descriptionCoroutine != null)
        {
            StopCoroutine(descriptionCoroutine);
        }
        descriptionCoroutine = StartCoroutine(DisplayDescription(data.description));

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

        // Ferma la visualizzazione della descrizione
        if (descriptionCoroutine != null)
        {
            StopCoroutine(descriptionCoroutine);
            descriptionCoroutine = null;
        }
    }

    IEnumerator DisplayDescription(string description)
    {
        descriptionText.text = ""; // Pulisci il testo iniziale
        string[] words = description.Split(' '); // Dividi la descrizione in parole
        foreach (string word in words)
        {
            descriptionText.text += word + " "; // Aggiungi una parola
            yield return new WaitForSecondsRealtime(wordDelay); // Aspetta un intervallo di tempo
        }
    }
}
