using UnityEngine;

public class SpeedReducer : MonoBehaviour
{
    // Topun Rigidbody bileþeni
    private Rigidbody ballRigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        // Çarpýþan obje "Ball" tag'ine sahip mi kontrol et
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Topun Rigidbody bileþenini al
            ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Topun hýzýný yarýya düþür
                ballRigidbody.velocity = ballRigidbody.velocity * 0.5f;
                ballRigidbody.angularVelocity = ballRigidbody.angularVelocity * 0.5f;
            }
        }
    }
}
