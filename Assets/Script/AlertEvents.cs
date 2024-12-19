using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce la finestra di Alert
/// </summary>
public class AlertEvents : MonoBehaviour
{
    public static AlertEvents instance;         // Istanza della classe

    private UIDocument documentAlert;       // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentExternal;        // Componente documento di un altro oggetto

    private Label header;                       // Componente Label del documento UXML per la finestra di Alert

    private Button buttonYes;                   // Componente bottone del documento UXML per la finestra di Alert

    private Button buttonNo;                    // Componente bottone del documento UXML per la finestra di Alert

    private string scena;                       // Stringa contenente il nome della scena


    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;

        documentAlert = GetComponent<UIDocument>();     // Per prendere il documento UXML assegnato all'oggetto

        // Inizializzazione della label da assegnare allo specifica label del documento UXML di cui si sta facendo riferimento
        header = documentAlert.rootVisualElement.Q("HeaderAlert") as Label;

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonYes = documentAlert.rootVisualElement.Q("ButtonYes") as Button;
        buttonYes.RegisterCallback<ClickEvent>(OnButtonYes);    // Aggiunge un gestore di eventi in questo caso
                                                                // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonNo = documentAlert.rootVisualElement.Q("ButtonNo") as Button;
        buttonNo.RegisterCallback<ClickEvent>(OnButtonNo);      // Aggiunge un gestore di eventi in questo caso
                                                                // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Imposta la stringa di quello che conterrà la label
    /// </summary>
    /// <param name="msg">Messaggio dell'alert</param>
    public void SetStringHeader(string msg)
    {
        header.text = msg;
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
    /// Imposta il documento di un altro oggetto
    /// </summary>
    /// <param name="document">Documento dell'altro oggetto</param>
    public void SetDocument(UIDocument document)
    {
        documentExternal = document;
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva le varie azioni
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonYes(ClickEvent evt)
    {
        // Se la scena attuale è il museo torna al Main Menu
        if (scena.Equals("Museo"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        // Se la scena attuale è il Main Menu esci dall'applicazione
        else if (scena.Equals("MainMenu"))
        {
            Application.Quit();
        }
        // Se la scena attuale è il museo ma si è fatto un click sulla porta allora attiva l'interfaccia di feedback
        else if (scena.Equals("MuseoPorta"))
        {
            // Imposta la varianile di controllo in modo che non si possa più cliccare sulla porta
            MouseHoverDoor.instance.SetUserLog(false);

            // Imposta le variabili a false per non far comparire il menù di pausa durante il feedback
            FeedbackEvents.instance.SetFeedback(false);
            LoginEvents.instance.SetLogged(false);
            RegistrationEvents.instance.SetRegistered(false);

            documentAlert.rootVisualElement.style.display = DisplayStyle.None;      // Rende non visibile l'interfaccia dell'alert

            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;   // Rende visibile l'interfaccia del feedback
        }
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva le varie azioni
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonNo(ClickEvent evt)
    {
        // Se la scena attuale è il museo ma si è fatto click sulla porta si torna nel museo senza che si attivi l'interfaccia di feedback
        if (scena.Equals("MuseoPorta"))
        {
            // Reimposta i cursori del mouse come prima
            MouseLook.instance.Start();
            MouseLook.instance.Update();

            Time.timeScale = 1f;  // Sblocca il gioco

            documentAlert.rootVisualElement.style.display = DisplayStyle.None;  // Rende non visibile l'interfaccia dell'alert
        }
        // Altrimenti in ogni altra scena (in particolare nel Main Menu e nel Pause Menu) disabilita l'alert
        else
        {
            documentAlert.rootVisualElement.style.display = DisplayStyle.None;      // Rende non visibile l'interfaccia dell'alert
            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;   // Rende visibile l'interfaccia prima dell'alert
        }
    }

    /// <summary>
    /// Disabilita gli eventi sui bottoni
    /// </summary>
    private void OnDisable()
    {
        buttonYes.UnregisterCallback<ClickEvent>(OnButtonYes);
        buttonNo.UnregisterCallback<ClickEvent>(OnButtonNo);
    }
}
