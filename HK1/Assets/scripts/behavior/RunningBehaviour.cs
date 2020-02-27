using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).runAudio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//跑动动画可以起跳，停止，掉落
    {
        if (Input.GetKeyDown(InputManager.getInstance().fireballKey))
        {
            animator.SetTrigger("fireballtrigger");
            animator.SetBool("isrun", false);
        }
        if (GetCharacterControl(animator).isRush)
        {
            animator.SetTrigger("rushtrigger");
        }
        if (!GetCharacterControl(animator).isground)//掉落
        {
            animator.SetBool("isfalling", true);
            animator.SetBool("isrun", false);
        }

        if (Input.GetKeyDown(InputManager.getInstance().jumpKey))//起跳
        {
            animator.SetBool("isjump", true);
            animator.SetBool("isrun", false);
        }

        if(!Input.GetKey(InputManager.getInstance().moveRightKey)&&!Input.GetKey(InputManager.getInstance().moveLeftKey))
        {
            animator.SetTrigger("stoptrigger");
            animator.SetBool("isrun", false);
        }

        if(GetCharacterControl(animator).currentDir==CharacterControl.PlayerDir.Right)//如果当前方向为右
        {
            if(Input.GetKey(InputManager.getInstance().moveRightKey))//继续hold右键
            {
                //什么都不做
            }

            if(Input.GetKeyUp(InputManager.getInstance().moveRightKey)|| Input.GetKeyDown(InputManager.getInstance().moveLeftKey))//松开右键或者按下左键
            {
                animator.SetTrigger("stoptrigger");
                animator.SetBool("isrun", false);
            }
        }

        if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Left)
        {
            if (Input.GetKey(InputManager.getInstance().moveLeftKey))
            {
                //什么都不做
            }

            if (Input.GetKeyUp(InputManager.getInstance().moveLeftKey) || Input.GetKeyDown(InputManager.getInstance().moveRightKey))
            {
                animator.SetTrigger("stoptrigger");
                animator.SetBool("isrun", false);
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
            animator.SetBool("isrun", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).runAudio.Stop();
    }
}
