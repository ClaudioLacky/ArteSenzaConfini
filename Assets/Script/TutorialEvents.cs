using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce l'interfaccia del tutorial
/// </summary>
public class TutorialEvents : MonoBehaviour
{
    private UIDocument document;        // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private Button buttonTutorial;      // Componente bottone del documento UXML per disattivare il tutorial e attivare il player

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        document = GetComponent<UIDocument>();      // Per prendere il documento UXML assegnato all'oggetto

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonTutorial = document.rootVisualElement.Q("ButtonTutorial") as Button;
        buttonTutorial.RegisterCallback<ClickEvent>(OnButtonTutorial);      // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che disattiva l'interfaccia del tutorial e attiva il player nel museo
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonTutorial(ClickEvent evt)
    {
        // Reimposta i cursori del mouse come prima
        MouseLook.instance.Start();
        MouseLook.instance.Update();
        
        Time.timeScale = 1f;  // Sblocca il gioco
        
        document.rootVisualElement.style.display = DisplayStyle.None;   // Rende non visibile l'interfaccia del tutorial
    }

    /// <summary>
    /// Metodo che disabilita l'evento sul bottone
    /// </summary>
    private void OnDisable()
    {
        buttonTutorial.UnregisterCallback<ClickEvent>(OnButtonTutorial);
    }
}
