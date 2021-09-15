using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private Animator creditsAnimator;
    [SerializeField] private GameObject creditsGameObject;
    private bool creditsRunning;

    private void Update()
    {
        if (creditsRunning && !isPlaying(creditsAnimator, "Credits"))
        {
            creditsRunning = false;
            creditsGameObject.SetActive(false);
        }
    }
    public void StartCredits()
    {
        creditsRunning = true;
        creditsGameObject.SetActive(true);
        creditsAnimator.Play("Credits");
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
