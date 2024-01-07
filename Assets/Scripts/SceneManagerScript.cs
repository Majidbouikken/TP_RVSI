using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public Camera globalCamera;
    public GameObject playerArmature;
    public int countdownDuration = 3;
    public TMP_Text countdownText;

    public event System.Action CountdownCompleted;


    void Start()
    {
        globalCamera.enabled = true;
        playerArmature.SetActive(false);

        countdownText.text = countdownDuration.ToString();

        StartCoroutine(CountdownCoroutine());

        Invoke("SpawnPlayer", countdownDuration); // On affiche le joueur apres 3 secondes
    }

    // La fonction SpawnPlayer est responsable de l'apparition du joueur apres 3 secondes
    void SpawnPlayer()
    {
        globalCamera.enabled = false;
        playerArmature.SetActive(true);
    }

    private IEnumerator CountdownCoroutine() {
        float elapsedTime = 0f;
        while (elapsedTime < countdownDuration)
        {
            float remainingTime = countdownDuration - elapsedTime;

            countdownText.text = Mathf.Ceil(remainingTime).ToString();

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        Destroy(countdownText);
        OnCountdownCompleted();
    }

    private void OnCountdownCompleted()
    {
        if (CountdownCompleted != null)
        {
            CountdownCompleted.Invoke();
        }
    }
}
