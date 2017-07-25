using System;
using UnityEngine;
using FireExtinguishers = FighterState.EquipableFireExtinguishers;

public class Extinguishable : MonoBehaviour
{
    public FireExtinguishers OutType1 = FireExtinguishers.None;
    public FireExtinguishers OutType2 = FireExtinguishers.None;
    public FireExtinguishers OutType3 = FireExtinguishers.None;
    public FireExtinguishers OutType4 = FireExtinguishers.None;
    public FighterState State;
    public GameObject Light;

    
    private const float DecreaseStep = 4.0f;
    private const float IncreaseStep = 2.0f;
    private float startLifeTime = 5.0f;

    private Light pointLight;

    void Start()
    {
        var system = GetComponent<ParticleSystem>();
        startLifeTime = system.startLifetime;

        pointLight = Light.GetComponent<Light>();
    }

    void Update()
    {
        if (State.ExstinguisherActive())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= 10.0f && hit.collider.name == name)
                {
                    TestExtinguisher(OutType1);
                    TestExtinguisher(OutType2);
                    TestExtinguisher(OutType3);
                    TestExtinguisher(OutType4);
                }
            }
        }
        else
        {
            var system = GetComponent<ParticleSystem>();

            system.startLifetime += Time.deltaTime / DecreaseStep;
            system.startLifetime = Math.Min(startLifeTime, system.startLifetime);
        }
    }

    void TestExtinguisher(FireExtinguishers type)
    {
        if (type == State.GetEquippedEtinguisher() && type != FireExtinguishers.None && gameObject.activeSelf)
        {
            var component = GetComponent<ParticleSystem>();

            if (component != null)
            {
                component.startLifetime -= Time.deltaTime * IncreaseStep;
                pointLight.intensity -= Time.deltaTime * IncreaseStep;

                if (component.startLifetime <= 0.0f)
                {
                    Light.SetActive(false);
                    gameObject.SetActive(false);
                    State.FirePutOut();
                }
            }
        }
    }
}
