using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel;      // Il pannello che contiene il testo e l'immagine
    public Text uiText;             // Il testo da visualizzare
    public Image uiImage;           // L'immagine da visualizzare
    public float maxDistance = 10f; // Distanza massima per il clic

    private bool isUIVisible = false;  // Flag per verificare se l'interfaccia è visibile

    void Start()
    {
        // Nascondi l'interfaccia all'inizio
        uiPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;  // Assicurati che il cursore non sia bloccato
        Cursor.visible = true;                  // Assicurati che il cursore sia visibile
    }

    void Update()
    {
        // Controlla il clic del mouse (tasto sinistro)
        if (Input.GetMouseButtonDown(0)) // 0 per il tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Controlla se il raycast colpisce un oggetto
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Controlla se l'oggetto cliccato ha il tag giusto
                if (hit.transform.CompareTag("Clickable"))
                {
                    // Aggiorna il testo e l'immagine specifici per l'oggetto cliccato
                    UpdateUI(hit.transform);
                    ShowUI();  // Mostra l'interfaccia
                }
            }
        }

        // Controlla se il tasto per nascondere l'interfaccia è premuto (per esempio, il tasto "Esc")
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUI();  // Nascondi l'interfaccia
        }
    }

    // Funzione per aggiornare il testo e l'immagine in base all'oggetto cliccato
    void UpdateUI(Transform clickedObject)
    {
        // Qui imposti dinamicamente il testo e l'immagine in base all'oggetto cliccato
        if (clickedObject.CompareTag("Clickable"))
        {
            // Ad esempio, puoi usare un componente che contiene il nome dell'oggetto
            // o aggiungere un componente personalizzato per immagine e testo.
            uiText.text = clickedObject.name + " cliccato!";

            // Supponiamo di avere un componente personalizzato "ObjectInfo" che contiene l'immagine
            ObjectInfo objectInfo = clickedObject.GetComponent<ObjectInfo>();
            if (objectInfo != null)
            {
                uiImage.sprite = objectInfo.objectImage;  // Assegna l'immagine specifica per quell'oggetto
            }
        }
    }

    void ShowUI()
    {
        if (!isUIVisible)
        {
            // Mostra l'interfaccia
            uiPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;  // Sblocca il cursore
            Cursor.visible = true;                   // Mostra il cursore

            isUIVisible = true;  // Imposta lo stato dell'interfaccia come visibile
        }
    }

    void HideUI()
    {
        if (isUIVisible)
        {
            // Nascondi l'interfaccia
            uiPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;  // Blocca il cursore
            Cursor.visible = false;                    // Nascondi il cursore

            isUIVisible = false;  // Imposta lo stato dell'interfaccia come nascosto
        }
    }
}