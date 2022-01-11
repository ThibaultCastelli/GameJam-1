using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] KeyCode[] keyTriggers;
    [SerializeField] string[] animatorParameters;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        for (int i = 0; i < keyTriggers.Length; i++)
        {
            if (Input.GetKeyDown(keyTriggers[i]))
            {
                animator.SetTrigger(animatorParameters[i]);
            }
        }
    }
}
