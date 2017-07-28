using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;

public class WebService : MonoBehaviour
{
    public struct GuestInfo
    {
        public string gender;
        public string name;
        public int age;
    };

    private const string Server = "http://dev.williamsamtaylor.co.uk:3002";

    public static int LastScene = -1;
    public static int GameplayID = -1;
    public static int PlayerID = -1;

    public static bool GuestLoggedIn = false;
    public static bool PlayerLoggedIn = false;
    public static GuestInfo GuestData;

    public static IEnumerator<WWW> Get(string url, Action<JSONNode, string> action)
    {
        var www = new WWW(Server + url);
        yield return www;
        action(JSON.Parse(www.text), www.error);
    }

    public static void ResetState()
    {
        GameplayID = -1;
        PlayerID = -1;
    }

    public static IEnumerator<WWW> Post(string url, JSONClass json, Action<JSONNode, string> action)
    {
        var bytes = Encoding.UTF8.GetBytes(json.ToString());
        var headers = new Dictionary<string, string>
        {
            {"Content-Type", "application/json"}
        };

        var request = new WWW(Server + url, bytes, headers);
        yield return request;
        action(JSON.Parse(request.text), request.error);
    }

    public static void StartGameplay(MonoBehaviour mono, JSONClass guest, JSONClass student, Action onStart)
    {
        var body = new JSONClass();

        if (student != null)
        {
            body["student"] = student;
        }
        else
        {
            body["guest"] = guest;
        }
  
        mono.StartCoroutine(Post("/start-gameplay", body, (json, err) =>
        {
            GameplayID = json["idGameplay"].AsInt;
            PlayerID = json["idPlayer"].AsInt;

            onStart();
        }));
    }

    public static void PostAction(MonoBehaviour mono, string type)
    {
        if (GameplayID != -1 && PlayerID != -1)
        {
            var body = new JSONClass
            {
                {"gameplayID", new JSONData(GameplayID)},
                {"type", type}
            };

            mono.StartCoroutine(Post("/action-gameplay", body, (a, b) => { Debug.Log("Posted Action");}));
        }
    }

    public static void EndGameplay(MonoBehaviour mono, string url)
    {
        if (GameplayID != -1 && PlayerID != -1)
        {
            var body = new JSONClass();
            body["gameplayID"] = new JSONData(GameplayID);
            mono.StartCoroutine(Post(url, body, (json, err) => ResetState()));
        }
    }
}
