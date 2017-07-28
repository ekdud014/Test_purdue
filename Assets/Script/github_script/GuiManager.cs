using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    private List<GameObject> scores = new List<GameObject>();
    private string popupMessage = string.Empty;
    private bool showingPopup = false;
    private bool loginTeacher = true;

    public GameObject TeacherLogin;
    public GameObject GuestLogin;
    public GameObject IntroVideo;
    public GameObject Leaderboard;
    public GameObject Login;

    void Start()
    {
        ToggleLoginType();

        if (Leaderboard != null && Login != null)
        {
            Leaderboard.SetActive(false);
            IntroVideo.SetActive(false);
            Login.SetActive(false);

            StartCoroutine(WebService.Get("/leaderboard", (json, err) =>
            {
                var added = new List<string>();
                var names = json["fires_put_out"].AsArray;

                if (names != null)
                {
                    var y = 175;

                    for (var i = 0; i < Math.Min(10, names.Count); i++)
                    {
                        var firesPutOut = names[i]["score"].AsInt;
                        var marshalName = names[i]["name"].ToString();

                        marshalName = marshalName.Substring(1, marshalName.Length - 2);

                        if (firesPutOut >= 6 && !added.Contains(marshalName) && !string.IsNullOrEmpty(marshalName))
                        {
                            scores.Add(NewTextElement(i, marshalName, y, 30));
                            added.Add(marshalName);
                            y -= 55;
                        }
                    }
                }
            }));
        }
    }

    private void StartGame()
    {
        var video = IntroVideo.GetComponent<VideoPlayer>();
        video.OnFinished = () => SceneManager.LoadScene(1);
  
        IntroVideo.SetActive(true);
    }

    public void ToggleLoginType()
    {
        loginTeacher = !loginTeacher;

        TeacherLogin.SetActive(loginTeacher);
        GuestLogin.SetActive(!loginTeacher);
    }

    public void OnPlay()
    {
        if (showingPopup) return;

        if (WebService.GuestLoggedIn)
        {
            var guest = new JSONClass
            {
                {"age", new JSONData(WebService.GuestData.age)},
                {"gender", WebService.GuestData.gender},
                {"name", WebService.GuestData.name}
            };

            WebService.StartGameplay(this, guest, null, StartGame);
        }
        else if (WebService.PlayerLoggedIn)
        {
            var player = new JSONClass
            {
                {"idPlayer", new JSONData(WebService.GuestData.age)}
            };

            WebService.StartGameplay(this, null, player, StartGame);
        }
        else
        {
            ShowMessageBox("Error you must login before playing");
        }
    }

    public void OnCloseLogin()
    {
        if (showingPopup) return;
        Login.SetActive(false);
    }

    public void OnCloseLeaderboard()
    {
        if (showingPopup) return;

        Leaderboard.SetActive(false);
        foreach (var go in scores)
        {
            go.SetActive(false);
        }
    }

    public void OnLeaderboard()
    {
        if (showingPopup) return;

        Leaderboard.SetActive(true);
        foreach (var go in scores)
        {
            go.SetActive(true);
        }
    }

    private GameObject NewTextElement(int uniqueID, string contents, int y, int fontSize)
    {
        var textObject = new GameObject("score" + uniqueID);
        textObject.SetActive(false);

        var text = textObject.AddComponent<Text>();
        text.text = contents;
        text.alignment = TextAnchor.MiddleCenter;
        text.fontSize = fontSize;
        text.color = Color.black;
        text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.transform.SetParent(Leaderboard.transform);
        text.supportRichText = true;

        var rect = textObject.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
        rect.localPosition = new Vector3(0, y, 0);
        rect.sizeDelta = new Vector2(500, 100);

        return textObject;
    }

    public void OnCertification()
    {
        System.Diagnostics.Process.Start(@"http://www.williamsamtaylor.co.uk/apps/sg/index.html#william taylor");
    }

    public void OnLogin()
    {
        if (showingPopup) return;

        Login.SetActive(true);
    }

    void OnGUI()
    {
        if (showingPopup)
        {
            var x = Screen.width / 2;
            var y = Screen.height / 2;
            var w = 300;
            var h = 100;

            GUI.backgroundColor = Color.white;
            GUI.color = Color.white;
            GUI.skin.window.normal.background = Texture2D.whiteTexture;
            GUI.skin.window.normal.textColor = Color.black;
            GUI.skin.window.active = GUI.skin.window.normal;
            GUI.skin.window.focused = GUI.skin.window.normal;
            GUI.skin.window.hover = GUI.skin.window.normal;
            GUI.skin.window.onActive = GUI.skin.window.normal;
            GUI.skin.window.onHover = GUI.skin.window.normal;
            GUI.skin.window.onFocused = GUI.skin.window.normal;
            GUI.Window(0, new Rect(x - w / 2, y - h / 2, w, h), id => StartCoroutine(ShowMessage()), "", GUI.skin.window);
        }
    }

    IEnumerator ShowMessage()
    {
        GUI.skin.label.fontSize = 14;
        GUI.skin.label.normal.textColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(0, 20, 300, 30), popupMessage, new GUIStyle(GUI.skin.label));
        GUI.skin.button.normal = GUI.skin.button.onHover;

        if (GUI.Button(new Rect(50, 60, 200, 30), "OK", new GUIStyle(GUI.skin.button)))
        {
            yield return new WaitForSeconds(0.25f);
            showingPopup = false;
        }
    }

    public void ShowMessageBox(string msg)
    {
        showingPopup = true;
        popupMessage = msg;
    }

    public void OnLoginGuest(string name, int age, string gender)
    {
        if (showingPopup) return;

        StartCoroutine(WebService.Post("/login-guest", new JSONClass(), (data, error) =>
        {
            if (string.IsNullOrEmpty(error) && data["version"] != null)
            {
                Login.SetActive(false);

                WebService.GuestData.gender = gender;
                WebService.GuestData.name = name;
                WebService.GuestData.age = age;
                WebService.GuestLoggedIn = true;

                ShowMessageBox("Guest logged in");
            }
            else
            {
                ShowMessageBox("Guest could not login as game is not public");
            }
        }));
    }

    public void OnLoginTeacher(string username, string password)
    {
        if (showingPopup) return;

        var loginData = new JSONClass();
        loginData["username"] = username;
        loginData["password"] = password;

        StartCoroutine(WebService.Post("/login-student", loginData, (data, error) =>
        {
            if (string.IsNullOrEmpty(error) && data["version"] != null)
            {
                WebService.PlayerID = data["idPlayer"].AsInt;
                WebService.PlayerLoggedIn = true;

                Login.SetActive(false);

                ShowMessageBox("Teacher logged in");
            }
            else
            {
                ShowMessageBox("Teacher could not login");
            }
        }));
    }

    public void OnQuit()
    {
        if (showingPopup) return;
        Application.Quit();
    }
}
