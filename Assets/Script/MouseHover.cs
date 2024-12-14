using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseHover : MonoBehaviour
{
    public static MouseHover instance;

    private Material material;

    private bool userLog = true;

    private void Awake()
    {
        material = new Material(Shader.Find("Standard"));

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
            // Riabilita i cursori standard di Unity
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pausa il gioco
            Time.timeScale = 0f;

            SceneManager.LoadScene("Login", LoadSceneMode.Additive);
        }
    }

    public void SetUserLog(bool userLog)
    {
        this.userLog = userLog;
    }
}