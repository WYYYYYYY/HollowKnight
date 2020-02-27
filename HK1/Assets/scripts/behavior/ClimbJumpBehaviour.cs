﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbJumpBehaviour : CharacterStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterControl(animator).wallSlideAudio.Stop();
        GetCharacterControl(animator).wallJumpAudio.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Left)
        {
            GetCharacterControl(animator).transform.Rotate(0, 180, 0);
            GetCharacterControl(animator).currentDir = CharacterControl.PlayerDir.Right;
        }
        else if (GetCharacterControl(animator).currentDir == CharacterControl.PlayerDir.Right)
        {
            GetCharacterControl(animator).transform.Rotate(0, -180, 0);
            GetCharacterControl(animator).currentDir = CharacterControl.PlayerDir.Left;
        }
        animator.ResetTrigger("climbjumptrigger");
    }

}
