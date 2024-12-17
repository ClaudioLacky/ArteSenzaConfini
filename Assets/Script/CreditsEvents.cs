using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsEvents : MonoBehaviour
{
    private UIDocument document;

    private UIDocument documentMainMenu;

    private Button buttonCredits;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        documentMainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<UIDocument>();

        buttonCredits = document.rootVisualElement.Q("ButtonCredits") as Button;
        buttonCredits.RegisterCallback<ClickEvent>(OnButtonCredits);
    }

    private void OnButtonCredits(ClickEvent evt)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
        documentMainMenu.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnDisable()
    {
        buttonCredits.UnregisterCallback<ClickEvent>(OnButtonCredits);
    }
}
