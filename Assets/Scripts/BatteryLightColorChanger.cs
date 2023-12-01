using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryLightColorChanger : MonoBehaviour
{
    public Collider[] triggerColliders;
    public Light[] lights;
    public Material emissiveMaterial;

    private bool[] triggerStates;

    void Start()
    {
        triggerStates = new bool[triggerColliders.Length];
    }

    void OnTriggerEnter(Collider collider)
    {
        for (int i = 0; i < triggerColliders.Length; i++)
        {
            if (collider == triggerColliders[i])
            {
                Debug.Log("Rah yel7ag hna");
                triggerStates[i] = true;

                if (CheckAllTriggersActivated())
                {
                    ChangeLightsColor(Color.cyan);
                    ChangeEmissiveColor(Color.cyan * 2.0f);
                }
            }
        }
    }

    bool CheckAllTriggersActivated()
    {
        foreach (bool state in triggerStates)
        {
            if (!state)
            {
                return false;
            }
        }

        return true;
    }

    void ChangeLightsColor(Color newColor)
    {
        foreach (Light light in lights)
        {
            light.color = newColor;
        }
    }

    void ChangeEmissiveColor(Color newColor)
    {
        emissiveMaterial.SetColor("_EmissionColor", newColor);
    }
}
