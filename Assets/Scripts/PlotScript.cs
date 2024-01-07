using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotScript : MonoBehaviour
{
    public GameObject explostionFx;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BB8")
        {
            GameObject explosionGameObject = Instantiate(explostionFx, transform.position, Quaternion.identity);

            Destroy(gameObject);

            SceneManagerScript.destroyedObjects++;
        }
    }
}
