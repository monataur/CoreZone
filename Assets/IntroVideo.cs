using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer theVideoPlayer;
    public string videoType;
    // 0 = Splash | 1 = Intro FMV

    void Start()
    {
        theVideoPlayer.loopPointReached += Progress;
    }

    public void Progress(UnityEngine.Video.VideoPlayer vp)
    {
        switch (videoType)
        {
            case "Splash":
                Debug.Log("Splash is done.");
                SceneManager.LoadScene("Intro");
                break;

            case "Intro":
                Debug.Log("Intro FMV is done.");
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
