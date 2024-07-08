using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carpisma : MonoBehaviour
{
    public float forceAmount = 1000f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                Vector3 forceDirection = collision.transform.position - transform.position;
                forceDirection.Normalize();
                ballRigidbody.AddForce(forceDirection * forceAmount);

                Debug.Log("Kaleci kurtardý amazing save !");

                GameSceneController gameController = FindObjectOfType<GameSceneController>();
                if (gameController != null)
                {
                    gameController.SavedShot();
                }
            }
        }
    }
}
