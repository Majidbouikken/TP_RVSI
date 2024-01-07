using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerScript : MonoBehaviour
{
    private Material mat;

    public float DISTANCE_MAX = 10f;
    public Transform bb8;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float distance = Vector3.Distance(bb8.position, transform.position);
        if (distance < DISTANCE_MAX)
        {
            mat.SetFloat("_ChangeColorAB", 1f - (distance / DISTANCE_MAX));
        }
        else
        {
            mat.SetFloat("_ChangeColorAB", 0f);
        }
    }
}
