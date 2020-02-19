using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//停止动画可以起跑，idle，起跳，掉落，转向
    {
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
        }
        if (Input.GetKeyDown(InputManager.getInstance().jumpKey))//起跳
        {
            animator.SetBool("isjump", true);
            animator.ResetTrigger("stoptrigger");
        }

        if (!GetCharacterControl(animator).isground)//掉落
        {
            animator.SetBool("isfalling", true);
            animator.ResetTrigger("stoptrigger");
        }

        if(Input.GetKey(InputManager.getInstance().moveRightKey)&& Input.GetKey(InputManager.getInstance().moveLeftKey))//如果两键同时按
        {
            //什么也不做等待进入idle
        }
        else
        {
            if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Right)//右向时
            {
                if (Input.GetKeyDown(InputManager.getInstance().moveRightKey) || Input.GetKey(InputManager.getInstance().moveRightKey))//如果按右键,起跑
                {
                    animator.SetBool("isrun", true);
                    animator.ResetTrigger("stoptrigger");
                }
                if(Input.GetKeyDown(InputManager.getInstance().moveLeftKey)|| Input.GetKey(InputManager.getInstance().moveLeftKey))//如果按左键，转向
                {
                    animator.SetTrigger("rotatetrigger");
                    animator.ResetTrigger("stoptrigger");
                }
            }

            if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Left)
            {
                if (Input.GetKeyDown(InputManager.getInstance().moveLeftKey) || Input.GetKey(InputManager.getInstance().moveLeftKey))
                {
                    animator.SetBool("isrun", true);
                    animator.ResetTrigger("stoptrigger");
                }
                if (Input.GetKeyDown(InputManager.getInstance().moveRightKey) || Input.GetKey(InputManager.getInstance().moveRightKey))
                {
                    animator.SetTrigger("rotatetrigger");
                    animator.ResetTrigger("stoptrigger");
                }
            }
        }
        if (Input.GetKeyDown(InputManager.getInstance().attackKey))
        {
            if (Input.GetKey(InputManager.getInstance().moveDownKey) && Input.GetKey(InputManager.getInstance().moveUpKey))
            {
                animator.SetTrigger("atklr1trigger");
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
        animator.ResetTrigger("stoptrigger");
    }

}
