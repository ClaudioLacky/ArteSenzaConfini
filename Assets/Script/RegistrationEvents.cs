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
    public static RegistrationEvents instance;

    private UIDocument documentRegistration;

    private UIDocument documentLogin;

    private Button buttonRegistration;
    private Button buttonReturnRegistration;

    //private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private TextField textFieldName;
    private TextField textFieldSurname;
    private TextField textFieldEmail;
    private TextField textFieldPassword;
    private TextField textFieldNomeUtente;

    private bool isRegistred = true;

    private void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
        documentRegistration = GetComponent<UIDocument>();

        documentRegistration.rootVisualElement.style.display = DisplayStyle.None;

        documentLogin = GameObject.FindGameObjectWithTag("Login").GetComponent<UIDocument>();

        buttonRegistration = documentRegistration.rootVisualElement.Q("ButtonRegistration") as Button;
        buttonRegistration.RegisterCallback<ClickEvent>(OnButtonRegistration);

        buttonReturnRegistration = documentRegistration.rootVisualElement.Q("ButtonReturnRegistration") as Button;
        buttonReturnRegistration.RegisterCallback<ClickEvent>(OnButtonReturn);

        textFieldName = documentRegistration.rootVisualElement.Q("NameRegistration") as TextField;

        textFieldSurname = documentRegistration.rootVisualElement.Q("SurnameRegistration") as TextField;

        textFieldEmail = documentRegistration.rootVisualElement.Q("EmailRegistration") as TextField;

        textFieldPassword = documentRegistration.rootVisualElement.Q("PasswordRegistration") as TextField;

        textFieldNomeUtente = documentRegistration.rootVisualElement.Q("NomeutenteRegistration") as TextField;

        //menuButtons = document.rootVisualElement.Query<Button>().ToList();

        /*for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }

    private void OnButtonRegistration(ClickEvent evt)
    {
        bool controllo = false;

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

        if (controllo)
        {
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

        if (!controllo)
        {
            Debug.Log("Hai premuto Crea Account Button");
            print("Successfully Registered!");
            StartCoroutine(MySqlManager.instance.RegisterUser(textFieldName.text, textFieldSurname.text, textFieldEmail.text, textFieldPassword.text, textFieldNomeUtente.text, documentRegistration));
        }
        else
        {
            print("Failed to Register User!");
        }
    }

    private bool isValid(string pass)
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

    public void SetRegistered(bool isRegistered)
    {
        this.isRegistred = isRegistered;
    }

    public bool GetRegistered()
    {
        return isRegistred;
    }

    private void OnButtonReturn(ClickEvent evt)
    {
        Debug.Log("Hai premuto Return Button");
        documentRegistration.rootVisualElement.style.display = DisplayStyle.None;
        documentLogin.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnDisable()
    {
        buttonRegistration.UnregisterCallback<ClickEvent>(OnButtonRegistration);
        buttonReturnRegistration.UnregisterCallback<ClickEvent>(OnButtonReturn);

        /*for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }

    /*private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }*/
}
