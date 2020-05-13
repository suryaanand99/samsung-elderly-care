using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorInside : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            animator.SetTrigger("open door");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            animator.enabled = true;
        }
    }

    void pauseAnimationEvent()
    {
        animator.enabled = false;
    }
}
