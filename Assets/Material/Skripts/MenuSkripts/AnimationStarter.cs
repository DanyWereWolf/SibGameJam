using UnityEngine;

public class AnimationStarter : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.Play("StartGame");
    }
}
