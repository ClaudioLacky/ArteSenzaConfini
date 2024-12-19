using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce la finestra di PopUp
/// </summary>
public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;    // Istanza della classe

    private UIDocument documentInternal;    // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentExternal;    // Componente documento di un altro oggetto

    private Label header;                   // Componente Label del documento UXML per la finestra di PopUp

    private Label textPopUp;                // Componente Label del documento UXML per la finestra di PopUp

    private Button buttonPopUp;             // Componente button del documento UXML per la finestra di PopUp

    private string scena = "";              // Stringa contenente il nome della scena

    private bool controllo = false;         // Variabile di controllo per capire se è un messaggio di richiamo o di conferma

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;

        documentInternal = GetComponent<UIDocument>();      // Per prendere il documento UXML assegnato all'oggetto

        documentInternal.rootVisualElement.style.display = DisplayStyle.None;   // Rende non visibile all'inizio l'interfaccia del PopUp

        // Inizializzazione della label da assegnare alla specifica label del documento UXML di cui si sta facendo riferimento
        header = documentInternal.rootVisualElement.Q("HeaderPopUp") as Label;

        // Inizializzazione della label da assegnare alla specifica label del documento UXML di cui si sta facendo riferimento
        textPopUp = documentInternal.rootVisualElement.Q("TextPopUp") as Label;

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonPopUp = documentInternal.rootVisualElement.Q("ButtonPopUp") as Button;
        buttonPopUp.RegisterCallback<ClickEvent>(OnButtonExit);     // Aggiunge un gestore di eventi in questo caso
                                                                    // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Imposta il documento di un altro oggetto
    /// </summary>
    /// <param name="document">Documento dell'altro oggetto</param>
    public void SetDocument(UIDocument document)
    {
        documentExternal = document;
    }

    /// <summary>
    /// Imposta la stringa di quello che conterrà la label
    /// </summary>
    /// <param name="msg">Titolo del PopUp</param>
    public void SetStringHeader(string msg)
    {
        header.text = msg;
    }

    /// <summary>
    /// Imposta la stringa di quello che conterrà la label
    /// </summary>
    /// <param name="msg">Avviso del PopUp</param>
    public void SetStringText(string msg)
    {
        textPopUp.text = msg;
    }

    /// <summary>
    /// Imposta la variabile di controllo
    /// </summary>
    /// <param name="controllo">Variabile che controllo che tipo di PopUp è</param>
    public void SetControllo(bool controllo)
    {
        this.controllo = controllo;
    }

    /// <summary>
    /// Rende visibile il documento assegnato all'oggetto
    /// </summary>
    public void ShowDocument()
    {
        documentInternal.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Imposta la stringa della scena 
    /// </summary>
    /// <param name="scena">Nome della scena</param>
    public void SetScena(string scena)
    {
        this.scena = scena;
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva le varie azioni
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonExit(ClickEvent evt)
    {
        documentInternal.rootVisualElement.style.display = DisplayStyle.None;   // Rende non visibile l'interfaccia del PopUp

        // Controlla se il messaggio da far apparire è un richiamo all'attenzione dell'utente
        if (controllo)
        {
            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        // Se non è un controllo ma la scena è il museo e si è fatto click sulla porta allora si è fatto il feedback
        // e deve tornare al Main Menu
        else if (scena.Equals("MuseoPorta"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        // Altrimenti è stato fatto il login o la registrazione e deve tornare il comando al player nel museo
        else
        {
            // Riabilita la possibilità di cliccare la "P" per attivare il Pause Menu
            LoginEvents.instance.SetLogged(true);
            RegistrationEvents.instance.SetRegistered(true);
            FeedbackEvents.instance.SetFeedback(true);

            // Reimposta i cursori del mouse come prima
            MouseLook.instance.Start();
            MouseLook.instance.Update();

            // Disabilita la possibilità di cliccare sui PC
            MouseHoverPC.instance.SetUserLog(false);

            // Abilità la possibilità di cliccare sulla porta per uscire dall'esperienza di gioco
            MouseHoverDoor.instance.SetUserLog(true);

            Time.timeScale = 1f;  // Sblocca il gioco
        }
    }

    /// <summary>
    /// Disabilita l'evento sul bottone
    /// </summary>
    private void OnDisable()
    {
        buttonPopUp.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}