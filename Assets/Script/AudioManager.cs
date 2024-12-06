using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Riferimento all'AudioSource
    private AudioClip currentAudioClip;

    void Start()
    {
        audioSource.Stop();  // Ferma qualsiasi audio all'inizio
        currentAudioClip = null;
    }

    // Metodo per aggiornare l'audio
    public void UpdateAudio(AudioClip newAudioClip)
    {
        if (newAudioClip != currentAudioClip)
        {
            currentAudioClip = newAudioClip; // Imposta l'audio corrente
            audioSource.clip = newAudioClip; // Assegna l'AudioClip all'AudioSource

            audioSource.Stop();  // Ferma l'audio precedente
            audioSource.Play();  // Avvia la riproduzione dell'audio
            Debug.Log("Audio aggiornato e riprodotto: " + newAudioClip.name); // Debugging
        }
        else
        {
            Debug.Log("Audio già in riproduzione, nessuna modifica: " + currentAudioClip.name); // Debugging
        }
    }

    // Metodo per fermare l'audio
    public void StopAudio()
    {
        audioSource.Stop();
        currentAudioClip = null;
    }
}




