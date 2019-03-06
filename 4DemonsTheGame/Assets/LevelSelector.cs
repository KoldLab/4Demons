
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public SceneFade fader;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
