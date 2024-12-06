using UnityEngine;

public class AudioInteraction : MonoBehaviour
{
    private AudioSource audioSource; // Riferimento all'AudioSource del GameObject corrente
    private bool isInteracting = false; // Stato di interazione con il GameObject

    void Start()
    {
        // Ottieni il riferimento all'AudioSource del GameObject
        audioSource = GetComponent<AudioSource>();

        // Se non ci sono AudioSource, segnala un errore
        if (audioSource == null)
        {
            Debug.LogError("AudioSource non trovato su " + gameObject.name);
        }
    }

    void Update()
    {
        // Se premi il tasto "E" e non sei già in interazione, avvia l'audio
        if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            StartInteraction();
        }
    }

    void StartInteraction()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Avvia l'audio solo per il GameObject con cui interagisci
            isInteracting = true; // Imposta lo stato di interazione
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto con il quale interagisci ha un AudioSource
        if (other.CompareTag("Painting"))
        {
            // Assegna il riferimento all'AudioSource dell'oggetto con il quale interagisci
            audioSource = other.GetComponent<AudioSource>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Quando esci dal trigger, interrompi l'interazione e ferma l'audio
        if (other.CompareTag("Painting"))
        {
            isInteracting = false;
            audioSource.Stop();
        }
    }
}
