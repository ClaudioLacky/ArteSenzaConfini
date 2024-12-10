using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{

    private UIDocument document;

    private Button buttonEnter;
    private Button buttonExit;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        buttonEnter = document.rootVisualElement.Q("ButtonEnter") as Button;
        buttonEnter.RegisterCallback<ClickEvent>(OnButtonEnter);

        buttonExit = document.rootVisualElement.Q("ButtonExit") as Button;
        buttonExit.RegisterCallback<ClickEvent>(OnButtonExit);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnButtonEnter(ClickEvent evt)
    {
        Debug.Log("Hai premuto Start Button");
        SceneManager.LoadScene("Login");
    }

    private void OnButtonExit(ClickEvent evt)
    {
        Debug.Log("Hai premuto Exit Button");
        Application.Quit();
    }

    private void OnDisable()
    {
        buttonEnter.UnregisterCallback<ClickEvent>(OnButtonEnter);
        buttonExit.UnregisterCallback<ClickEvent>(OnButtonExit);

        for (int i = 0;i < menuButtons.Count;i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }
}
