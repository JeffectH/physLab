using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGasAnimStateController : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DropGasAnimateController control = FindObjectOfType<DropGasAnimateController>();
        if (control != null) 
            control.setDropBlockState(false);

        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
