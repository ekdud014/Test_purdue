using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FighterState : MonoBehaviour
{
    public enum EquipableFireExtinguishers
    {
        None, CO2, Foam, Powder, Water
    }

    public GameObject EquippedText;
    public GameObject TimeText;
    public GameObject DoneText;
    public GameObject Smoke;

    private EquipableFireExtinguishers equippedExtinguisher = EquipableFireExtinguishers.None;
    private float timerCountdown = 0.0f;
    private bool timerStarted = false;
    private int firesPutOut = 0;

    public bool ExstinguisherActive()
    {
        return Smoke.activeSelf;
    }

    public EquipableFireExtinguishers GetEquippedEtinguisher()
    {
        return equippedExtinguisher;
    }

    public void SetFireExtinguisher(EquipableFireExtinguishers extinguishers)
    {
        equippedExtinguisher = extinguishers;

        var text = EquippedText.GetComponent<Text>();
        text.text = "Equipped Extinguisher: " + extinguishers;
    }

    void Start()
    {
        timerCountdown = 120.5f;
        timerStarted = true;

        DoneText.SetActive(false);
        Smoke.SetActive(false);
    }

    public void FirePutOut()
    {
        WebService.PostAction(this, "fires");
        ++firesPutOut;
    }

    void Update()
    {
        if (timerStarted)
        {
            timerCountdown -= Time.deltaTime;
            timerCountdown = Mathf.Max(0.0f, timerCountdown);
        }

        var text = TimeText.GetComponent<Text>();
        text.text = "Time Left: " + (int)timerCountdown;

        if (Input.GetMouseButtonDown(0) && equippedExtinguisher != EquipableFireExtinguishers.None)
        {
            Smoke.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Smoke.SetActive(false);
        }

        Smoke.transform.Rotate(Vector3.down, Input.GetAxis("Mouse Y"));
        Smoke.transform.Rotate(Vector3.left, Input.GetAxis("Mouse X") / 50.0f);

        if (timerCountdown <= 0.0 && !LeavingBuilding.leftBuilding && firesPutOut < 6)
        {
            DoneText.GetComponent<Text>().text = "Scenario Failed";
            DoneText.SetActive(true);

            StartCoroutine(Transition(6));
        }
        else if (firesPutOut == 6 || LeavingBuilding.leftBuilding)
        {
            DoneText.GetComponent<Text>().text = "Scenario Passed";
            DoneText.SetActive(true);

            StartCoroutine(Transition(7));
        }
    }

    IEnumerator Transition(int scene)
    {
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene(scene);
    }
}
