using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject DoneText;
    public GameObject TimeText;
    private bool didPlayerRun;
    private bool stopTimer;

    private ClickOnHazard clickOnHazard;
    private static float timer;
    private Scene activeScene;
    private Text timerText;

    void Start()
    {
        clickOnHazard = GameObject.Find("Floor_5").GetComponent<ClickOnHazard>();
        activeScene = SceneManager.GetActiveScene();
        timerText = TimeText.GetComponent<Text>();
        didPlayerRun = false;
        stopTimer = false;

        switch (activeScene.name)
        {
            case "Fighter": timer = 120.5f; break;
            case "Runner": timer = 120.5f; break;
            case "Hazards": timer = 90.5f; break;
            case "Safety": timer = 90.5f; break;
        }

        DoneText.SetActive(false);
    }

    IEnumerator Wait(int seconds, Action action)
    {
        yield return new WaitForSeconds(5.0f);
        action();
    }

    void NextScene(string scene, string endtext)
    {
        stopTimer = true;
        StartCoroutine(Wait(3, () => Application.LoadLevel(scene)));

        DoneText.GetComponent<Text>().text = endtext;
        DoneText.SetActive(true);
    }

    void Update()
    {
        if(stopTimer == false)
        {
            timer -= Time.deltaTime;
            timer = Math.Max(0.0f, timer);
        }
        timerText.text = "Time Left: " + (int)timer;

        if (activeScene.name == "Hazards")
        {
            if (timer <= 0.0f || clickOnHazard.collectedHazardItems.Count == 10)
            {
                stopTimer = true;
                if (clickOnHazard.collectedHazardItems.Count >= 7)
                {
                    NextScene("Runner", "Scenario Passed");
                }
                else
                {
                    NextScene("Failed", "Scenario Failed");
                }
            }
        }
        else if (activeScene.name == "Safety")
        {
            if (timer <= 0.0f || clickOnHazard.collectedSafetyItems.Count == 16)
            {
                stopTimer = true;
                if (clickOnHazard.collectedSafetyItems.Count >= 10)
                {
                    NextScene("Hazards", "Scenario Passed");
                }
                else
                {
                    NextScene("Failed", "Scenario Failed");
                }
            }
        }
        else if (activeScene.name == "Runner")
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                didPlayerRun = true;
            }

            if(timer <= 0.0f || LeavingBuilding.leftBuilding)
            {
                stopTimer = true;
                if((LeavingBuilding.leftBuilding) && (didPlayerRun == false))
                {
                    NextScene("Fighter", "Scenario Passed");
                }
                else
                {
                    NextScene("Failed", "Scenario Failed");
                }
            }
        }
    }
}
