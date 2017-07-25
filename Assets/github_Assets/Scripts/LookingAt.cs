using System;
using UnityEngine;
using UnityEngine.UI;

public class LookingAt : MonoBehaviour
{
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
            text.text = "Looking At: " + clickOnHazard.lookingAt;
        }
    }
}