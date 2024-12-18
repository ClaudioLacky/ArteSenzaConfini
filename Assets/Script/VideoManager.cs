using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    public VideoPlayer videoPlayer;

    private double time;
    private double currentTime;

    float videoTime;

    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer.playOnAwake = false;

        videoPlayer.Prepare();

        videoTime = (float)videoPlayer.length - 1;
        Invoke("videoEnded", videoTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPrepared)
        {
            Debug.Log("Via");
            videoPlayer.Play();
        }
        //currentTime = videoPlayer.time;
    }

    void videoEnded()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
