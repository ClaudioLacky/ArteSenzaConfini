using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che permette di illuminare gli oggetti PC e visualizza il login
/// </summary>
public class MouseHoverPC : MonoBehaviour
{
    public static MouseHoverPC instance;        // Istanza della classe

    private Material material;                  // Materiale dell'oggetto

    private UIDocument document;            // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private bool userLog = true;            // Variabile che permette di capire se il login � stato fatto

    // Metodo Awake rispetto a Start � chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        material = new Material(Shader.Find("Standard"));   // Trova e salva il materiale dell'oggetto tramite il nome dello shader

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        document = GameObject.FindGameObjectWithTag("Login").GetComponent<UIDocument>();

        instance = this;
    }

    /// <summary>
    /// Metodo che si attiva quando il puntatore del mouse � sull'oggetto
    /// </summary>
    private void OnMouseEnter()
    {
        // Se il login non � stato fatto quando il puntatore del mouse punta l'oggetto, esso cambia colore
        if (userLog)
        {
            // Assegna il colore giallo all'oggetto
            material.color = Color.yellow;

            // Assegna il materiale al renderer dell'oggetto
            GetComponent<Renderer>().material = material;
        }
    }

    /// <summary>
    /// Metodo che si attiva quando il puntatore del mouse non punta pi� sull'oggetto
    /// </summary>
    private void OnMouseExit()
    {
        // Se il login non � stato fatto quando il puntatore del mouse non punta l'oggetto torna ad avere il colore originale
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
        // Se il login non � stato fatto attiva l'interfaccia del login per permettere l'autenticazione
        if (userLog)
        {
            // Imposta le variabili a false per non far comparire il men� di pausa durante il login
            LoginEvents.instance.SetLogged(false);
            RegistrationEvents.instance.SetRegistered(false);
            FeedbackEvents.instance.SetFeedback(false);

            // Riabilita i cursori standard di Unity
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            // Pausa il gioco
            Time.timeScale = 0f;

            document.rootVisualElement.style.display = DisplayStyle.Flex;   // Rende visibile l'interfaccia utente del login
        }
    }

    /// <summary>
    /// Imposta la variabile userLog
    /// </summary>
    /// <param name="userLog">Determina se � stato fatto il login</param>
    public void SetUserLog(bool userLog)
    {
        this.userLog = userLog;
    }
}