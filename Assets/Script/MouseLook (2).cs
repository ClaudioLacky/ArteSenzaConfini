using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilità del mouse

    public Transform playerBody; // Riferimento al corpo del giocatore
    private float xRotation = 0f; // Per la rotazione verticale (su/giù)

    void Start()
    {
        // Bloccare il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Leggere l'input del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ruotare il giocatore attorno all'asse Y (sinistra/destra)
        playerBody.Rotate(Vector3.up * mouseX);

        // Ruotare la camera verticalmente (su/giù), ma limitare l'angolo
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotazione verticale

        // Applica la rotazione solo alla camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}