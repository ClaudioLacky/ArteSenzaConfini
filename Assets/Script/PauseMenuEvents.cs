using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce l'interfaccia del Pause Menu
/// </summary>
public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument documentPause;       // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentSettings;    // Componente documento dell'oggetto Settings

    private UIDocument documentAlert;       // Componente documento dell'oggetto Alert

    private Button buttonReturn;            // Componente bottone del documento UXML per uscire dal menù di pausa

    private Button buttonSettings;          // Componente bottone del documento UXML per modificare le impostazioni di gioco

    private Button buttonExit;              // Componente bottone del documento UXML per uscire dal museo e tornare al Main Menu

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        documentPause = GetComponent<UIDocument>();     // Per prendere il documento UXML assegnato all'oggetto

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentSettings = GameObject.FindGameObjectWithTag("Settings").GetComponent<UIDocument>();

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonReturn = documentPause.rootVisualElement.Q("ButtonReturn") as Button;
        buttonReturn.RegisterCallback<ClickEvent>(OnButtonReturn);      // Aggiunge un gestore di eventi in questo caso
                                                                        // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonSettings = documentPause.rootVisualElement.Q("ButtonSettings") as Button;
        buttonSettings.RegisterCallback<ClickEvent>(OnButtonSettings);      // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonExit = documentPause.rootVisualElement.Q("ButtonExit") as Button;
        buttonExit.RegisterCallback<ClickEvent>(OnButtonExit);      // Aggiunge un gestore di eventi in questo caso
                                                                    // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonReturn(ClickEvent evt)
    {
        // Disattiva l'interfaccia del menù di pausa e riabilita il player per muoversi nel museo
        Time.timeScale = 1f;  // Sblocca il gioco

        // Reimposta i cursori del mouse come prima
        MouseLook.instance.Start();
        MouseLook.instance.Update();
        
        documentPause.rootVisualElement.style.display = DisplayStyle.None;      // Rende non visibile l'interfaccia del Pause Menu
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonSettings(ClickEvent evt)
    {
        // Disattiva l'interfaccia del menù di pausa e abilita l'interfaccia dei Settings
        documentPause.rootVisualElement.style.display = DisplayStyle.None;
        SettingsEvents.instance.SetDocument(documentPause);
        documentSettings.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonExit(ClickEvent evt)
    {
        // Disattiva l'interfaccia del menù di pausa e abilita l'interfaccia Alert con i rispettivi messaggi di avviso
        documentPause.rootVisualElement.style.display = DisplayStyle.None;
        AlertEvents.instance.SetDocument(documentPause);
        AlertEvents.instance.SetScena("Museo");
        AlertEvents.instance.SetStringHeader("Sei sicuro/a di voler tornare al menù principale?");
        documentAlert.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Disabilita gli eventi sui bottoni
    /// </summary>
    private void OnDisable()
    {
        buttonReturn.UnregisterCallback<ClickEvent>(OnButtonReturn);
        buttonSettings.UnregisterCallback<ClickEvent>(OnButtonSettings);
        buttonExit.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}
