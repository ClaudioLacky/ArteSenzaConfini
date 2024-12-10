using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoginEvents : MonoBehaviour
{
    private UIDocument document;

    private Button buttonAccessoLogin;
    private Button buttonRegistrationLogin;
    private Button buttonReturnLogin;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();

        buttonAccessoLogin = document.rootVisualElement.Q("ButtonAccessoLogin") as Button;
        buttonAccessoLogin.RegisterCallback<ClickEvent>(OnButtonAccesso);

        buttonRegistrationLogin = document.rootVisualElement.Q("ButtonRegistrationLogin") as Button;
        buttonRegistrationLogin.RegisterCallback<ClickEvent>(OnButtonRegistration);

        buttonReturnLogin = document.rootVisualElement.Q("ButtonReturnLogin") as Button;
        buttonReturnLogin.RegisterCallback<ClickEvent>(OnButtonReturn);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnButtonAccesso(ClickEvent evt)
    {
        Debug.Log("Hai premuto Accesso Button");
        SceneManager.LoadScene("Login");
    }

    private void OnButtonRegistration(ClickEvent evt)
    {
        Debug.Log("Hai premuto Registration Button");
        SceneManager.LoadScene("Registration");
    }

    private void OnButtonReturn(ClickEvent evt)
    {
        Debug.Log("Hai premuto Return Button");
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        buttonAccessoLogin.UnregisterCallback<ClickEvent>(OnButtonAccesso);
        buttonRegistrationLogin.UnregisterCallback<ClickEvent>(OnButtonRegistration);
        buttonReturnLogin.UnregisterCallback<ClickEvent>(OnButtonReturn);

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
