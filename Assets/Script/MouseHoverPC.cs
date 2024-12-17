using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MouseHoverPC : MonoBehaviour
{
    public static MouseHoverPC instance;

    private Material material;

    private UIDocument document;

    private bool userLog = true;

    private void Awake()
    {
        material = new Material(Shader.Find("Standard"));

        document = GameObject.FindGameObjectWithTag("Login").GetComponent<UIDocument>();

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
            LoginEvents.instance.SetLogged(false);
            RegistrationEvents.instance.SetRegistered(false);
            FeedbackEvents.instance.SetFeedback(false);

            // Riabilita i cursori standard di Unity
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            // Pausa il gioco
            Time.timeScale = 0f;

            //SceneManager.LoadScene("Login", LoadSceneMode.Additive);
            document.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    public void SetUserLog(bool userLog)
    {
        Debug.Log("SetUserLog");
        this.userLog = userLog;
    }
}