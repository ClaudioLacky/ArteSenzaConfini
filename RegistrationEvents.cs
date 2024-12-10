using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RegistrationEvents : MonoBehaviour
{
    private UIDocument document;

    private Button buttonRegistration;
    private Button buttonReturnRegistration;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private TextField textFieldName;
    private TextField textFieldSurname;
    private TextField textFieldEmail;
    private TextField textFieldPassword;
    private TextField textFieldNomeUtente;

    private MySqlManager MySqlManager;

    //private PopUpManager PopUpManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        buttonRegistration = document.rootVisualElement.Q("ButtonRegistration") as Button;
        buttonRegistration.RegisterCallback<ClickEvent>(OnButtonRegistration);

        buttonReturnRegistration = document.rootVisualElement.Q("ButtonReturnRegistration") as Button;
        buttonReturnRegistration.RegisterCallback<ClickEvent>(OnButtonReturn);

        textFieldName = document.rootVisualElement.Q("NameRegistration") as TextField;

        textFieldSurname = document.rootVisualElement.Q("SurnameRegistration") as TextField;

        textFieldEmail = document.rootVisualElement.Q("EmailRegistration") as TextField;

        textFieldPassword = document.rootVisualElement.Q("PasswordRegistration") as TextField;

        textFieldNomeUtente = document.rootVisualElement.Q("NomeutenteRegistration") as TextField;

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        MySqlManager = GetComponent<MySqlManager>();

        //PopUpManager = GameObject.Find("PopUp").GetComponent<PopUpManager>();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnButtonRegistration(ClickEvent evt)
    {
        bool controllo = false;

        //while (!controllo)
        //{
            if (string.IsNullOrWhiteSpace(textFieldName.text) || textFieldName.text.Length < 2)
            {
                Debug.LogError("Inserire un nome valido di almeno 2 caratteri");
                controllo = true;
            }

            if (string.IsNullOrWhiteSpace(textFieldSurname.text) || textFieldSurname.text.Length < 2)
            {
                Debug.LogError("Inserire un cognome valido di almeno 2 caratteri");
                controllo = true;
            }

            if (string.IsNullOrWhiteSpace(textFieldEmail.text) || textFieldEmail.text.Length < 5)
            {
                Debug.LogError("Inserire un email valida");
                controllo = true;
            }

            if (string.IsNullOrWhiteSpace(textFieldPassword.text) || isValid(textFieldPassword.text))
            {
                Debug.LogError("Inserire una password valida che deve contenere:\n" +
                    " lunghezza minina di 8 caratteri, una lettera maiuscola, una lettera minuscola e un carattere speciale.");
                controllo = true;
            }

            if (string.IsNullOrWhiteSpace(textFieldNomeUtente.text) || textFieldNomeUtente.text.Length < 5)
            {
                Debug.LogError("Inserire un Nome Utente");
                controllo = true;
            }

        /*if (controllo)
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
            PopUpManager.ShowDocument();
            PopUpManager.setStringHeader("Attenzione!");
            PopUpManager.setStringText("Inserire un nome valido di almeno 2 caratteri\n\n" +
                "Inserire un cognome valido di almeno 2 caratteri\n\n" +
                "Inserire un email valida\n\n" +
                "Inserire una password valida che deve contenere:\n" +
                "lunghezza minina di 8 caratteri, una lettera maiuscola, una lettera minuscola e un carattere speciale.");
            return;
        }*/

        if (!controllo)
        {
            Debug.Log("Hai premuto Crea Account Button");
            print("Successfully Registered!");
            StartCoroutine(MySqlManager.RegisterUser(textFieldName.text, textFieldSurname.text, textFieldEmail.text, textFieldPassword.text,textFieldNomeUtente.text));
            //SceneManager.LoadScene("");
        }
        else
        {
            print("Failed to Register User!");
        }
    }

    private Boolean isValid(string pass)
    {
        int minLength = 8;
        bool numero = false;
        bool maiuscola = false;
        bool minuscola = false;
        bool simbolo = false;

        if (pass.Length >= minLength)
        {
            for (int i = 0; i < pass.Length; i++)
            {
                if (Char.IsNumber(pass[i]))
                {
                    numero = true;
                }
                else
                {
                    if (Char.IsLetter(pass[i]))
                    {
                        if (Char.IsUpper(pass[i]))
                        {
                            maiuscola |= true;
                        }
                        else if (Char.IsLower(pass[i]))
                        {
                            minuscola |= true;
                        }
                        return false;
                    }
                    else
                    {
                        if (Char.IsSymbol(pass[i]))
                        {
                            simbolo = true;
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

    private void OnButtonReturn(ClickEvent evt)
    {
        Debug.Log("Hai premuto Return Button");
        SceneManager.LoadScene("Login");
    }

    private void OnDisable()
    {
        buttonRegistration.UnregisterCallback<ClickEvent>(OnButtonRegistration);
        buttonReturnRegistration.UnregisterCallback<ClickEvent>(OnButtonReturn);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }
}
