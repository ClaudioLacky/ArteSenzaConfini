using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce l'interfaccia di registrazione dell'utente
/// </summary>
public class RegistrationEvents : MonoBehaviour
{
    public static RegistrationEvents instance;  // Istanza della classe

    private UIDocument documentRegistration;    // Componente che collega l'oggetto ai documenti UXML per il rendering dell'interfaccia
                                                // utente

    private UIDocument documentLogin;           // Componente documento dell'oggetto Login

    private Button buttonRegistration;          // Componente bottone del documento UXML per fare la registrazioe
    private Button buttonReturnRegistration;    // Componente bottone del documento UXML per tornare all'interfaccia di login

    private TextField textFieldName;            // Componente TextField del documento UXML per inserire il nome
    private TextField textFieldSurname;         // Componente TextField del documento UXML per inserire il cognome
    private TextField textFieldEmail;           // Componente TextField del documento UXML per inserire l'email
    private TextField textFieldPassword;        // Componente TextField del documento UXML per inserire la password
    private TextField textFieldNomeUtente;      // Componente TextField del documento UXML per inserire il nome utente

    private bool isRegistered = true;            // Variabile che permette di capire se l'interfaccia di registrazione è attiva

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;

        documentRegistration = GetComponent<UIDocument>();          // Per prendere il documento UXML assegnato all'oggetto

        // Per rendere non visibile all'inizio l'interfaccia di Registrazione
        documentRegistration.rootVisualElement.style.display = DisplayStyle.None;

        // Per prendere il documento UXML assegnato ad un altro oggetto a cui si fa riferimento tramite tag
        documentLogin = GameObject.FindGameObjectWithTag("Login").GetComponent<UIDocument>();

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonRegistration = documentRegistration.rootVisualElement.Q("ButtonRegistration") as Button;
        buttonRegistration.RegisterCallback<ClickEvent>(OnButtonRegistration);      // Aggiunge un gestore di eventi in questo caso
                                                                                    // se si clicca il bottone genera un evento

        // Inizializzazione del bottone da assegnare allo specifico bottone del documento UXML di cui si sta facendo riferimento
        buttonReturnRegistration = documentRegistration.rootVisualElement.Q("ButtonReturnRegistration") as Button;
        buttonReturnRegistration.RegisterCallback<ClickEvent>(OnButtonReturn);      // Aggiunge un gestore di eventi in questo caso
                                                                                    // se si clicca il bottone genera un evento

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldName = documentRegistration.rootVisualElement.Q("NameRegistration") as TextField;

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldSurname = documentRegistration.rootVisualElement.Q("SurnameRegistration") as TextField;

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldEmail = documentRegistration.rootVisualElement.Q("EmailRegistration") as TextField;

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldPassword = documentRegistration.rootVisualElement.Q("PasswordRegistration") as TextField;

