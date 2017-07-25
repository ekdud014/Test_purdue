using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject GUI;

    public void OnPlay()
    {
        GUI.GetComponent<GuiManager>().OnPlay();
    }

    public void OnLeaderboard()
    {
        GUI.GetComponent<GuiManager>().OnLeaderboard();
    }

    public void OnCertification()
    {
        GUI.GetComponent<GuiManager>().OnCertification();
    }

    public void OnCloseLeaderboard()
    {
        GUI.GetComponent<GuiManager>().OnCloseLeaderboard();
    }

    public void OnCloseLogin()
    {
        GUI.GetComponent<GuiManager>().OnCloseLogin();
    }

    public void OnLogin()
    {
        GUI.GetComponent<GuiManager>().OnLogin();
    }


    private string GetInput(string gameObjectName)
    {
        var go = GameObject.Find(gameObjectName);
        var input = go.GetComponent<InputField>();
        return input.text;
    }

    public void OnLoginGuest()
    {
        var guestName = GetInput("NameInput");
        var guestAge = Convert.ToInt32(GetInput("AgeInput"));
        var gender = GetInput("GenderInput");

        GUI.GetComponent<GuiManager>().OnLoginGuest(guestName, guestAge, gender);
    }

    public void OnLoginStudent()
    {
        var username = GetInput("UsernameInput");
        var password = GetInput("PasswordInput");

        GUI.GetComponent<GuiManager>().OnLoginTeacher(username, password);
    }

    public void OnQuit()
    {
        GUI.GetComponent<GuiManager>().OnQuit();
    }
}
