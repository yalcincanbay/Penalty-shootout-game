using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolCizgisi : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Gol oldu!");

            GameSceneController gameController = FindObjectOfType<GameSceneController>();
            if (gameController != null)
            {
                gameController.GoalScored();
            }
        }
    }
}
