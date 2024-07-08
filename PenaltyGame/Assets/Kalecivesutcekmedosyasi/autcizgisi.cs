using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autcizgisi : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("YOU MISSED !!");

            GameSceneController gameController = FindObjectOfType<GameSceneController>();
            if (gameController != null)
            {
                gameController.MissedShot();
            }
        }
    }
}
