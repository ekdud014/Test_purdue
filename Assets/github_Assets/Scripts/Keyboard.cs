using UnityEngine;
using UnityEngine.SceneManagement;

public class Keyboard : MonoBehaviour
{
    private Scene scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void Update()
    {
        if (Input.GetKeyDown("escape") && scene.name == "Menu" && !IntroVideoPlaying())
        {
            WebService.EndGameplay(this, "/fail-gameplay");

            Application.Quit();
        }
    }

    public void OnApplicationQuit()
    {
        WebService.EndGameplay(this, "/fail-gameplay");
    }

    private bool IntroVideoPlaying()
    {
        var video = GameObject.Find("IntroVideo");
        var playing = false;

        if (video != null)
        {
            var player = video.GetComponent<VideoPlayer>();

            if (player != null)
            {
                playing = player.movTexture.isPlaying;
            }
        }

        return playing;
    }
}
