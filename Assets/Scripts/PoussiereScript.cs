using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoussiereScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        GetComponentInParent<BB8Script>(true).HandleCollision(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        GetComponentInParent<BB8Script>(true).HandleCollision(collision);
    }
}
