using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (levelCompleted)
            return;

        if (other.CompareTag("Player"))
        {
            levelCompleted = true;

            FindObjectOfType<UIManager>()
                .ShowLevelComplete();
        }
    }
}