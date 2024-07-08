using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : StateMachineBehaviour
{
    // Bu metod, state'e giriþ yapýldýðýnda çaðrýlýr
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Herhangi bir giriþ iþlemi gerekiyorsa burada yapabilirsiniz
    }

    // Topun yönüne göre zýplama türünü belirle
    public void DetectBallDirection(Vector3 ballForce, Animator animator)
    {
        if (ballForce.x > 0)
        {
            // Top saða gidiyor, saða atla animasyonunu oynat
            int[] jumpTypes = { 1, 3, 4 };
            int randomIndex = Random.Range(0, jumpTypes.Length); // 0, 1 veya 2 rastgele sayý üretir
            int randomJump = jumpTypes[randomIndex]; // 1, 3 veya 4 deðeri alýr
            animator.SetInteger("JumpType", randomJump); // Parametre türünü kontrol edin
        }
        else if (ballForce.x < 0)
        {
            // Top sola gidiyor, sola atla animasyonunu oynat
            int[] jumpTypes = { 2, 0, 4 };
            int randomIndex = Random.Range(0, jumpTypes.Length); // 0, 1 veya 2 rastgele sayý üretir
            int randomJump = jumpTypes[randomIndex]; // 2, 0 veya 4 deðeri alýr
            animator.SetInteger("JumpType", randomJump); // Parametre türünü kontrol edin
        }
    }
}
