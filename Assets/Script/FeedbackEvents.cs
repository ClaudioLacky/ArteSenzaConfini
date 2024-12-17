using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FeedbackEvents: MonoBehaviour
{
    public static FeedbackEvents instance;

    private UIDocument document;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private TextField NomeUtente;
    private TextField textFieldFeedback;

    private Button buttonDescription;

    private bool isFeedback = true;

    private void Awake()
	{
        instance = this;

        //audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        NomeUtente = document.rootVisualElement.Q("NomeUtente") as TextField;

        textFieldFeedback = document.rootVisualElement.Q("TextFeedback") as TextField;

        buttonDescription = document.rootVisualElement.Q("ButtonFeedback") as Button;
        buttonDescription.RegisterCallback<ClickEvent>(OnButtonFeedback);

        /*for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }


    private void OnButtonFeedback(ClickEvent evt)
    {

        Debug.Log("ciao");
        bool controllo = false;

        if (string.IsNullOrWhiteSpace(NomeUtente.text) || NomeUtente.text.Length < 5)
        {
            Debug.LogError("Inserire un nome Utente");
            controllo = true;
        }

        if (string.IsNullOrWhiteSpace(textFieldFeedback.text) || textFieldFeedback.text.Length < 5)
        {
            Debug.LogError("Inserire una descrizione");
            controllo = true;
        }

        if (controllo)
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
            PopUpManager.instance.SetDocument(document);
            PopUpManager.instance.SetControllo(true);
            PopUpManager.instance.ShowDocument();
            PopUpManager.instance.SetStringHeader("Attenzione!");
            PopUpManager.instance.SetStringText("Inserire un nome utente valido di almeno 5 caratteri!\n\n" +
                "Inserire una descrizione valida di almeno 5 caratteri!");
            return;
        }

        if (!controllo)
        {
            Debug.Log("Hai premuto Crea Account Button");
            print("Successfully Registered!");
            StartCoroutine(MySqlManager.sendToFeedback(NomeUtente.text, textFieldFeedback.text, document));
        }
        else
        {
            print("Failed to Register description!");
        }
    }

    public void SetFeedback(bool isFeedback)
    {
        this.isFeedback = isFeedback;
    }

    public bool GetFeedback()
    {
        return this.isFeedback;
    }

    private void OnDisable()
    {
        buttonDescription.UnregisterCallback<ClickEvent>(OnButtonFeedback);

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
