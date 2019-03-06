
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public SceneFade fader;

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
