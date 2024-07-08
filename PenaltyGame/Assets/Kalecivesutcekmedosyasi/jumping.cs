using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : StateMachineBehaviour
{
    // Bu metod, state'e giri� yap�ld���nda �a�r�l�r
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Herhangi bir giri� i�lemi gerekiyorsa burada yapabilirsiniz
    }

    // Topun y�n�ne g�re z�plama t�r�n� belirle
    public void DetectBallDirection(Vector3 ballForce, Animator animator)
    {
        if (ballForce.x > 0)
        {
            // Top sa�a gidiyor, sa�a atla animasyonunu oynat
            int[] jumpTypes = { 1, 3, 4 };
            int randomIndex = Random.Range(0, jumpTypes.Length); // 0, 1 veya 2 rastgele say� �retir
            int randomJump = jumpTypes[randomIndex]; // 1, 3 veya 4 de�eri al�r
            animator.SetInteger("JumpType", randomJump); // Parametre t�r�n� kontrol edin
        }
        else if (ballForce.x < 0)
        {
            // Top sola gidiyor, sola atla animasyonunu oynat
            int[] jumpTypes = { 2, 0, 4 };
            int randomIndex = Random.Range(0, jumpTypes.Length); // 0, 1 veya 2 rastgele say� �retir
            int randomJump = jumpTypes[randomIndex]; // 2, 0 veya 4 de�eri al�r
            animator.SetInteger("JumpType", randomJump); // Parametre t�r�n� kontrol edin
        }
    }
}
