using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).wallSlideAudio.Stop();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//idle状态可以起跑 起跳 转向 掉落
    {
        if (Input.GetKeyDown(InputManager.getInstance().fireballKey))
        {
            animator.SetTrigger("fireballtrigger");
        }
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
        }
        if (Input.GetKey(InputManager.getInstance().moveRightKey)&& Input.GetKey(InputManager.getInstance().moveLeftKey))
        {

        }else 
        {
            if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Right)//右方向时
            {
                if (Input.GetKeyDown(InputManager.getInstance().moveRightKey) || Input.GetKey(InputManager.getInstance().moveRightKey))//按右键
                {
                    animator.SetBool("isrun", true);
                }
                if (Input.GetKeyDown(InputManager.getInstance().moveLeftKey) || Input.GetKey(InputManager.getInstance().moveLeftKey))//不同转向
                {
                    animator.SetTrigger("rotatetrigger");
                }
            }
            if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Left)
            {
                if (Input.GetKeyDown(InputManager.getInstance().moveLeftKey) || Input.GetKey(InputManager.getInstance().moveLeftKey))
                {
                    animator.SetBool("isrun", true);
                }
                else if (Input.GetKeyDown(InputManager.getInstance().moveRightKey) || Input.GetKey(InputManager.getInstance().moveRightKey))
                {
                    animator.SetTrigger("rotatetrigger");
                }
            }
        }

        if (Input.GetKeyDown(InputManager.getInstance().jumpKey))//起跳
        {
            animator.SetBool("isjump", true);
        }
        if(!GetCharacterControl(animator).CheckIsGround())
        {
            if(GetCharacterControl(animator).move.y>0)
                animator.SetBool("isjump", true);
            else
                animator.SetBool("isfalling", true);
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
        animator.SetBool("isidle",false);
    }

}
