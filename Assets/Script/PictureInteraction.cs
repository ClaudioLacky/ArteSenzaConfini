using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PictureInteraction : MonoBehaviour
{
    public GameObject panel; // Pannello che si apre
    public Image displayImage; // Immagine ingrandita nel pannello
    public TextMeshProUGUI displayText; // Testo nel pannello
    public MonoBehaviour cameraController; // Controllore della camera

    public string id;

    private bool isPanelActive = false; // Stato del pannello
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        panel.SetActive(false); // Nascondi il pannello all'inizio
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPanelActive) // Click sinistro
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f)) // Cambia 10f con la distanza massima
            {
                if (hit.collider.CompareTag(id)) // Assicurati che il quadro abbia questo tag
                {
                    // Ottieni i dati del quadro
                    PictureData data = hit.collider.GetComponent<PictureData>();
                    if (data != null)
                    {
                        OpenPanel(data);
                    }
                }
            }
        }

        if (isPanelActive && Input.GetKeyDown(KeyCode.Escape)) // Chiudi pannello con ESC
        {
            ClosePanel();
        }
    }

    void OpenPanel(PictureData data)
    {
        displayImage.sprite = data.image; // Imposta immagine
        displayImage.rectTransform.sizeDelta = data.size; // Imposta dimensione immagine
        displayText.text = data.text; // Imposta testo

        panel.SetActive(true);
        isPanelActive = true;

        // Disabilita il movimento della camera
                if (cameraController != null)
                {
                    cameraController.enabled = false; // Disabilita il controllo della camera
                }

        // Disabilita movimento (esempio per CharacterController)
        var controller = FindObjectOfType<CharacterController>();
        if (controller != null)
            controller.enabled = false;
    }

    void ClosePanel()
    {
        panel.SetActive(false);
        isPanelActive = false;

        // Riabilita il movimento della camera
                if (cameraController != null)
                {
                    cameraController.enabled = true; // Riabilita il controllo della camera
                }

        // Riabilita movimento
        var controller = FindObjectOfType<CharacterController>();
        if (controller != null)
            controller.enabled = true;
    }
}
