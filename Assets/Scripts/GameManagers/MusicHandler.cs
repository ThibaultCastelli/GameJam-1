using MusicTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] MusicEvent gameMusic;

    private void Start()
    {
        gameMusic.Play();
    }
}
