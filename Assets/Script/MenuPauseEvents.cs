using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuPauseEvents : MonoBehaviour
{
    private UIDocument documentPause;

    private UIDocument documentSettings;

    private UIDocument documentAlert;

    private Button buttonReturn;

    private Button buttonSettings;

    private Button buttonExit;

    private void Awake()
    {
        documentPause = GetComponent<UIDocument>();

        documentSettings = GameObject.FindGameObjectWithTag("Settings").GetComponent<UIDocument>();

        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        buttonReturn = documentPause.rootVisualElement.Q("ButtonReturn") as Button;
        buttonReturn.RegisterCallback<ClickEvent>(OnButtonReturn);

        buttonSettings = documentPause.rootVisualElement.Q("ButtonSettings") as Button;
        buttonSettings.RegisterCallback<ClickEvent>(OnButtonSettings);

        buttonExit = documentPause.rootVisualElement.Q("ButtonExit") as Button;
        buttonExit.RegisterCallback<ClickEvent>(OnButtonExit);
    }

    private void OnButtonReturn(ClickEvent evt)
    {
        Time.timeScale = 1f;  // Sblocca il gioco
        MouseLook.instance.Start();
        MouseLook.instance.Update();
        //gameObject.SetActive(false);
        documentPause.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnButtonSettings(ClickEvent evt)
    {
        Debug.Log("Settings");
        documentPause.rootVisualElement.style.display = DisplayStyle.None;
        documentSettings.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnButtonExit(ClickEvent evt)
    {
        documentPause.rootVisualElement.style.display = DisplayStyle.None;
        AlertEvents.instance.SetDocument(documentPause);
        AlertEvents.instance.SetScena("Museo");
        AlertEvents.instance.SetStringHeader("Sei sicuro/a di voler tornare al menù principale?");
        documentAlert.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnDisable()
    {
        buttonReturn.UnregisterCallback<ClickEvent>(OnButtonReturn);
        buttonSettings.UnregisterCallback<ClickEvent>(OnButtonSettings);
        buttonExit.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}
