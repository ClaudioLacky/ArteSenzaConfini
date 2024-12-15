using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;

    private UIDocument documentInternal;

    private UIDocument documentExternal;

    private Label header;

    private Label textPopUp;

    private Button buttonPopUp;

    private string scena;

    private bool controllo = false;

    private void Awake()
    {
        instance = this;

        documentInternal = GetComponent<UIDocument>();

        documentInternal.rootVisualElement.style.display = DisplayStyle.None;

        header = documentInternal.rootVisualElement.Q("HeaderPopUp") as Label;

        textPopUp = documentInternal.rootVisualElement.Q("TextPopUp") as Label;

        buttonPopUp = documentInternal.rootVisualElement.Q("ButtonPopUp") as Button;
        buttonPopUp.RegisterCallback<ClickEvent>(OnButtonExit);
    }


    public void SetDocument(UIDocument document)
    {
        documentExternal = document;
    }

    public void SetStringHeader(string msg, string scena)
    {
        header.text = msg;

        this.scena = scena;
    }

    public void SetStringText(string msg)
    {
        textPopUp.text = msg;
    }

    public void SetControllo(bool controllo)
    {
        this.controllo = controllo;
    }

    public void ShowDocument()
    {
        documentInternal.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnButtonExit(ClickEvent evt)
    {
        Debug.Log("Ciao");
        documentInternal.rootVisualElement.style.display = DisplayStyle.None;

        if (controllo)
        {
            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            MouseLook.instance.Start();
            MouseLook.instance.Update();
            MouseHover.instance.SetUserLog(false);
            Time.timeScale = 1f;  // Sblocca il gioco
        }
    }

    private void OnDisable()
    {
        buttonPopUp.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}