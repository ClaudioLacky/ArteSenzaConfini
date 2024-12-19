using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe che gestisce l'interfaccia del Main Menu
/// </summary>
public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;            // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentAlert;       // Componente documento dell'oggetto Alert

    private UIDocument documentSettings;    // Componente documento dell'oggetto Settings

    private UIDocument documentCredits;     // Componente documento dell'oggetto Credits

    private Button buttonEnter;             // Componente bottone del documento UXML per entrare nel museo
    private Button buttonSettings;          // Componente bottone del documento UXML per impostare le impostazioni di gioco
    private Button buttonCredits;           // Componente bottone del documento UXML per visualizzare gli autori del progetto
    private Button buttonExit;              // Componente bottone del documento UXML per uscire dall'applicazione

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        document = GetComponent<UIDocument>();      // Per prendere il documento UXML assegnato all'oggetto

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        documentAlert.rootVisualElement.style.display = DisplayStyle.None;  // Rende non visibile all'inizio l'interfaccia dell'alert

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentCredits = GameObject.FindGameObjectWithTag("Credits").GetComponent<UIDocument>();

        documentCredits.rootVisualElement.style.display = DisplayStyle.None;    // Rende non visibile all'inizio l'interfaccia dei credits

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentSettings = GameObject.FindGameObjectWithTag("Settings").GetComponent<UIDocument>();

        documentSettings.rootVisualElement.style.display = DisplayStyle.None;   // Rende non visibile all'inizio l'interfaccia dei settins

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonEnter = document.rootVisualElement.Q("ButtonEnter") as Button;
        buttonEnter.RegisterCallback<ClickEvent>(OnButtonEnter);    // Aggiunge un gestore di eventi in questo caso
                                                                    // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonSettings = document.rootVisualElement.Q("ButtonSettings") as Button;
        buttonSettings.RegisterCallback<ClickEvent>(OnButtonSettings);      // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonCredits = document.rootVisualElement.Q("ButtonCredits") as Button;
        buttonCredits.RegisterCallback<ClickEvent>(OnButtonCredits);    // Aggiunge un gestore di eventi in questo caso
                                                                        // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonExit = document.rootVisualElement.Q("ButtonExit") as Button;
        buttonExit.RegisterCallback<ClickEvent>(OnButtonExit);      // Aggiunge un gestore di eventi in questo caso
                                                                    // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva l'azione impostata
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonEnter(ClickEvent evt)
    {
        SceneManager.LoadScene("Museo");
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva l'azione impostata
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonSettings(ClickEvent evt)
    {
        // Disabilita l'interfaccia del Main Menu e rende visibile l'interfaccia dei Settings
        document.rootVisualElement.style.display = DisplayStyle.None;
        SettingsEvents.instance.SetDocument(document);
        documentSettings.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva l'azione impostata
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonCredits(ClickEvent evt)
    {
        // Disabilita l'interfaccia del Main Menu e rende visibile l'interfaccia dei Credits
        document.rootVisualElement.style.display = DisplayStyle.None;
        documentCredits.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Gestisce l'evento del click sul bottone che attiva l'azione impostata
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonExit(ClickEvent evt)
    {
        // Disabilita l'interfaccia del Main Menu e rende visibile l'interfaccia dell'Alert
        document.rootVisualElement.style.display = DisplayStyle.None;
        AlertEvents.instance.SetDocument(document);
        AlertEvents.instance.SetScena("MainMenu");
        AlertEvents.instance.SetStringHeader("Sei sicuro/a di voler uscire?");
        documentAlert.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Disabilita gli eventi sui bottoni
    /// </summary>
    private void OnDisable()
    {
        buttonEnter.UnregisterCallback<ClickEvent>(OnButtonEnter);
        buttonSettings.UnregisterCallback<ClickEvent>(OnButtonSettings);
        buttonCredits.UnregisterCallback<ClickEvent>(OnButtonCredits);
        buttonExit.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}
