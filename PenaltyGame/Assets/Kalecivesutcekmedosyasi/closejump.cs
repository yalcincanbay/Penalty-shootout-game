using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closejump : StateMachineBehaviour
{// Animator bileþeni
    private Animator animator;

    // Bu metod, state'e giriþ yapýldýðýnda çaðrýlýr
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Animator bileþenini al
        this.animator = animator;
    }

    // Bu metod, state'te güncelleme yapýldýðýnda çaðrýlýr
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // E tuþuna basýldýðýnda
        if (animator == true)
        {
            // Jumping state'ine geçiþ yap
            animator.SetBool("IsJumping", false);
        }
    }
}
