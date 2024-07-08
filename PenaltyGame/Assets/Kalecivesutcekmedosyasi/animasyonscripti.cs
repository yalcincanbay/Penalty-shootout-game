using UnityEngine;

public class animasyonscripti : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator bile�enini al
        animator = GetComponent<Animator>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Jumping state'ine ge�i� yap
            // animator.SetBool("IsJumping", true);
            animator.Play("Jumping"); // "Jumping" durumunun do�ru ad� oldu�undan emin olun
        }
        // Animasyonu ba�lat
       

        // Animasyonun s�rekli tekrar etmesini sa�lamak i�in Animator ayarlar�n� yap�n
       // animator.SetBool("IsLooping", true); // Animator controller'da IsLooping parametresi olmal� ve animasyonun loop'u ayarlanmal�
    }
}
