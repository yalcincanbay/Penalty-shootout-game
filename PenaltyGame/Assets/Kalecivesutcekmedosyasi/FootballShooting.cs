using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FootballShooting : MonoBehaviour
{
    public GameObject Ball;
    public GameObject ArrowPrefab; // Ok prefab'ý
    public Slider shotPowerBar; // Þut gücü barý
    public float maxKickForce = 1000f; // Maksimum kuvvet
    public float chargeRate = 200f;    // Kuvvet artýþ hýzý
    public float upwardForceMultiplier = 0.3f; // Yukarý doðru kuvvet çarpaný
    public Animator animator; // Animator referansý
    private Jumping jumpingScript;

    private float currentKickForce = 0f;
    private Rigidbody ballRigidbody;
    private GameObject arrowInstance;
    private GameSceneController gameController;
    private Coroutine resetCoroutine;

    void Start()
    {
        if (Ball != null)
        {
            ballRigidbody = Ball.GetComponent<Rigidbody>();
        }

        // Ok iþaretini oluþtur
        if (ArrowPrefab != null)
        {
            arrowInstance = Instantiate(ArrowPrefab, Ball.transform.position, Quaternion.identity);
            arrowInstance.SetActive(false); // Baþlangýçta görünmez
        }

        // Þut gücü barýný sýfýrla
        if (shotPowerBar != null)
        {
            shotPowerBar.minValue = 0f;
            shotPowerBar.maxValue = maxKickForce;
            shotPowerBar.value = 0f;
        }

        // Jumping script'ini bul
        jumpingScript = animator.GetBehaviour<Jumping>();

        // GameSceneController'ý bul
        gameController = FindObjectOfType<GameSceneController>();
    }

    void Update()
    {
        // Þut tuþuna basýlý tutulduðunda
        if (Input.GetKey(KeyCode.E))
        {
            // Basýlý tutma süresine göre kuvveti artýr
            currentKickForce += chargeRate * Time.deltaTime;
            currentKickForce = Mathf.Clamp(currentKickForce, 0f, maxKickForce);

            // Þut gücü barýný güncelle
            if (shotPowerBar != null)
            {
                shotPowerBar.value = currentKickForce;
            }

            // Ok iþaretini güncelle ve görünür yap
            UpdateArrow();
        }

        // Þut tuþunu býraktýðýnda
        if (Input.GetKeyUp(KeyCode.E))
        {
            KickBallTowardsMouse();

            // Ýþlem tamamlandý, deðiþkenleri sýfýrla
            currentKickForce = 0f;

            // Þut gücü barýný sýfýrla
            if (shotPowerBar != null)
            {
                shotPowerBar.value = 0f;
            }

            // Ok iþaretini gizle
            arrowInstance.SetActive(false);

            // Top hareket ettiðinde zamanlayýcý baþlat
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(CheckForAction());

            // Þut atýldýktan sonra sýrayý deðiþtir
            if (gameController != null)
            {
                gameController.SwitchTeam();
            }
        }
    }

    void KickBallTowardsMouse()
    {
        // Mouse pozisyonunu al ve dünya koordinatlarýna çevir
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y; // Kamera yüksekliði
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.y = Ball.transform.position.y; // Yüksekliði sabit tut

        // Yönü hesapla
        Vector3 direction = (targetPosition - Ball.transform.position).normalized;

        // Yukarý doðru kuvveti hesapla
        float upwardForce = currentKickForce * upwardForceMultiplier;

        // Yukarý doðru kuvvet ekle
        Vector3 force = direction * currentKickForce + Vector3.up * upwardForce;

        ballRigidbody.AddForce(force);

        // Topun yönünü Jumping script'ine bildir
        if (jumpingScript != null)
        {
            jumpingScript.DetectBallDirection(direction, animator);
        }
    }

    void UpdateArrow()
    {
        if (arrowInstance != null)
        {
            // Mouse pozisyonunu al ve dünya koordinatlarýna çevir
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.y; // Kamera yüksekliði
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPosition.y = Ball.transform.position.y; // Yüksekliði sabit tut

            // Yönü hesapla
            Vector3 direction = (targetPosition - Ball.transform.position).normalized;

            // Oku yönlendir ve pozisyonunu güncelle
            arrowInstance.transform.position = Ball.transform.position;
            arrowInstance.transform.rotation = Quaternion.LookRotation(direction);
            arrowInstance.SetActive(true);
        }
    }

    IEnumerator CheckForAction()
    {
        yield return new WaitForSeconds(5f); // 5 saniye bekle

        // Eðer 5 saniye içinde herhangi bir aksiyon olmadýysa topu dýþarý çýkmýþ gibi göster
        if (gameController != null)
        {
            gameController.MissedShot();
        }
    }

    public void ResetBallPosition()
    {
        if (gameController != null)
        {
            gameController.ResetGameObjects();
        }
    }
}
