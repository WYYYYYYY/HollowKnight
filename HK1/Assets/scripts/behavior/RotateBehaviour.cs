using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("rotate");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//转向动画可以起跳，起跑,停止
    {
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
        }
        if (Input.GetKeyDown(InputManager.getInstance().jumpKey))//起跳
        {
            animator.SetBool("isjump", true);
            animator.ResetTrigger("rotatetrigger");
        }
        
        if (Input.GetKey(InputManager.getInstance().moveRightKey) && Input.GetKey(InputManager.getInstance().moveLeftKey))
        {
            animator.SetTrigger("stoptrigger");
            animator.ResetTrigger("rotatetrigger");
        }

        if (!Input.GetKey(InputManager.getInstance().moveRightKey) && !Input.GetKey(InputManager.getInstance().moveLeftKey))
        {
            animator.SetTrigger("stoptrigger");
            animator.ResetTrigger("rotatetrigger");
        }

        if (Input.GetKeyDown(InputManager.getInstance().moveLeftKey)|| Input.GetKeyDown(InputManager.getInstance().moveRightKey)|| Input.GetKey(InputManager.getInstance().moveLeftKey) || Input.GetKey(InputManager.getInstance().moveRightKey))
        {
            animator.ResetTrigger("rotatetrigger");
            animator.SetBool("isrun", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
