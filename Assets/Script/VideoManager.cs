using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

/// <summary>
/// Classe che gestisce la riproduzione del video
/// </summary>
public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;     // L'oggetto VideoPlayer che contiene il video

    private float videoTime;            // La variabile videoTime che contiene la durata del video
    
    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    void Awake()
    {
        videoPlayer.playOnAwake = false;    // In questo modo non si fa partire il video all'avvio dell'applicazione

        videoPlayer.Prepare();  // Carica il video prima che venga avviato così da non esserci problemi nella riproduzione

        videoTime = (float)videoPlayer.length - 1; // Salva la durata del video con una sottrazione di -1 perché unity si porta avanti di
                                                   // qualche frame rispetto all'effettiva durata del video

        Invoke("videoEnded", videoTime);    // Invoca il metodo dopo un numero di secondi in questo caso alla fine del video
    }

    // Metodo che si ripete ciclicamente per controllare e fare operazioni che avviene una volta per frame
    // e che determina cosa avviene in scena
    void Update()
    {
        // Controlla se la clip del video è pronta per essere avviata
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();     // Fa partire il video quando è pronto
        }
    }

    /// <summary>
    /// Termina il video, il metodo viene invocato tramite Invoke per passare alla scena successiva
    /// </summary>
    void videoEnded()
    {
        SceneManager.LoadScene("MainMenu");     // Carica la scena del Main Menu
    }
}
