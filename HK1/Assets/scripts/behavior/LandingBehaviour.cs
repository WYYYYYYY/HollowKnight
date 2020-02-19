using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).landAudio.Play();
        animator.SetBool("isfalling", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//着陆可以起跳，可以idle，可以起跑，转向,falling
    {
        if(!GetCharacterControl(animator).CheckIsGround())
        {
            animator.ResetTrigger("landingtrigger");
            animator.SetBool("isfalling",true);
        }
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
        }
        if (Input.GetKeyDown(InputManager.getInstance().jumpKey))//起跳
        {
            animator.SetBool("isjump", true);
            animator.ResetTrigger("landingtrigger");
        }

        if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Right)//右方向时
        {
            if (Input.GetKeyDown(InputManager.getInstance().moveRightKey))//按右键
            {
                animator.SetBool("isrun", true);
                animator.ResetTrigger("landingtrigger");
            }
            else if (Input.GetKeyDown(InputManager.getInstance().moveLeftKey))//不同转向
            {
                animator.SetTrigger("rotatetrigger");
                animator.ResetTrigger("landingtrigger");
            }
        }
        if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Left)
        {
            if (Input.GetKeyDown(InputManager.getInstance().moveLeftKey))
            {
                animator.SetBool("isrun", true);
                animator.ResetTrigger("landingtrigger");
            }
            else if (Input.GetKeyDown(InputManager.getInstance().moveRightKey))
            {
                animator.SetTrigger("rotatetrigger");
                animator.ResetTrigger("landingtrigger");
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
            animator.ResetTrigger("landingtrigger");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("landingtrigger");
    }

}
