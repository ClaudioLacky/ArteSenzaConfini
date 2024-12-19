using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce tutta la scena del museo e l'interazione con i quadri
/// </summary>
public class PictureInteraction : MonoBehaviour
{
    public GameObject panel;                    // Pannello che si apre
    public UnityEngine.UI.Image displayImage;   // Immagine ingrandita nel pannello
    public TextMeshProUGUI displayText;         // Testo nel pannello
    public TextMeshProUGUI descriptionText;     // Descrizione nel pannello
    public float maxDistance = 5f;
    public float wordDelay = 0.6f;              // Ritardo tra una parola e l'altra

    public string id = "Clickable";             // Tag per identificare il quadro

    private bool isPanelActive = false;         // Stato del pannello
    private Camera mainCamera;

    private AudioManager audioManager;          // Riferimento all'AudioManager
    private Coroutine descriptionCoroutine;     // Riferimento alla coroutine attiva

    private AudioSource audioSource;            // Componente che permette di riprodurre audio

    private UIDocument documentTutorial;        // Documento per l'interfaccia grafica che fa riferimento all'oggetto Tutorial

    private UIDocument documentPause;           // Documento per l'interfaccia grafica che fa riferimento all'oggetto PauseMenu

    private UIDocument documentAlert;           // Documento per l'interfaccia grafica che fa riferimento all'oggetto Alert

    private UIDocument documentFeedback;        // Documento per l'interfaccia grafica che fa riferimento all'oggetto Feedback

    // Metodo Start che si avvia all'esecuzione dello script
    void Start()
    {

        // Riabilita i cursori standard di Unity
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        mainCamera = Camera.main;
        panel.SetActive(isPanelActive); // Nascondi il pannello all'inizio
        audioManager = FindObjectOfType<AudioManager>(); // Trova l'AudioManager nella scena

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentPause = GameObject.FindGameObjectWithTag("Pause").GetComponent<UIDocument>();

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentTutorial = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<UIDocument>();

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentFeedback = GameObject.FindGameObjectWithTag("Feedback").GetComponent<UIDocument>();

        // Trova il componente AudioSource assegnato all'oggetto Sottofondo tramite tag
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        audioSource.Play();     // Avvia l'audio dell'AudioSource in questo caso la musica di sottofondo del museo

        documentPause.rootVisualElement.style.display = DisplayStyle.None;      // Imposta non visibile il documento nella scena

        documentAlert.rootVisualElement.style.display = DisplayStyle.None;      // Imposta non visibile il documento nella scena

        documentFeedback.rootVisualElement.style.display = DisplayStyle.None;   // Imposta non visibile il documento nella scena

        documentTutorial.rootVisualElement.style.display = DisplayStyle.Flex;   // Imposta visibile il documento nella scena

        // Pausa il gioco
        Time.timeScale = 0f;
    }

    // Metodo che si ripete ciclicamente per controllare e fare operazioni che avviene una volta per frame
    // e che determina cosa avviene in scena
    void Update()
    {
        // Controlla se è stato premuto il tasto sinistro del mouse sul quadro e se il pannello della descrizione non è attivo
        if (Input.GetMouseButtonDown(0) && !isPanelActive) // Click sinistro
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);     // Imposto un punto fisso della telecamera
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance)) // Cambia 10f con la distanza massima
            {
                if (hit.collider.CompareTag(id)) // Assicurati che il quadro abbia questo tag
                {
                    // Ottieni i dati del quadro
                    PictureData data = hit.collider.GetComponent<PictureData>();
                    if (data != null)
                    {
                        OpenPanel(data);    // Metodo per aprire il pannello
                    }
                }
            }
        }
        // Se viene premuto il tasto "P" della tastiera e non sono attivi login, registrazione e feedback compare il menù di pausa
        else if(Input.GetKeyDown(KeyCode.P) && LoginEvents.instance.GetLogged() && RegistrationEvents.instance.GetRegistered() 
            && FeedbackEvents.instance.GetFeedback())
        {
            // Pausa il gioco
            Time.timeScale = 0f;

            // Riabilita i cursori standard di Unity
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            documentPause.rootVisualElement.style.display = DisplayStyle.Flex; // Imposta visibile il documento del menù di pausa
                                                                               // nella scena
        }
        // Se il pannello è attivo e l'utente preme il tasto "ESC" della tastiera chiude il pannello della descrizione del quadro
        else if (isPanelActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();   // Metodo per chiudere il pannello
        }
    }
    
    /// <summary>
    /// Apre il pannello e imposta tutti i dati del quadro passati
    /// </summary>
    /// <param name="data">Contiene tutti i dati sul quadro</param>
    void OpenPanel(PictureData data)
    {
        audioSource.Pause();    // Mette in pausa la musica di sottofondo del museo

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

    /// <summary>
    /// Chiude il pannello
    /// </summary>
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

        // Riporta il cursore del mouse come prima
        MouseLook.instance.Start();
        MouseLook.instance.Update();

        audioSource.UnPause();  // Fa ripartire la musica di sottofondo
    }
    
    /// <summary>
    /// Imposta il testo della descrizione del quadro facendolo comparire man mano
    /// </summary>
    /// <param name="description">La descrizione del quadro</param>
    /// <returns>Termina quando la descrizione è terminata</returns>
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
