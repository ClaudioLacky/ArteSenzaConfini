using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce l'interfaccia di login dell'utente
/// </summary>
public class LoginEvents : MonoBehaviour
{
    public static LoginEvents instance;     // Istanza della classe

    private UIDocument documentLogin;       // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia utente

    private UIDocument documentRegistration; // Componente documento dell'oggetto Registration

    private Button buttonAccessoLogin;          // Componente bottone del documento UXML per fare il login
    private Button buttonRegistrationLogin;     // Componente bottone del documento UXML per fare la registrazione
    private Button buttonReturnLogin;           // Componente bottone del documento UXML per tornare nel museo senza fare l'autenticazione

    private TextField textFieldEmail;           // Componente TextField del documento UXML per inserire l'email
    private TextField textFieldPassword;        // Componente TextField del documento UXML per inserire la password

    private bool isLogged = true;               // Variabile che permette di capire se l'interfaccia di login è attiva

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;

        documentLogin = GetComponent<UIDocument>();     // Per prendere il documento UXML assegnato all'oggetto

        // Per rendere non visibile all'inizio l'interfaccia di Login
        documentLogin.rootVisualElement.style.display = DisplayStyle.None;

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentRegistration = GameObject.FindGameObjectWithTag("Registration").GetComponent<UIDocument>();

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonAccessoLogin = documentLogin.rootVisualElement.Q("ButtonAccessoLogin") as Button;
        buttonAccessoLogin.RegisterCallback<ClickEvent>(OnButtonAccesso);   // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonRegistrationLogin = documentLogin.rootVisualElement.Q("ButtonRegistrationLogin") as Button;
        buttonRegistrationLogin.RegisterCallback<ClickEvent>(OnButtonRegistration);     // Aggiunge un gestore di eventi in questo caso
                                                                                        // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonReturnLogin = documentLogin.rootVisualElement.Q("ButtonReturnLogin") as Button;
        buttonReturnLogin.RegisterCallback<ClickEvent>(OnButtonReturn);     // Aggiunge un gestore di eventi in questo caso
                                                                            // se si clicca il bottone genera un evento

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldEmail = documentLogin.rootVisualElement.Q("EmailLogin") as TextField;

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldPassword = documentLogin.rootVisualElement.Q("PasswordLogin") as TextField;
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che esegue il login
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonAccesso(ClickEvent evt)
    {
        // Metodo che fa partire la richiesta di login
        StartCoroutine(MySqlManager.instance.LoginUser(textFieldEmail.text, textFieldPassword.text, documentLogin));
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che attiva l'interfaccia della registrazione
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonRegistration(ClickEvent evt)
    {
        documentLogin.rootVisualElement.style.display = DisplayStyle.None;
        documentRegistration.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che disattiva l'interfaccia del login e sblocca il gioco nel museo
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonReturn(ClickEvent evt)
    {
        // Imposta le variabili di controllo in modo che si possa abilitare nuovamente il menù di pausa
        isLogged = true;
        RegistrationEvents.instance.SetRegistered(true);
        FeedbackEvents.instance.SetFeedback(true);

        // Reimposta i cursori del mouse come prima
        MouseLook.instance.Start();
        MouseLook.instance.Update();
        Time.timeScale = 1f;  // Sblocca il gioco

        // Rende non visibile l'interfaccia di login
        documentLogin.rootVisualElement.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Imposta la variabile di controllo
    /// </summary>
    /// <param name="isLogged">Valore da impostare alla variabile isLogged</param>
    public void SetLogged(bool isLogged)
    {
        this.isLogged = isLogged;
    }

    /// <summary>
    /// Acquisisce la variabile di controllo
    /// </summary>
    /// <returns>La variabile isLogged</returns>
    public bool GetLogged()
    {
        return this.isLogged;
    }

    /// <summary>
    /// Metodo che disabilita gli eventi sui bottoni
    /// </summary>
    private void OnDisable()
    {
        buttonAccessoLogin.UnregisterCallback<ClickEvent>(OnButtonAccesso);
        buttonRegistrationLogin.UnregisterCallback<ClickEvent>(OnButtonRegistration);
        buttonReturnLogin.UnregisterCallback<ClickEvent>(OnButtonReturn);
    }
}
