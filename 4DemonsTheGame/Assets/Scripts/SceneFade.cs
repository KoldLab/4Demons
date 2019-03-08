using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    public Image img; //image that fades
    public AnimationCurve curve;
    private Time elapsedTime;

    private void Start()
    {
        StartCoroutine(FadeIn());       
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn() //fonction qui run en background comme un update mais qu'on controle
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime * 1f; //plus le chiffre augmente moins le fade est rapide
            float a = curve.Evaluate(t); //la fonction va prendre comme x la valeur de T
            img.color = new Color(0f, 0f, 0f, a); // couleur noir qui devient transparente
            yield return 0; //attend la prochaine frame jusqu'a t = 0
        }

    }

    IEnumerator FadeOut(string scene) //fonction  qui run en background comme un update mais qu'on controle
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * 1f; //plus le chiffre augmente moins le fade est rapide
            float a = curve.Evaluate(t); //la fonction va prendre comme x la valeur de T
            img.color = new Color(0f, 0f, 0f, a); // couleur noir qui devient transparente
            yield return 0; //attend la prochaine frame jusqu'a t = 0
        }
        SceneManager.LoadScene(scene);
    }



}