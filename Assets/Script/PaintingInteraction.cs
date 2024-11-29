using UnityEngine;

public class PaintingInteraction : MonoBehaviour
{
    public float maxInteractionDistance = 5f; // Distanza massima per interagire con il quadro
    private Transform player; // Riferimento al giocatore
    private PaintingManager paintingManager; // Riferimento al manager dell'interfaccia
    private bool isUIActive = false; // Stato dell'interfaccia

    void Start()
    {
        // Trova il giocatore (assumendo che abbia il tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Trova il PaintingManager
        paintingManager = FindObjectOfType<PaintingManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isUIActive) // Tasto sinistro del mouse
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= maxInteractionDistance)
            {
                // Apri l'interfaccia con i dati del quadro
                PaintingInfo paintingInfo = GetComponent<PaintingInfo>();
                if (paintingInfo != null)
                {
                    paintingManager.ShowPaintingInfo(
                                            paintingInfo.paintingImage,
                                            paintingInfo.paintingTitle,
                                            paintingInfo.customWidth,
                                            paintingInfo.customHeight
                                        );
                                        isUIActive = true; // Blocca ulteriori interazioni
                }
            }
        }

        if (isUIActive && Input.GetKeyDown(KeyCode.Escape)) // ESC per chiudere
        {
            paintingManager.CloseUIPanel();
            isUIActive = false;
        }
    }
}