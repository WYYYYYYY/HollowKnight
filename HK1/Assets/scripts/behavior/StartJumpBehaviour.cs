using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJumpBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//起跳可以停跳，着陆
    {
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
            animator.SetBool("isjump", false);
        }
        if (!GetCharacterControl(animator).isJump)
        {
            animator.SetTrigger("stopjumptrigger");
            animator.SetBool("isjump", false);
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
            animator.SetBool("isjump", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
