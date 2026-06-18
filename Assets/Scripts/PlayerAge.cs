using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAge : MonoBehaviour
{
    public float currentAge;
    public float ageRate = 20f;

    private UIManager uiManager;
    private Renderer cubeRenderer;

    private int currentStage = 0;

    void Start()
    {


        if (SaveManager.CurrentAge < 0)
        {
            currentAge = SaveManager.StartAge;
        }
        else
        {
            currentAge = SaveManager.CurrentAge;
        }

        cubeRenderer = GetComponent<Renderer>();

        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        currentAge += ageRate * Time.deltaTime;

        SaveManager.CurrentAge = currentAge;

        float scale;

        // CHILD
        if (currentAge < 20)
        {
            scale = Mathf.Lerp(0.3f, 1f, currentAge / 20f);
            cubeRenderer.material.color = Color.cyan;
        }
        // ADULT
        else if (currentAge < 60)
        {
            scale = 1f;
            cubeRenderer.material.color = Color.green;
        }
        // ELDER
        else
        {
            scale = Mathf.Lerp(1f, 0.5f, (currentAge - 60f) / 40f);
            cubeRenderer.material.color = new Color(1f, 0.5f, 0f);
        }

        transform.localScale = Vector3.one * scale;

        CheckStageTransition();

        if (currentAge >= 100)
        {
            DieOfOldAge();
        }
    }

    void CheckStageTransition()
    {
        if (currentAge >= 20 && currentStage == 0)
        {
            currentStage = 1;

            if (uiManager != null)
            {
                uiManager.ShowPopup(
                    "ADULTHOOD REACHED\nCombat Unlocked");
            }
        }

        if (currentAge >= 60 && currentStage == 1)
        {
            currentStage = 2;

            if (uiManager != null)
            {
                uiManager.ShowPopup(
                    "OLD AGE APPROACHES\nTriple Shot Unlocked");
            }
        }
    }

    void DieOfOldAge()
    {
        SaveManager.CurrentAge = 10;
        SaveManager.StartAge = 10;

        SceneManager.LoadScene(
            "Level" + SaveManager.CurrentLevel);
    }

    public string GetCurrentStage()
    {
        if (currentAge < 20)
            return "Child";

        if (currentAge < 60)
            return "Adult";

        return "Elder";
    }
}