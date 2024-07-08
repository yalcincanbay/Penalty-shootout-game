using UnityEngine;

public class animasyonscripti : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Jumping state'ine geçiþ yap
            // animator.SetBool("IsJumping", true);
            animator.Play("Jumping"); // "Jumping" durumunun doðru adý olduðundan emin olun
        }
        // Animasyonu baþlat
       

        // Animasyonun sürekli tekrar etmesini saðlamak için Animator ayarlarýný yapýn
       // animator.SetBool("IsLooping", true); // Animator controller'da IsLooping parametresi olmalý ve animasyonun loop'u ayarlanmalý
    }
}
