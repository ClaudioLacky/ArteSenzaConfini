using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoginEvents : MonoBehaviour
{
    private UIDocument documentLogin;

    private UIDocument documentRegistration;

    private Button buttonAccessoLogin;
    private Button buttonRegistrationLogin;
    private Button buttonReturnLogin;

    //private List<Button> menuButtons = new List<Button>();

    private TextField textFieldEmail;
    private TextField textFieldPassword;

    //private AudioSource audioSource;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        documentLogin = GetComponent<UIDocument>();

        documentRegistration = GameObject.FindGameObjectWithTag("Registration").GetComponent<UIDocument>();

        documentLogin.rootVisualElement.style.display = DisplayStyle.None;

        buttonAccessoLogin = documentLogin.rootVisualElement.Q("ButtonAccessoLogin") as Button;
        buttonAccessoLogin.RegisterCallback<ClickEvent>(OnButtonAccesso);

        buttonRegistrationLogin = documentLogin.rootVisualElement.Q("ButtonRegistrationLogin") as Button;
        buttonRegistrationLogin.RegisterCallback<ClickEvent>(OnButtonRegistration);

        buttonReturnLogin = documentLogin.rootVisualElement.Q("ButtonReturnLogin") as Button;
        buttonReturnLogin.RegisterCallback<ClickEvent>(OnButtonReturn);

        //menuButtons = document.rootVisualElement.Query<Button>().ToList();

        textFieldEmail = documentLogin.rootVisualElement.Q("EmailLogin") as TextField;

        textFieldPassword = documentLogin.rootVisualElement.Q("PasswordLogin") as TextField;

        /*for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }

    private void OnButtonAccesso(ClickEvent evt)
    {
        Debug.Log("Hai premuto Accesso Button");
        StartCoroutine(MySqlManager.instance.LoginUser(textFieldEmail.text, textFieldPassword.text, documentLogin));
    }

    private void OnButtonRegistration(ClickEvent evt)
    {
        Debug.Log("Hai premuto Registration Button");
        documentLogin.rootVisualElement.style.display = DisplayStyle.None;
        documentRegistration.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnButtonReturn(ClickEvent evt)
    {
        Debug.Log("Hai premuto Return Button");
        documentLogin.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        buttonAccessoLogin.UnregisterCallback<ClickEvent>(OnButtonAccesso);
        buttonRegistrationLogin.UnregisterCallback<ClickEvent>(OnButtonRegistration);
        buttonReturnLogin.UnregisterCallback<ClickEvent>(OnButtonReturn);

        /*for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }*/
    }

    /*private void OnAllButtonsClick(ClickEvent evt)
    {
        //audioSource.Play();
    }*/
}
