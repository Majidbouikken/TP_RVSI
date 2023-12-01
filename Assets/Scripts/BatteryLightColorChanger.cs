using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryLightColorChanger : MonoBehaviour
{
    public Light[] lights;
    public Collider[] triggerColliders;

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
}
