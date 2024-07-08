using UnityEngine;

public class SpeedReducer : MonoBehaviour
{
    // Topun Rigidbody bile�eni
    private Rigidbody ballRigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        // �arp��an obje "Ball" tag'ine sahip mi kontrol et
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Topun Rigidbody bile�enini al
            ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Topun h�z�n� yar�ya d���r
                ballRigidbody.velocity = ballRigidbody.velocity * 0.5f;
                ballRigidbody.angularVelocity = ballRigidbody.angularVelocity * 0.5f;
            }
        }
    }
}
