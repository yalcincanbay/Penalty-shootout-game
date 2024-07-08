using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closejump : StateMachineBehaviour
{// Animator bile�eni
    private Animator animator;

    // Bu metod, state'e giri� yap�ld���nda �a�r�l�r
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Animator bile�enini al
        this.animator = animator;
    }

    // Bu metod, state'te g�ncelleme yap�ld���nda �a�r�l�r
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // E tu�una bas�ld���nda
        if (animator == true)
        {
            // Jumping state'ine ge�i� yap
            animator.SetBool("IsJumping", false);
        }
    }
}
