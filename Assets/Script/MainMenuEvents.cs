using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private UIDocument documentAlert;

    private UIDocument documentSettings;

    private Button buttonEnter;
    private Button buttonSettings;
    private Button buttonExit;

    //private List<Button> menuButtons = new List<Button>();

    //private AudioSource audioSource;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        documentAlert.rootVisualElement.style.display = DisplayStyle.None;

        documentSettings = GameObject.FindGameObjectWithTag("Settings").GetComponent<UIDocument>();

        documentSettings.rootVisualElement.style.display = DisplayStyle.None;

        buttonEnter = document.rootVisualElement.Q("ButtonEnter") as Button;
        buttonEnter.RegisterCallback<ClickEvent>(OnButtonEnter);

        buttonSettings = document.rootVisualElement.Q("ButtonSettings") as Button;
        buttonSettings.RegisterCallback<ClickEvent>(OnButtonSettings);

        buttonExit = document.rootVisualElement.Q("ButtonExit") as Button;
        buttonExit.RegisterCallback<ClickEvent>(OnButtonExit);

        //menuButtons = document.rootVisualElement.Query<Button>().ToList();

        /*for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }

    private void OnButtonEnter(ClickEvent evt)
    {
        Debug.Log("Hai premuto Start Button");
        SceneManager.LoadScene("Museo");
    }
    private void OnButtonSettings(ClickEvent evt)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
        SettingsEvents.instance.SetDocument(document);
        documentSettings.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnButtonExit(ClickEvent evt)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
        AlertEvents.instance.SetDocument(document);
        AlertEvents.instance.SetScena("MainMenu");
        AlertEvents.instance.SetStringHeader("Sei sicuro/a di voler uscire?");
        documentAlert.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnDisable()
    {
        buttonEnter.UnregisterCallback<ClickEvent>(OnButtonEnter);
        buttonSettings.UnregisterCallback<ClickEvent>(OnButtonSettings);
        buttonExit.UnregisterCallback<ClickEvent>(OnButtonExit);

        /*for (int i = 0;i < menuButtons.Count;i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }

    /*private void OnAllButtonsClick(ClickEvent evt)
    {
        //audioSource.Play();
    }*/
}
