using UnityEngine;

/// <summary>
/// Classe che gestisce l'audio della descrizione del quadro
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;         // Componente che permette di riprodurre audio
    private AudioClip currentAudioClip;     // Componente che contiene la traccia audio da riprodurre

    // Metodo Start che si avvia quando lo script è eseguito
    void Start()
    {
        audioSource.Stop();  // Ferma qualsiasi audio all'inizio
        currentAudioClip = null;
    }


    /// <summary>
    /// Metodo per aggiornare l'audio
    /// </summary>
    /// <param name="newAudioClip">Traccia audio</param>
    public void UpdateAudio(AudioClip newAudioClip)
    {
        // Controlla se la nuova traccia audio è diversa da quella corrente
        if (newAudioClip != currentAudioClip)
        {
            currentAudioClip = newAudioClip;    // Imposta l'audio corrente
            audioSource.clip = newAudioClip;    // Assegna l'AudioClip all'AudioSource

            audioSource.Stop();     // Ferma l'audio precedente
            audioSource.Play();     // Avvia la riproduzione dell'audio

        }
    }

    /// <summary>
    /// Metodo per fermare l'audio corrente
    /// </summary>
    public void StopAudio()
    {
        audioSource.Stop();
        currentAudioClip = null;
    }
}




