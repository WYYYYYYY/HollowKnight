using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLRBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).attackAudio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetCharacterControl(animator).isground)
        {
            if (GetCharacterControl(animator).move.x == 0)
            {
                animator.SetTrigger("stoptrigger");
            }
            else
            {
                animator.SetBool("isrun", true);
            }
        }
        else
        {
            animator.SetBool("isfalling", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("atklr1trigger");
    }
}
