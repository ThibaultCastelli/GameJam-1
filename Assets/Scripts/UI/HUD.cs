using CustomVariablesTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("INFOS")]
    [SerializeField] IntReference playerLife;
    [SerializeField] IntReference currScore;
    [SerializeField] IntReference highscore;

    [Header("COMPONENTS")]
    [SerializeField] Image[] hearts;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI highscoreTxt;

    private int _currLife;
    private int _currScore;

    private void Start()
    {
        _currLife = playerLife.Value;
        _currScore = currScore.Value;

        DisplayScore();
    }

    private void Update()
    {
        if (_currLife != playerLife.Value)
        { 
            _currLife = playerLife.Value;
            DisplayHearts();
        }

        if (_currScore != currScore.Value)
        {
            _currScore = currScore.Value;
            DisplayScore();
        }
    }

    private void DisplayHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < playerLife.Value)
                hearts[i].gameObject.SetActive(true);
            else
                hearts[i].gameObject.SetActive(false);
        }
    }

    private void DisplayScore()
    {
        scoreTxt.text = currScore.Value.ToString();
        highscoreTxt.text = highscore.Value.ToString();
    }
}
