using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static int StartAge = 10;

    public static float CurrentAge = -1;

    public static bool ShowStartPanel = true;

    public static int CurrentLevel
    {
        get { return PlayerPrefs.GetInt("CurrentLevel", 1); }
        set { PlayerPrefs.SetInt("CurrentLevel", value); }
    }
}