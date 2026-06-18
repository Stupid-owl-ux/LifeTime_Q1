using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerAge playerAge;

    public TMP_Text ageText;
    public TMP_Text stageText;
    public TMP_Text stagePopup;

    public GameObject startPanel;
    public GameObject levelCompletePanel;

    private bool levelLoading = false;

    void Update()
    {
        ageText.text = "Age: " + Mathf.FloorToInt(playerAge.currentAge);

        stageText.text = "Stage: " + playerAge.GetCurrentStage();
    }

    public void ShowPopup(string message)
    {
        StartCoroutine(PopupRoutine(message));
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        if (levelLoading)
            return;

        levelLoading = true;

        levelCompletePanel.SetActive(false);

        Time.timeScale = 1f;

        int nextLevel = SaveManager.CurrentLevel + 1;

        SaveManager.CurrentLevel = nextLevel;

        if (nextLevel <= 3)
        {
            UnityEngine.SceneManagement.SceneManager
                .LoadScene("Level" + nextLevel);
        }
        else
        {
            stagePopup.gameObject.SetActive(true);

            stagePopup.text =
                "CONGRATULATIONS!!You survived an entire lifetime.\n\n" +
                "Prototype Completed!";
        }
    }

    public void ReturnToAgeSelection()
    {
        SaveManager.ShowStartPanel = true;

        levelCompletePanel.SetActive(false);

        startPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    IEnumerator PopupRoutine(string message)
    {
        stagePopup.gameObject.SetActive(true);

        stagePopup.text = message;

        yield return new WaitForSeconds(2f);

        stagePopup.gameObject.SetActive(false);
    }
}