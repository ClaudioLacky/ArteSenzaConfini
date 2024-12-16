using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseHoverDoor : MonoBehaviour
{
    public static MouseHoverDoor instance;

    private Material material;

    private UIDocument document;

    private UIDocument documentAlert;

    private bool userLog = false;

    private void Awake()
    {
        material = new Material(Shader.Find("Standard"));

        document = GameObject.FindGameObjectWithTag("Feedback").GetComponent<UIDocument>();

        documentAlert = GameObject.FindGameObjectWithTag("Alert").GetComponent<UIDocument>();

        instance = this;
    }

    private void OnMouseEnter()
    {
        if (userLog)
        {
            material.color = Color.yellow;

            //assign the material to the renderer
            GetComponent<Renderer>().material = material;
        }
    }

    private void OnMouseExit()
    {
        if (userLog)
        {
            material.color = Color.black;

            //assign the material to the renderer
            GetComponent<Renderer>().material = material;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (userLog)
        {
            AlertEvents.instance.SetStringHeader("Sei sicuro di voler terminare l'esperienza?");
            AlertEvents.instance.SetDocument(document);
            AlertEvents.instance.SetScena("MuseoPorta");

            // Riabilita i cursori standard di Unity
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            // Pausa il gioco
            Time.timeScale = 0f;

            documentAlert.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    public void SetUserLog(bool userLog)
    {
        Debug.Log("SetUserLog");
        this.userLog = userLog;
    }
}
