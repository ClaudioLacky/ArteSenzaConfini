using OpenCover.Framework.Model;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

/// <summary>
/// Classe che gestisce l'interfaccia del feedback
/// </summary>
public class FeedbackEvents: MonoBehaviour
{
    public static FeedbackEvents instance;      // Istanza della classe

    private UIDocument document;            // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente  

    private TextField NomeUtente;               // Componente TextField del documento UXML per inserire il nome utente
    private TextField textFieldFeedback;        // Componente TextField del documento UXML per inserire la recensione

    private Button buttonFeedback;              // Componente bottone del documento UXML per inviare il feedback

    private bool isFeedback = true;             // Variabile che permette di capire se l'interfaccia di feedback è attiva

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
	{
        instance = this;

        document = GetComponent<UIDocument>();      // Per prendere il documento UXML assegnato all'oggetto

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        NomeUtente = document.rootVisualElement.Q("NomeUtente") as TextField;

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldFeedback = document.rootVisualElement.Q("TextFeedback") as TextField;

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonFeedback = document.rootVisualElement.Q("ButtonFeedback") as Button;
        buttonFeedback.RegisterCallback<ClickEvent>(OnButtonFeedback);      // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva le varie azioni per inviare il feedback
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonFeedback(ClickEvent evt)
    {
        bool controllo = false;     // Variabile di controllo inizialmente impostata a false

        // Controllo se il nome utente è nullo o uno spazio bianco o se la lunghezza è minore di 5
        if (string.IsNullOrWhiteSpace(NomeUtente.text) || NomeUtente.text.Length < 5)
        {
            controllo = true;
        }

        // Controllo se la recensione è nulla o uno spazio bianco o se la lunghezza è minore di 5
        if (string.IsNullOrWhiteSpace(textFieldFeedback.text) || textFieldFeedback.text.Length < 5)
        {
            controllo = true;
        }

        // Se si verifica qualcuna delle condizioni sopra controllate attiva il PopUp con l'avviso all'utente di sistemare
        if (controllo)
        {
            // Disattiva l'interfaccia del feedback e attiva quella del PopUp impostando i vari messaggi di avviso
            document.rootVisualElement.style.display = DisplayStyle.None;
            PopUpManager.instance.SetDocument(document);
            PopUpManager.instance.SetControllo(true);
            PopUpManager.instance.ShowDocument();
            PopUpManager.instance.SetStringHeader("Attenzione!");
            PopUpManager.instance.SetStringText("Inserire un nome utente valido di almeno 5 caratteri!\n\n" +
                "Inserire una descrizione valida di almeno 5 caratteri!");
            return;
        }

        // Se il controllo è superato invia la recensione
        if (!controllo)
        {
            // Metodo che fa partire la richiesta di invio del feedback
            StartCoroutine(MySqlManager.sendToFeedback(NomeUtente.text, textFieldFeedback.text, document));
        }
    }

    /// <summary>
    /// Imposta la variabile di controllo
    /// </summary>
    /// <param name="isFeedback">Valore da impostare alla variabile isFeedback</param>
    public void SetFeedback(bool isFeedback)
    {
        this.isFeedback = isFeedback;
    }

    /// <summary>
    /// Acquisisce la variabile di controllo
    /// </summary>
    /// <returns>La variabile isFeedback</returns>
    public bool GetFeedback()
    {
        return this.isFeedback;
    }

    /// <summary>
    /// Metodo che disabilita l'evento sul bottone
    /// </summary>
    private void OnDisable()
    {
        buttonFeedback.UnregisterCallback<ClickEvent>(OnButtonFeedback);
    }
}
