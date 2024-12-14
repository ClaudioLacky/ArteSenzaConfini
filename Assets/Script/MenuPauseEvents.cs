using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuPauseEvents : MonoBehaviour
{
    private UIDocument document;

    private Button buttonReturn;

    private Button buttonSettings;

    private Button buttonExit;

    private GameObject gameObject;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        gameObject = GetComponent<GameObject>();

        buttonReturn = document.rootVisualElement.Q("ButtonReturn") as Button;
        buttonReturn.RegisterCallback<ClickEvent>(OnButtonReturn);

        buttonSettings = document.rootVisualElement.Q("ButtonSettings") as Button;
        buttonSettings.RegisterCallback<ClickEvent>(OnButtonSettings);

        buttonExit = document.rootVisualElement.Q("ButtonExit") as Button;
        buttonExit.RegisterCallback<ClickEvent>(OnButtonExit);
    }

    private void OnButtonReturn(ClickEvent evt)
    {
        Time.timeScale = 1f;  // Sblocca il gioco
        gameObject.SetActive(false);
    }

    private void OnButtonSettings(ClickEvent evt)
    {
        Debug.Log("Settings");
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    private void OnButtonExit(ClickEvent evt)
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnDisable()
    {
        buttonReturn.UnregisterCallback<ClickEvent>(OnButtonReturn);
        buttonSettings.UnregisterCallback<ClickEvent>(OnButtonSettings);
        buttonExit.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}
