using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Safety 16
    Hazard 10
*/
public class Score : MonoBehaviour
{
    public string ScoreName;

    private ClickOnHazard clickOnHazard;
    private Text text;

    void Start()
    {
        clickOnHazard = GameObject.Find("Floor_5").GetComponent<ClickOnHazard>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (clickOnHazard != null)
        {
            text.text = ScoreName + Environment.NewLine;
            foreach (var identifiedItem in clickOnHazard.identified)
            {
                text.text += Environment.NewLine + clickOnHazard.descriptions[identifiedItem];
            }
        }
    }
}