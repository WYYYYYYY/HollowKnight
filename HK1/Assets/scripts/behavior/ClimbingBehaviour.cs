using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!GetCharacterControl(animator).wallSlideAudio.isPlaying)
        {
            GetCharacterControl(animator).wallSlideAudio.PlayDelayed(0.1f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (GetCharacterControl(animator).CheckIsGround())
        {
            animator.Play("idle");
            animator.SetBool("isclimb", false);
            GetCharacterControl(animator).isClimb = false;
            GetCharacterControl(animator).climbCount = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
