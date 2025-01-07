using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutController : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // When called, starts the fadeout animation for the sprite attatched to this game object
    public void FadeOut()
    {
        if (mAnimator != null)
        {
            mAnimator.SetTrigger("TrFadeOut");
        }
    }
}
