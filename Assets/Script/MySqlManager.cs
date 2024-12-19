using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

/// <summary>
/// Classe che gestisce login, registrazione e invio del feedback tramite PHP al server su cui è collocato il database
/// </summary>
public class MySqlManager : MonoBehaviour
{
    public static MySqlManager instance;        // Istanza della classe

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Metodo che permette di effettuare la registrazione inviando i dati al database tramite PHP
    /// </summary>
    /// <param name="name">Nome dell'utente</param>
    /// <param name="surname">Cognome dell'utente</param>
    /// <param name="email">Email dell'utente</param>
    /// <param name="password">Password dell'utente</param>
    /// <param name="nomeUtente">Nome utente dell'utente</param>
    /// <param name="document">Documento dell'interfaccia di registrazione</param>
    /// <returns></returns>
    public IEnumerator RegisterUser(string name, string surname, string email, string password, string nomeUtente, UIDocument document)
    {
        WWWForm form = new WWWForm();               // Creazione del form che permette l'invio dei dati al database tramite PHP
        form.AddField("nome", name);                // Inserimento nel form del nome dell'utente
        form.AddField("cognome", surname);          // Inserimento nel form del cognome dell'utente
        form.AddField("mail", email);               // Inserimento nel form dell'email dell'utente
        form.AddField("password", password);        // Inserimento nel form della password dell'utente
        form.AddField("nomeUtente", nomeUtente);    // Inserimento nel form del nome utente dell'utente

        // Invio dei dati tramite richiesta di invio POST al file PHP del database
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ArteSenzaConfini/Register_User.php", form))
        {
            yield return www.SendWebRequest();              // Attesa della risposta della richiesta di invio

            string controllo = www.downloadHandler.text;    // Si salva nella stringa controllo il risultato dell'operazione

            // Controllo della risposta se la richiesta non va a buon fine compare l'interfaccia PopUp di avviso
            // se va a buon fine compare l'interfaccia PopUp di buona riuscita con la possibilità di tornare ad usare il player
            switch (controllo)
            {
                case "1: Connection failed":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Connessione al server fallita");
                    break;

                case "2: Email check failed":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Controllo dell'email fallito");
                    break;

                case "3: Email already exists":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Utente già esistente");
                    break;

                case "4: Insert user query failed":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Invio dei dati fallito");
                    break;

                case "success":
                    Debug.Log("Successfully Registered!");
                    BarriereManager.instance.controlloBarriere();
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Registrazione effettuata");
                    PopUpManager.instance.SetStringText("Registrazione effettuata con successo, benvenuto/a " + nomeUtente);
                    break;
            }
        }
    }

    /// <summary>
    /// Metodo che permette di effettuare il login inviando i dati al database tramite PHP
    /// </summary>
    /// <param name="email">Email dell'utente</param>
    /// <param name="password">Password dell'utente</param>
    /// <param name="document">Documento dell'interfaccia di login</param>
    /// <returns></returns>
    public IEnumerator LoginUser(string email, string password, UIDocument document)
    {
        WWWForm form = new WWWForm();                   // Creazione del form che permette l'invio dei dati al database tramite PHP
        form.AddField("mail", email);                   // Inserimento nel form dell'email dell'utente
        form.AddField("passwordLogin", password);       // Inserimento nel form della password dell'utente

        // Invio dei dati tramite richiesta di invio POST al file PHP del database
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ArteSenzaConfini/Login_User.php", form))
        {
            yield return www.SendWebRequest();              // Attesa della risposta della richiesta di invio

            string controllo = www.downloadHandler.text;    // Si salva nella stringa controllo il risultato dell'operazione

            // Controllo della risposta se la richiesta non va a buon fine compare l'interfaccia PopUp di avviso
            // se va a buon fine compare l'interfaccia PopUp di buona riuscita con la possibilità di tornare ad usare il player
            switch (controllo)
            {
                case "1: Connection failed":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Connessione al server fallita");
                    break;

                case "2: Email check failed":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Controllo dell'email fallito");
                    break;

                case "5: Either no user with email, or more that one":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Email errata o inesistente");
                    break;

                case "6: Incorrect password":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Password errata");
                    break;

                case "success":
                    Debug.Log("Successfully Login!");
                    BarriereManager.instance.controlloBarriere();
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Login effetuato");
                    PopUpManager.instance.SetStringText("Benvenuto/a");
                    break;
            }
        }
    }

    /// <summary>
    /// Metodo che permette di effettuare il feedback inviando i dati al database tramite PHP
    /// </summary>
    /// <param name="nomeUtente">Nome utente dell'utente</param>
    /// <param name="feedback">Recensione dell'utente</param>
    /// <param name="document">Documento dell'interfaccia di feedback</param>
    /// <returns></returns>
    public static IEnumerator sendToFeedback(string nomeUtente, string feedback, UIDocument document)
    {
        WWWForm form = new WWWForm();                   // Creazione del form che permette l'invio dei dati al database tramite PHP
        form.AddField("nomeUtente", nomeUtente);        // Inserimento nel form del nome utente dell'utente
        form.AddField("feedback", feedback);            // Inserimento nel form della recensione dell'utente

        // Invio dei dati tramite richiesta di invio POST al file PHP del database
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ArteSenzaConfini/Register_Feedback.php", form))
        {
            yield return www.SendWebRequest();              // Attesa della risposta della richiesta di invio

            string controllo = www.downloadHandler.text;    // Si salva nella stringa controllo il risultato dell'operazione

            // Controllo della risposta se la richiesta non va a buon fine compare l'interfaccia PopUp di avviso
            // se va a buon fine compare l'interfaccia PopUp di buona riuscita con ritorno al MainMenu
            switch (controllo)
            {
                case "1: Connection failed":
                    Debug.Log("Failed to Feedback!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Connessione al server fallita");
                    break;

                case "2: User check failed":
                    Debug.Log("Failed to Feedback!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Controllo del nome utente fallito");
                    break;

                case "5: Either no user with username, or more that one":
                    Debug.Log("Failed to Feedback!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Nome utente errato o inesistente");
                    break;

                case "4: Insert feedback query failed":
                    Debug.Log("Failed to Feedback!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!");
                    PopUpManager.instance.SetStringText("Invio dei dati fallito");
                    break;

                case "success":
                    Debug.Log("Successfully Feedback!");
                    BarriereManager.instance.controlloBarriere();
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetScena("MuseoPorta");
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Feedback effettuato");
                    PopUpManager.instance.SetStringText("Grazie per aver partecipato!");
                    break;
            }
        }
    }
}