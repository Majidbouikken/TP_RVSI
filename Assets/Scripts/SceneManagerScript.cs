using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public Camera globalCamera;
    public GameObject playerArmature;
    [Header("Countdown")]
    public int countdownDuration = 3;
    public TMP_Text countdownText;
    [Header("Destroyables")]
    public GameObject[] destroyableOBjects;
    public static int totalDestroyableOBjects = 0;
    public static int destroyedObjects = 0;
    public TMP_Text destroyedCount;
    public TMP_Text totalDestroyableCount;

    public event System.Action CountdownCompleted;

    void Start()
    {
        globalCamera.enabled = true;
        playerArmature.SetActive(false);

        countdownText.text = countdownDuration.ToString();
        totalDestroyableOBjects = destroyableOBjects.Length;
        totalDestroyableCount.text = "/" + totalDestroyableOBjects.ToString();

        StartCoroutine(CountdownCoroutine());

        Invoke("SpawnPlayer", countdownDuration); // On affiche le joueur apres 3 secondes
    }

    private void Update()
    {
        destroyedCount.text = destroyedObjects.ToString();
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

    public static bool WinningCondition()
    {
        return destroyedObjects == totalDestroyableOBjects;
    }

    public static void DisplayWinningText()
    {
        GameObject victoryText = GameObject.Find("GUI").transform.Find("Victory Text").gameObject;
        if (victoryText != null)
        {
            victoryText.SetActive(true);
        }
    }

    public static void DisplayDefeatText()
    {
        GameObject defeatText = GameObject.Find("GUI").transform.Find("Defeat Text").gameObject;
        if (defeatText != null)
        {
            defeatText.SetActive(true);
        }
    }
}