        // Inizializzazione della textField da assegnare allo specifico textfield del documento UXML di cui si sta facendo riferimento
        textFieldNomeUtente = documentRegistration.rootVisualElement.Q("NomeutenteRegistration") as TextField;
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che esegue la registrazione
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonRegistration(ClickEvent evt)
    {
        bool controllo = false;     // Variabile di controllo inizialmente impostata a false

        // Controllo se il nome dell'utente è nullo o uno spazio bianco o se la lunghezza è minore di 2
        if (string.IsNullOrWhiteSpace(textFieldName.text) || textFieldName.text.Length < 2)
        {
            controllo = true;
        }

        // Controllo se il cognome dell'utente è nullo o uno spazio bianco o se la lunghezza è minore di 2
        if (string.IsNullOrWhiteSpace(textFieldSurname.text) || textFieldSurname.text.Length < 2)
        {
            controllo = true;
        }

        // Controllo se l'email è nulla o uno spazio bianco o se la lunghezza è minore di 5
        if (string.IsNullOrWhiteSpace(textFieldEmail.text) || textFieldEmail.text.Length < 5)
        {
            controllo = true;
        }

        // Controllo se la password è nulla o uno spazio bianco o se non rispetta i requisti di password sicura tramite il metodo isValid
        if (string.IsNullOrWhiteSpace(textFieldPassword.text) || isValid(textFieldPassword.text))
        {
            controllo = true;
        }

        // Controllo se il nome utente è nullo o uno spazio bianco o se la lunghezza è minore di 5
        if (string.IsNullOrWhiteSpace(textFieldNomeUtente.text) || textFieldNomeUtente.text.Length < 5)
        {
            controllo = true;
        }

        // Se si verifica qualcuna delle condizioni sopra controllate attiva il PopUp con l'avviso all'utente di sistemare
        if (controllo)
        {
            // Disattiva l'interfaccia della registrazione e attiva quella del PopUp impostando i vari messaggi di avviso
            documentRegistration.rootVisualElement.style.display = DisplayStyle.None;
            PopUpManager.instance.SetDocument(documentRegistration);
            PopUpManager.instance.SetControllo(true);
            PopUpManager.instance.ShowDocument();
            PopUpManager.instance.SetStringHeader("Attenzione!");
            PopUpManager.instance.SetStringText("Inserire un nome valido di almeno 2 caratteri\n\n" +
                "Inserire un cognome valido di almeno 2 caratteri\n\n" +
                "Inserire un email valida\n\n" +
                "Inserire un nome utente valido di almeno 5 caratteri\n\n" +
                "Inserire una password valida che deve contenere:\n" +
                "lunghezza minina di 8 caratteri, una lettera maiuscola, una lettera minuscola e un carattere speciale.");
            return;
        }

        // Se il controllo è superato avviene la registrazione
        if (!controllo)
        {
            // Metodo che fa partire la richiesta di registrazione
            StartCoroutine(MySqlManager.instance.RegisterUser(textFieldName.text, textFieldSurname.text, textFieldEmail.text, 
                textFieldPassword.text, textFieldNomeUtente.text, documentRegistration));
        }
    }

    /// <summary>
    /// Metodo che controlla se la password rispetta i requisiti minimi di sicurezza
    /// </summary>
    /// <param name="pass">Password dell'utente</param>
    /// <returns>Le variabili di controllo dei requisiti da rispettare</returns>
    private bool isValid(string pass)
    {
        int minLength = 8;
        bool numero = false;
        bool maiuscola = false;
        bool minuscola = false;
        bool simbolo = false;

        // Controlla se la password ha la lunghezza minima di 8 caratteri
        if (pass.Length >= minLength)
        {
            // Controllo ogni carattere della stringa della password
            for (int i = 0; i < pass.Length; i++)
            {
                // Controlla se ci sono numeri
                if (Char.IsNumber(pass[i]))
                {
                    numero = true;
                }
                else
                {
                    // Controlla se ci sono lettere
                    if (Char.IsLetter(pass[i]))
                    {
                        // Controlla se ci sono lettere maiuscole
                        if (Char.IsUpper(pass[i]))
                        {
                            maiuscola = true;
                        }
                        // Controlla se ci sono lettere minuscole
                        else if (Char.IsLower(pass[i]))
                        {
                            minuscola = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // Controlla se ci sono simboli speciali
                        if (Char.IsSymbol(pass[i]))
                        {
                            simbolo = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }   
        else
        {
            return false;
        }
        return (numero && maiuscola && minuscola && simbolo);
    }

    /// <summary>
    /// Imposta la variabile di controllo
    /// </summary>
    /// <param name="isRegistered">Valore da impostare alla variabile isLogged</param>
    public void SetRegistered(bool isRegistered)
    {
        this.isRegistered = isRegistered;
    }

    /// <summary>
    /// Acquisisce la variabile di controllo
    /// </summary>
    /// <returns>La variabile isRegistered</returns>
    public bool GetRegistered()
    {
        return isRegistered;
    }

    /// <summary>
    /// Metodo che gestisce l'evento del click sul bottone che disattiva l'interfaccia della registrazione e attiva quella del login
    /// </summary>
    /// <param name="evt">Registra l'evento del click</param>
    private void OnButtonReturn(ClickEvent evt)
    {
        // Disattiva l'interfaccia della registrazione e attiva l'interfaccia del login
        documentRegistration.rootVisualElement.style.display = DisplayStyle.None;
        documentLogin.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Metodo che disabilita gli eventi sui bottoni
    /// </summary>
    private void OnDisable()
    {
        buttonRegistration.UnregisterCallback<ClickEvent>(OnButtonRegistration);
        buttonReturnRegistration.UnregisterCallback<ClickEvent>(OnButtonReturn);
    }
}
