using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private UIDocument documentAlert;

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

        documentAlert.rootVisualElement.style.display = DisplayStyle.N;

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
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    private void OnButtonExit(ClickEvent evt)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
        AlertEvents.instance.SetDocument(document);
        AlertEvents.instance.SetScena("Museo");
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
