using UnityEngine;
using UnityEngine.UIElements;

public class MouseHover : MonoBehaviour
{
    private Material material;

    private UIDocument documentLogin;

    private void Awake()
    {
        material = new Material(Shader.Find("Standard"));

        documentLogin = GameObject.Find("Login").GetComponent<UIDocument>();
    }

    private void OnMouseEnter()
    {
        material.color = Color.yellow;

        //assign the material to the renderer
        GetComponent<Renderer>().material = material;
    }

    private void OnMouseExit()
    {
        material.color = Color.black;

        //assign the material to the renderer
        GetComponent<Renderer>().material = material;
    }

    private void OnMouseUpAsButton()
    {
        documentLogin.enabled = true;
    }
}
