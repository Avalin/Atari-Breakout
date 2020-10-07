using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePaddle : MonoBehaviour
{
    private Animator animator;

    public void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void StretchPaddle() 
    {
        StartCoroutine(StretchPaddleAfterAnimation());
    }

    public void SquishPaddle() 
    {
        StartCoroutine(SquishPaddleAfterAnimation());
    }

    IEnumerator StretchPaddleAfterAnimation() 
    {
        animator.Play("Stretch_Paddle");
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Squish_Paddle"));
        animator.enabled = false;
        transform.localScale = new Vector2(2,1);
    }

    IEnumerator SquishPaddleAfterAnimation()
    {
        animator.enabled = true;
        animator.Play("Squish_Paddle");
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("Squish_Paddle"));
        transform.localScale = new Vector2(1, 1);
    }
}
