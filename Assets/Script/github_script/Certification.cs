using UnityEngine;
using UnityEngine.UI;
using Process = System.Diagnostics.Process;

public class Certification : MonoBehaviour
{
    public void Open()
    {
        var input = GameObject.Find("NameInput").GetComponent<InputField>();
        var inputName = input.text;

        Process.Start(@"http://www.williamsamtaylor.co.uk/apps/sg/index.html#" + inputName);
    }
}
