using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Riferimento all'AudioSource
    public AudioClip footstepClip;          // Clip audio del passo
    public float stepInterval = 0.5f;      // Tempo tra i passi
    private float stepTimer = 0f;          // Timer per gestire l'intervallo tra i passi
    private Rigidbody rb;                   // Riferimento al Rigidbody del personaggio

    void Start()
    {
        // Ottieni il riferimento al Rigidbody
        rb = GetComponentInChildren<Rigidbody>();
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Solo se il personaggio si sta muovendo
        if (rb.velocity.magnitude > 0.1f) // Controlla se la velocità del Rigidbody è sufficiente
        {
            stepTimer += Time.deltaTime;

            // Se è passato abbastanza tempo tra i passi, riproduci il suono
            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f; // Reset del timer
            }
        }
        else
        {
            stepTimer = 0f; // Resetta il timer se il personaggio non si muove
        }
    }

    // Funzione per riprodurre il suono del passo
    private void PlayFootstepSound()
    {
        if (footstepClip != null)
        {
            footstepAudioSource.PlayOneShot(footstepClip);
        }
    }
}