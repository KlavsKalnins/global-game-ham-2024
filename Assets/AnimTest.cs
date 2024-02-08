using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] string triggerName;
    [SerializeField] string boolName;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger(triggerName);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool(boolName, true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool(boolName, false);
        }
    }
}
