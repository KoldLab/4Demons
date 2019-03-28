
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    public SceneFade fader;

    public Player player;

    private void Start()
    {
        player.Load();

        int levelReached = player.level;

        Time.timeScale = 1;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i > levelReached) //va uninteract tout les niveau que les joueurs a pas atteint
            {
                levelButtons[i].interactable = false;
            }
            
        }
    }
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
