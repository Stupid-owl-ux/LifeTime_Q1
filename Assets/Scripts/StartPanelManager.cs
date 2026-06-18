using UnityEngine;
using TMPro;

public class StartPanelManager : MonoBehaviour
{
    public GameObject startPanel;

    public TMP_InputField ageInput;

    public TMP_Text stagePreviewText;

    void Start()
    {
        if (SaveManager.ShowStartPanel)
        {
            startPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            startPanel.SetActive(false);
            Time.timeScale = 1f;
        }

        ageInput.text = SaveManager.StartAge.ToString();

        UpdateStagePreview();
    }

    void Update()
    {
        UpdateStagePreview();
    }

    public void UpdateStagePreview()
    {
        int age;

        if (!int.TryParse(ageInput.text, out age))
        {
            stagePreviewText.text = "Stage: Child";
            return;
        }

        if (age < 20)
        {
            stagePreviewText.text = "Stage: Child";
        }
        else if (age < 60)
        {
            stagePreviewText.text = "Stage: Adult";
        }
        else
        {
            stagePreviewText.text = "Stage: Elder";
        }
    }

    public void StartGame()
    {
        int age;

        if (int.TryParse(ageInput.text, out age))
        {
            age = Mathf.Clamp(age, 1, 99);

            SaveManager.StartAge = age;
            SaveManager.CurrentAge = age;
        }
        else
        {
            SaveManager.StartAge = 10;
            SaveManager.CurrentAge = 10;
        }

        PlayerAge player = FindObjectOfType<PlayerAge>();

        if (player != null)
        {
            player.currentAge = SaveManager.CurrentAge;
        }

        SaveManager.ShowStartPanel = false;

        Time.timeScale = 1f;

        startPanel.SetActive(false);
    }
}