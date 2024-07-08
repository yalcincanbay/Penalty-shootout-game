using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FootballShooting : MonoBehaviour
{
    public GameObject Ball;
    public GameObject ArrowPrefab; // Ok prefab'�
    public Slider shotPowerBar; // �ut g�c� bar�
    public float maxKickForce = 1000f; // Maksimum kuvvet
    public float chargeRate = 200f;    // Kuvvet art�� h�z�
    public float upwardForceMultiplier = 0.3f; // Yukar� do�ru kuvvet �arpan�
    public Animator animator; // Animator referans�
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

        // Ok i�aretini olu�tur
        if (ArrowPrefab != null)
        {
            arrowInstance = Instantiate(ArrowPrefab, Ball.transform.position, Quaternion.identity);
            arrowInstance.SetActive(false); // Ba�lang��ta g�r�nmez
        }

        // �ut g�c� bar�n� s�f�rla
        if (shotPowerBar != null)
        {
            shotPowerBar.minValue = 0f;
            shotPowerBar.maxValue = maxKickForce;
            shotPowerBar.value = 0f;
        }

        // Jumping script'ini bul
        jumpingScript = animator.GetBehaviour<Jumping>();

        // GameSceneController'� bul
        gameController = FindObjectOfType<GameSceneController>();
    }

    void Update()
    {
        // �ut tu�una bas�l� tutuldu�unda
        if (Input.GetKey(KeyCode.E))
        {
            // Bas�l� tutma s�resine g�re kuvveti art�r
            currentKickForce += chargeRate * Time.deltaTime;
            currentKickForce = Mathf.Clamp(currentKickForce, 0f, maxKickForce);

            // �ut g�c� bar�n� g�ncelle
            if (shotPowerBar != null)
            {
                shotPowerBar.value = currentKickForce;
            }

            // Ok i�aretini g�ncelle ve g�r�n�r yap
            UpdateArrow();
        }

        // �ut tu�unu b�rakt���nda
        if (Input.GetKeyUp(KeyCode.E))
        {
            KickBallTowardsMouse();

            // ��lem tamamland�, de�i�kenleri s�f�rla
            currentKickForce = 0f;

            // �ut g�c� bar�n� s�f�rla
            if (shotPowerBar != null)
            {
                shotPowerBar.value = 0f;
            }

            // Ok i�aretini gizle
            arrowInstance.SetActive(false);

            // Top hareket etti�inde zamanlay�c� ba�lat
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(CheckForAction());

            // �ut at�ld�ktan sonra s�ray� de�i�tir
            if (gameController != null)
            {
                gameController.SwitchTeam();
            }
        }
    }

    void KickBallTowardsMouse()
    {
        // Mouse pozisyonunu al ve d�nya koordinatlar�na �evir
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y; // Kamera y�ksekli�i
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.y = Ball.transform.position.y; // Y�ksekli�i sabit tut

        // Y�n� hesapla
        Vector3 direction = (targetPosition - Ball.transform.position).normalized;

        // Yukar� do�ru kuvveti hesapla
        float upwardForce = currentKickForce * upwardForceMultiplier;

        // Yukar� do�ru kuvvet ekle
        Vector3 force = direction * currentKickForce + Vector3.up * upwardForce;

        ballRigidbody.AddForce(force);

        // Topun y�n�n� Jumping script'ine bildir
        if (jumpingScript != null)
        {
            jumpingScript.DetectBallDirection(direction, animator);
        }
    }

    void UpdateArrow()
    {
        if (arrowInstance != null)
        {
            // Mouse pozisyonunu al ve d�nya koordinatlar�na �evir
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.y; // Kamera y�ksekli�i
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPosition.y = Ball.transform.position.y; // Y�ksekli�i sabit tut

            // Y�n� hesapla
            Vector3 direction = (targetPosition - Ball.transform.position).normalized;

            // Oku y�nlendir ve pozisyonunu g�ncelle
            arrowInstance.transform.position = Ball.transform.position;
            arrowInstance.transform.rotation = Quaternion.LookRotation(direction);
            arrowInstance.SetActive(true);
        }
    }

    IEnumerator CheckForAction()
    {
        yield return new WaitForSeconds(5f); // 5 saniye bekle

        // E�er 5 saniye i�inde herhangi bir aksiyon olmad�ysa topu d��ar� ��km�� gibi g�ster
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
