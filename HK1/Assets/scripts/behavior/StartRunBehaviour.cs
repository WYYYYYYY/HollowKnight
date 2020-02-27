using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRunBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).startRunAudio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)//起跑动画可以起跳，停止，跑动，掉落
    {
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

        if (Input.GetKeyDown(InputManager.getInstance().fireballKey))
        {
            animator.SetTrigger("fireballtrigger");
            animator.SetBool("isrun", false);
        }

        if (GetCharacterControl(animator).currentDir==CharacterControl.PlayerDir.Right)//向右起跑时
        {
            if(Input.GetKey(InputManager.getInstance().moveRightKey))//继续向右
            {
                if(Input.GetKeyDown(InputManager.getInstance().moveLeftKey))//如果再按下左键就停止
                {
                    animator.SetTrigger("stoptrigger");
                    animator.SetBool("isrun", false);
                }
                //没有按下左键就等待播放完毕进入running
            }
            if(Input.GetKeyUp(InputManager.getInstance().moveRightKey))//停止向右
            {
                animator.SetTrigger("stoptrigger");
                animator.SetBool("isrun", false);
            }
        }
        if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Left)
        {
            if (Input.GetKey(InputManager.getInstance().moveLeftKey))
            {
                if (Input.GetKeyDown(InputManager.getInstance().moveRightKey))
                {
                    animator.SetTrigger("stoptrigger");
                    animator.SetBool("isrun", false);
                }
            }
            if (Input.GetKeyUp(InputManager.getInstance().moveLeftKey))
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
        GetCharacterControl(animator).startRunAudio.Stop();
    }

}
