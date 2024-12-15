using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MySqlManager : MonoBehaviour
{
    public static MySqlManager instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator RegisterUser(string name, string surname, string email, string password, string nomeUtente, UIDocument document)
    {
        WWWForm form = new WWWForm();
        form.AddField("nome", name);
        form.AddField("cognome", surname);
        form.AddField("mail", email);
        form.AddField("password", password);
        form.AddField("nomeUtente", nomeUtente);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ArteSenzaConfini/Register_User.php", form))
        {
            yield return www.SendWebRequest();

            string controllo = www.downloadHandler.text;

            switch (controllo)
            {
                case "1: Connection failed":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Connessione al server fallita");
                    break;

                case "2: Email check failed":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Controllo dell'email fallito");
                    break;

                case "3: Email already exists":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Utente già esistente");
                    break;

                case "4: Insert user query failed":
                    Debug.Log("Failed to Register User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Invio dei dati fallito");
                    break;

                case "success":
                    Debug.Log("Successfully Registered!");
                    BarriereManager.instance.controlloBarriere();
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Registrazione effettuata", null);
                    PopUpManager.instance.SetStringText("Registrazione effettuata con successo, benvenuto/a " + nomeUtente);
                    break;
            }
        }
    }

    public IEnumerator LoginUser(string email, string password, UIDocument document)
    {
        WWWForm form = new WWWForm();
        form.AddField("mail", email);
        form.AddField("passwordLogin", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ArteSenzaConfini/Login_User.php", form))
        {
            yield return www.SendWebRequest();

            string controllo = www.downloadHandler.text;

            Debug.Log(controllo);

            switch (controllo)
            {
                case "1: Connection failed":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Connessione al server fallita");
                    break;

                case "2: Email check failed":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Controllo dell'email fallito");
                    break;

                case "5: Either no user with email, or more that one":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Email errata o inesistente");
                    break;

                case "6: Incorrect password":
                    Debug.Log("Failed to Login User!");
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.SetControllo(true);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Attenzione!", null);
                    PopUpManager.instance.SetStringText("Password errata");
                    break;

                case "success":
                    Debug.Log("Successfully Login!");
                    BarriereManager.instance.controlloBarriere();
                    document.rootVisualElement.style.display = DisplayStyle.None;
                    PopUpManager.instance.SetDocument(document);
                    PopUpManager.instance.ShowDocument();
                    PopUpManager.instance.SetStringHeader("Login effetuato", null);
                    PopUpManager.instance.SetStringText("Benvenuto/a ");
                    break;
            }
        }
    }

    public static IEnumerator sendToDescription(string descrizione, string nomeUtente)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("descrizione", descrizione);
        form1.AddField("nomeUtente", nomeUtente);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ArteSenzaConfini/Register_Description.php", form1))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);  
                Debug.Log("Failed to Register description!");
            }
            else
            {
                Debug.Log("Successfully Registered!");
                //SceneManager.LoadScene("");
            }       
        }
    }
}