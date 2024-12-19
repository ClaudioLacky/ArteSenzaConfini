using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che mostra i crediti dell'applicazione
/// </summary>
public class CreditsEvents : MonoBehaviour
{
    private UIDocument document;        //  Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentMainMenu; // Componente documento dell'oggetto MainMenu

    private Button buttonCredits;   // Componente bottone del documento UXML

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        document = GetComponent<UIDocument>();  // Per prendere il documento UXML assegnato all'oggetto

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentMainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<UIDocument>();

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonCredits = document.rootVisualElement.Q("ButtonCredits") as Button;
        buttonCredits.RegisterCallback<ClickEvent>(OnButtonCredits);        // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Metodo che gestice l'evento del click sul bottone
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonCredits(ClickEvent evt)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
        documentMainMenu.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Metodo che disabilita l'evento sul bottone
    /// </summary>
    private void OnDisable()
    {
        buttonCredits.UnregisterCallback<ClickEvent>(OnButtonCredits);
    }
}
