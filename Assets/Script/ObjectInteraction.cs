using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Camera mainCamera; // Riferimento alla Main Camera
    public GameObject player; // Riferimento al player (o script del movimento)
    public GameObject panel;  // Riferimento al pannello della UI
    public float maxDistance = 10f; // Massima distanza per l'interazione

    private bool isPanelActive = false; // Stato del pannello
    private MonoBehaviour playerMovementScript; // Script del movimento del player
    private MonoBehaviour cameraControlScript;  // Script del controllo della camera

    void Start()
    {
        // Trova automaticamente i componenti di movimento se non sono assegnati
        if (player != null)
            playerMovementScript = player.GetComponent<MonoBehaviour>();
        if (mainCamera != null)
            cameraControlScript = mainCamera.GetComponent<MonoBehaviour>();

        // Assicurati che il pannello sia disattivato all'inizio
        panel.SetActive(false);
    }

    void Update()
    {
        // Se il pannello è attivo, ESC per chiudere
        if (isPanelActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
            return;
        }

        // Click sinistro per interagire
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                // Controlla se hai cliccato l'oggetto specifico
                if (hit.collider.CompareTag("Clickable")) //
                {
                    OpenPanel();
                }
            }
        }
    }

    void OpenPanel()
    {
        // Mostra il pannello
        panel.SetActive(true);
        isPanelActive = true;

        // Disabilita il movimento del player e della camera
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;
        if (cameraControlScript != null)
            cameraControlScript.enabled = false;
    }

    void ClosePanel()
    {
        // Nascondi il pannello
        panel.SetActive(false);
        isPanelActive = false;

        // Riabilita il movimento del player e della camera
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
        if (cameraControlScript != null)
            cameraControlScript.enabled = true;
    }
}
