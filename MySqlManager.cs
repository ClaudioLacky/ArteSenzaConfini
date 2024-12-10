using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MySqlManager : MonoBehaviour
{
    /*readonly static string SERVER_URL = "http://localhost/sqlconnect";



    public static async Task<bool> RegisterUser(string name, string surname, string email, string password)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/register.php";

        return await SendPostRequest(REGISTER_USER_URL, new Dictionary<string, string>()
        {
            {"nome", name},
            {"cognome", surname},
            {"mail", email},
            {"password", password}
        });
    }

    static async Task<bool> SendPostRequest(string url, Dictionary<string, string> data)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url, data))
        {
            req.SendWebRequest();

            while (!req.isDone) await Task.Delay(100);

            // When the Task is done
            if (req.error != null 
                || !string.IsNullOrWhiteSpace(req.error)
                || HasErrorMessage(req.downloadHandler.text))
            {
                return false;
            }

            // On Success
            return true;
        }
    }

    static bool HasErrorMessage(string msg) => int.TryParse(msg, out var res);*/

    /*private void Start()
    {
        StartCoroutine(RegisterUser("Matteo", "Di Maria", "matteodimaria03@gmail.com", "Matu565658789*"));
    }*/

    public IEnumerator RegisterUser(string name, string surname, string email, string password, string nomeUtente)
    {
        WWWForm form = new WWWForm();
        form.AddField("nome", name);
        form.AddField("cognome", surname);
        form.AddField("mail", email);
        form.AddField("password", password);
        form.AddField("nomeUtente", nomeUtente);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Register_User.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                Debug.Log("Failed to Register User!");
            }
            else
            {
                Debug.Log("Successfully Registered!");
                //SceneManager.LoadScene("");
            }
        }
    }

    public static IEnumerator sendToDescription(string descrizione, string nomeUtente)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("descrizione", descrizione);
        form1.AddField("nomeUtente", nomeUtente);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/register_description.php", form1))
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

/*public class DatabaseUser
{
    public string name;
    public string surname;
    public string email;
    public string password;
}*/