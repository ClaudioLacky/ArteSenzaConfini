using UnityEngine;

/// <summary>
/// Classe che gestisce l'audio dei passi del player
/// </summary>
public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource;         // Componente che permette di riprodurre audio
    public AudioClip footstepClip;                  // Clip audio del passo
    public float stepInterval = 0.5f;               // Tempo tra i passi
    private float stepTimer = 0f;                   // Timer per gestire l'intervallo tra i passi
    private Rigidbody rb;                           // Riferimento al Rigidbody del personaggio

    // Metodo Start che si avvia all'esecuzione dello script
    void Start()
    {
        // Ottieni il riferimento al Rigidbody
        rb = GetComponentInChildren<Rigidbody>();
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>();
        }
    }

    // Metodo che si ripete ciclicamente per controllare e fare operazioni che avviene una volta per frame
    // e che determina cosa avviene in scena
    void Update()
    {
        // Solo se il personaggio si sta muovendo
        if (rb.velocity.magnitude > 0.1f)               // Controlla se la velocità del Rigidbody è sufficiente
        {
            stepTimer += Time.deltaTime;

            // Se è passato abbastanza tempo tra i passi, riproduci il suono
            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;                         // Reset del timer
            }
        }
        else
        {
            stepTimer = 0f;                             // Resetta il timer se il personaggio non si muove
        }
    }

    /// <summary>
    /// Metodo per riprodurre il suono del passo
    /// </summary>
    private void PlayFootstepSound()
    {
        // Se è presente una traccia audio la riproduce
        if (footstepClip != null)
        {
            footstepAudioSource.PlayOneShot(footstepClip);      // Riproduce la traccia audio una volta sola
        }
    }
}