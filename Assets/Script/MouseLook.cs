using UnityEngine;

/// <summary>
/// Classe che gestisce il movimento del mouse come telecamera del player
/// </summary>
public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;           // Istanza della classe

    public float mouseSensitivity = 100f;       // Sensibilità del mouse

    public Transform playerBody;                // Riferimento al corpo del giocatore
    private float xRotation = 0f;               // Per la rotazione verticale (su/giù)

    // Metodo Start che si avvia all'esecuzione dello script
    public void Start()
    {
        instance = this;

        // Bloccare il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Metodo che si ripete ciclicamente per controllare e fare operazioni che avviene una volta per frame
    // e che determina cosa avviene in scena
    public void Update()
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