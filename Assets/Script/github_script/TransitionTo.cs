using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class TransitionTo : MonoBehaviour
{
    public bool LastScene = false;
    public int SceneID = 0;

    public void OnClick()
    {
        if (LastScene)
        {
            var last = WebService.LastScene;

            if (last == -1)
            {
                Debug.Log("No last scene defaulting to menu");
            }

            var guest = new JSONClass
            {
                {"age", new JSONData(WebService.GuestData.age)},
                {"gender", WebService.GuestData.gender},
                {"name", WebService.GuestData.name}
            };

            StartCoroutine(WebService.Post("/start-gameplay", new JSONClass { { "guest", guest } }, (json, err) =>
            {
                WebService.GameplayID = json["idGameplay"].AsInt;
                WebService.PlayerID = json["idPlayer"].AsInt;

                SceneManager.LoadScene(Mathf.Max(last, 0));
            }));
        }
        else
        {
            SceneManager.LoadScene(SceneID);
        }
    }
}
