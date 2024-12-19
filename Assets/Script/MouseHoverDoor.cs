using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che permette di illuminare gli oggetti porta e gestisce il feedback
/// </summary>
public class MouseHoverDoor : MonoBehaviour
{
    public static MouseHoverDoor instance;      // Istanza della classe

    private Material material;                  // Materiale dell'oggetto

    private UIDocument document;            // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentAlert;       // Componente documento dell'oggetto Alert

    private bool userLog = false;           // Variabile che permette di capire se il login è stato fatto

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        material = new Material(Shader.Find("Standard"));       // Trova e salva il materiale dell'oggetto tramite il nome dello shader

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        document = GameObject.FindGameObjectWithTag("Feedback").GetComponent<UIDocument>();

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        instance = this;
    }

    /// <summary>
    /// Metodo che si attiva quando il puntatore del mouse è sull'oggetto
    /// </summary>
    private void OnMouseEnter()
    {
        // Se il login è stato fatto quando il puntatore del mouse punta l'oggetto, esso cambia colore
        if (userLog)
        {
            // Assegna il colore giallo all'oggetto
            material.color = Color.yellow;

            // Assegna il materiale al renderer dell'oggetto
            GetComponent<Renderer>().material = material;
        }
    }

    /// <summary>
    /// Metodo che si attiva quando il puntatore del mouse non punta più sull'oggetto
    /// </summary>
    private void OnMouseExit()
    {
        // Se il login è stato fatto quando il puntatore del mouse non punta l'oggetto torna ad avere il colore originale
        if (userLog)
        {
            material.color = Color.black;

            // Assegna il materiale al renderer dell'oggetto
            GetComponent<Renderer>().material = material;
        }
    }

    /// <summary>
    /// Metodo che si attiva quando si clicca sull'oggetto con il mouse
    /// </summary>
    private void OnMouseUpAsButton()
    {
        // Se il login è stato fatto attiva il messaggio di alert per scegliere se passare al feedback o meno
        if (userLog)
        {
            // Impostazione del messaggio di Alert
            AlertEvents.instance.SetStringHeader("Sei sicuro di voler terminare l'esperienza?");
            AlertEvents.instance.SetDocument(document);
            AlertEvents.instance.SetScena("MuseoPorta");

            // Riabilita i cursori standard di Unity
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            // Pausa il gioco
            Time.timeScale = 0f;
            
            documentAlert.rootVisualElement.style.display = DisplayStyle.Flex;  // Rende visibile l'interfaccia del feedback
        }
    }

    /// <summary>
    /// Imposta la variabile userLog
    /// </summary>
    /// <param name="userLog">Determina se è stato fatto il login</param>
    public void SetUserLog(bool userLog)
    {
        this.userLog = userLog;
    }
}
