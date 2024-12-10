using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DescriptionEvents: MonoBehaviour
{
    private UIDocument document;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private Button buttonDescription;

    private TextField textFieldDescription;
    private TextField NomeUtente;

    private void Awake()
	{
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        buttonDescription = document.rootVisualElement.Q("ButtonDescription") as Button;
        buttonDescription.RegisterCallback<ClickEvent>(OnButtonDescription);

        textFieldDescription = document.rootVisualElement.Q("NameDescription") as TextField;
        NomeUtente = document.rootVisualElement.Q("NomeUtente") as TextField;

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }


    private void OnButtonDescription(ClickEvent evt)
    {

        Debug.Log("ciao");
        bool controllo = false;

        if (string.IsNullOrWhiteSpace(NomeUtente.text) || NomeUtente.text.Length < 5)
        {
            Debug.LogError("Inserire un nome Utente");
            controllo = true;
        }

        if (string.IsNullOrWhiteSpace(textFieldDescription.text) || textFieldDescription.text.Length < 5)
        {
            Debug.LogError("Inserire una descrizione");
            controllo = true;
        }


        if (!controllo)
        {
            Debug.Log("Hai premuto Crea Account Button");
            print("Successfully Registered!");
            StartCoroutine(MySqlManager.sendToDescription(textFieldDescription.text, NomeUtente.text));
            //SceneManager.LoadScene("");
        }
        else
        {
            print("Failed to Register description!");
        }
    }

    private void OnDisable()
    {
        buttonDescription.UnregisterCallback<ClickEvent>(OnButtonDescription);

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
