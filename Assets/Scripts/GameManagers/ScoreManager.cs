using CustomVariablesTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] IntReference currScore;
    [SerializeField] IntReference highscore;

    private void Awake()
    {
        currScore.Value = 0;
    }

    public void UpdateScore(int amount)
    {
        currScore.Value += amount;

        if (currScore.Value > highscore.Value)
            highscore.Value = currScore.Value;
    }
}
