using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoParent : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null);
    }
}
