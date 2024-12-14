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

    private void Awake()
    {
        instance = this;

        documentInternal = GameObject.Find("PopUp").GetComponent<UIDocument>();

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

    public void setStringHeader(string msg, string scena)
    {
        header.text = msg;

        this.scena = scena;
    }

    public void setStringText(string msg)
    {
        textPopUp.text = msg;
    }

    public void ShowDocument()
    {
        documentInternal.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void OnButtonExit(ClickEvent evt)
    {
        Debug.Log("Ciao");
        documentInternal.rootVisualElement.style.display = DisplayStyle.None;

        if (scena != null)
        {
            MouseLook.instance.Start();
            MouseLook.instance.Update();
            //MouseHover.instance.SetUserLog(false);
            SceneManager.UnloadSceneAsync(scena);
        }
        else
        {
            documentExternal.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    private void OnDisable()
    {
        buttonPopUp.UnregisterCallback<ClickEvent>(OnButtonExit);
    }
}