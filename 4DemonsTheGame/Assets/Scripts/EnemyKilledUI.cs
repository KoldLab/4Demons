using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyKilledUI : MonoBehaviour
{
    public TextMeshProUGUI enemiesKilledText;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        enemiesKilledText.text = "0";
        int round = 0; //on commence a 0

        yield return new WaitForSecondsRealtime(.7f); // on attend le fade

        while (round < LevelStatus.EnemyKilled)//on update lecriture
        {
            round++;
            enemiesKilledText.text = round.ToString();

            yield return new WaitForSecondsRealtime(.05f); // ca va le faire tous les 0.05 secondes
        }
    }
}
