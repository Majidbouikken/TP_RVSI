using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public Camera globalCamera;
    public GameObject playerArmature;

    void Start()
    {
        globalCamera.enabled = true;
        playerArmature.SetActive(false);

        Invoke("SpawnPlayer", 3f); // On affiche le joueur apres 3 secondes
    }

    // La fonction SpawnPlayer est responsable de l'apparition du joueur apres 3 secondes
    void SpawnPlayer()
    {
        globalCamera.enabled = false;
        playerArmature.SetActive(true);
    }
}
