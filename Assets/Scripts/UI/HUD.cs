using CustomVariablesTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("INFOS")]
    [SerializeField] IntReference playerLife;

    [Header("COMPONENTS")]
    [SerializeField] Image[] hearts;

    private void Start()
    {
        playerLife.OnValueChange += DisplayHearts;
        Debug.Log("test");
    }

    private void DisplayHearts()
    {
        Debug.Log("coucu");
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < playerLife.Value)
                hearts[i].gameObject.SetActive(true);
            else
                hearts[i].gameObject.SetActive(false);
        }
    }
}
