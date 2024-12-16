using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AlertEvents : MonoBehaviour
{
    public static AlertEvents instance;

    private UIDocument documentAlert;

    private UIDocument documentExternal;

    private Label header;

    private Button buttonYes;

    private Button buttonNo;

    private string scena;

    private void Awake()
    {
        instance = this;

        documentAlert = GetComponent<UIDocument>();

        header = documentAlert.rootVisualElement.Q("HeaderAlert") as Label;

        buttonYes = documentAlert.rootVisualElement.Q("ButtonYes") as Button;
        buttonYes.RegisterCallback<ClickEvent>(OnButtonYes);

        buttonNo = documentAlert.rootVisualElement.Q("ButtonNo") as Button;
        buttonNo.RegisterCallback<ClickEvent>(OnButtonNo);
    }

    public void SetStringHeader(string msg)
    {
        header.text = msg;
    }

    public void SetScena(string scena)
    {
        this.scena = scena;
    }

    public void SetDocument(UIDocument document)
    {
        documentExternal = document;
    }

    private void OnButtonYes(ClickEvent evt)
    {
        if (scena.Equals("Museo"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (scena.Equals("MainMenu"))
        {
            Application.Quit();
        }
        else if (scena.Equals("MuseoPorta"))
        {
            documentAlert.rootVisualElement.style.display = DisplayStyle.None;

            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    private void OnButtonNo(ClickEvent evt)
    {
        if (scena.Equals("MuseoPorta"))
        {
            MouseLook.instance.Start();
            MouseLook.instance.Update();
            Time.timeScale = 1f;  // Sblocca il gioco
            documentAlert.rootVisualElement.style.display = DisplayStyle.None;
        }
        else
        {
            documentAlert.rootVisualElement.style.display = DisplayStyle.None;
            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    private void OnDisable()
    {
        buttonYes.UnregisterCallback<ClickEvent>(OnButtonYes);
        buttonNo.UnregisterCallback<ClickEvent>(OnButtonNo);
    }
}
