using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PictureInteraction : MonoBehaviour
{
    public GameObject panel; // Pannello che si apre
    public Image displayImage; // Immagine ingrandita nel pannello
    public TextMeshProUGUI displayText; // Testo nel pannello
    public float maxDistance = 5f;

    public string id = "Clickable";

    private bool isPanelActive = false; // Stato del pannello
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        panel.SetActive(isPanelActive); // Nascondi il pannello all'inizio
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPanelActive) // Click sinistro
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance)) // Cambia 10f con la distanza massima
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

        isPanelActive = true;
        panel.SetActive(isPanelActive);

        Time.timeScale = 0f;  // Blocca il gioco

    }

    void ClosePanel()
    {

        isPanelActive = false;
        panel.SetActive(isPanelActive);

        Time.timeScale = 1f;  // Sblocca il gioco
    }
}
