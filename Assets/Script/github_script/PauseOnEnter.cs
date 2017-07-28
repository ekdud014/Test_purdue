using UnityEngine;
using System.Collections;

public class PauseOnEnter : MonoBehaviour
{
    public GameObject InstructionsText;
    public GameObject PressKeyText;

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            InstructionsText.SetActive(false);
            PressKeyText.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
