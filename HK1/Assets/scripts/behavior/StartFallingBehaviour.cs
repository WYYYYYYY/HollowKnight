using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFallingBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).wallSlideAudio.Stop();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
        }
        if (GetCharacterControl(animator).isground)
        {
            animator.SetBool("isfalling", false);
            animator.SetTrigger("landingtrigger");
        }
        if (Input.GetKeyDown(InputManager.getInstance().attackKey))
        {
            if (Input.GetKey(InputManager.getInstance().moveDownKey) && Input.GetKey(InputManager.getInstance().moveUpKey))
            {
                animator.SetTrigger("atklr1trigger");
            }
            else if (Input.GetKey(InputManager.getInstance().moveDownKey))
            {
                animator.SetTrigger("atkdowntrigger");
            }
            else if (Input.GetKey(InputManager.getInstance().moveUpKey))
            {
                animator.SetTrigger("atkuptrigger");
            }
            else
            {
                animator.SetTrigger("atklr1trigger");
            }
        }
        if (GetCharacterControl(animator).isClimb)
        {
            animator.SetBool("isclimb", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isfalling", false);
    }

}
