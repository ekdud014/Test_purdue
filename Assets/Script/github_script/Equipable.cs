using UnityEngine;
using UnityEngine.SceneManagement;

public class Equipable : MonoBehaviour
{
    public FighterState State;

    private string _currentScene = string.Empty;

    void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (!_currentScene.Contains("Fighter"))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == name)
                {
                    if (name.Contains("co2"))
                    {
                        State.SetFireExtinguisher(FighterState.EquipableFireExtinguishers.CO2);
                    }
                    else if (name.Contains("foam"))
                    {
                        State.SetFireExtinguisher(FighterState.EquipableFireExtinguishers.Foam);
                    }
                    else if (name.Contains("powder"))
                    {
                        State.SetFireExtinguisher(FighterState.EquipableFireExtinguishers.Powder);
                    }
                    else if (name.Contains("water"))
                    {
                        State.SetFireExtinguisher(FighterState.EquipableFireExtinguishers.Water);
                    }
                }
            }
        }
    }
}
