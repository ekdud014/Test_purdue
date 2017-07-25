using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Finished : MonoBehaviour
{
    public GameObject RetryButton;
    public List<MovieTexture> videos;
    public List<AudioClip> audio;

    public bool FailedScene = false;
    public string Url;

    void Start()
    {

        if (string.IsNullOrEmpty(Url))
            return;

        if (FailedScene)
        {
            RetryButton.SetActive(false);
            PresentVideo();
        }

        WebService.EndGameplay(this, Url);
    }

    void PresentVideo()
    {
        var lastSceneIndex = WebService.LastScene - 1;

        if (lastSceneIndex != -1)
        {
            var videoObject = GameObject.Find("Video");
            var player = videoObject.GetComponent<VideoPlayer>();
            var source = videoObject.GetComponent<AudioSource>();

            source.clip = audio[lastSceneIndex];

            player.PlayOnLoad = true;
            player.OnFinished = () => RetryButton.SetActive(true);
            player.movTexture = videos[lastSceneIndex];
            player.TryLoad();
        }
    }
}
