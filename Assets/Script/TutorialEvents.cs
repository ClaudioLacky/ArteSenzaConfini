using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialEvents : MonoBehaviour
{
    private UIDocument document;

    private Button buttonTutorial;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        buttonTutorial = document.rootVisualElement.Q("ButtonTutorial") as Button;
        buttonTutorial.RegisterCallback<ClickEvent>(OnButtonTutorial);
    }

    private void OnButtonTutorial(ClickEvent evt)
    {
        MouseLook.instance.Start();
        MouseLook.instance.Update();
        document.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        buttonTutorial.UnregisterCallback<ClickEvent>(OnButtonTutorial);
    }
}
