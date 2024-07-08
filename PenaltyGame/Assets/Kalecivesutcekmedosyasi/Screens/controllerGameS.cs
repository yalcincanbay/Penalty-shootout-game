using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public TMP_Text homeTeamText;
    public TMP_Text awayTeamText;
    public TMP_Text homeScoreText;
    public TMP_Text awayScoreText;
    public TMP_Text turnText;

    private int homeScore = 0;
    private int awayScore = 0;
    private int homeAttempts = 0;
    private int awayAttempts = 0;
    private int maxAttempts = 5;
    private bool isHomeTeamTurn = false; // Baþlangýç deðeri false olarak ayarlandý
    public GameObject Ball;
    private Vector3 initialBallPosition;
    public Animator animator;

    void Start()
    {
        string homeTeam = PlayerPrefs.GetString("HomeTeam", "Home Team");
        string awayTeam = PlayerPrefs.GetString("AwayTeam", "Away Team");

        homeTeamText.text = "Player 1: " + homeTeam;
        awayTeamText.text = "Player 2: " + awayTeam;

        UpdateScoreText();
        SwitchTeam(); // Ýlk turn away team ile baþlat
        UpdateTurnText();

        if (Ball != null)
        {
            initialBallPosition = Ball.transform.position;
        }
    }

    public void GoalScored()
    {
        if (isHomeTeamTurn)
        {
            homeScore++;
            homeAttempts++;
        }
        else
        {
            awayScore++;
            awayAttempts++;
        }

        UpdateScoreText();

        if (IsGameOver())
        {
            DisplayWinner();
            StartCoroutine(ReturnToMainMenu());
        }
        else
        {
            StartCoroutine(ResetAfterDelay());
        }
    }

    public void MissedShot()
    {
        if (isHomeTeamTurn)
        {
            homeAttempts++;
        }
        else
        {
            awayAttempts++;
        }

        if (IsGameOver())
        {
            DisplayWinner();
            StartCoroutine(ReturnToMainMenu());
        }
        else
        {
            StartCoroutine(ResetAfterDelay());
        }
    }

    public void SavedShot()
    {
        MissedShot();
    }

    void UpdateScoreText()
    {
        homeScoreText.text = "Home Score: " + homeScore;
        awayScoreText.text = "Away Score: " + awayScore;
    }

    void UpdateTurnText()
    {
        if (turnText != null)
        {
            string currentTurnTeam = isHomeTeamTurn ? "Home Team" : "Away Team";
            turnText.text = "Next Turn: " + currentTurnTeam;
        }
    }

    void DisplayWinner()
    {
        string winner;

        if (homeScore > awayScore)
        {
            winner = "Home Team Wins!";
        }
        else
        {
            winner = "Away Team Wins!";
        }

        turnText.text = winner;
    }

    bool IsGameOver()
    {
        return homeAttempts >= maxAttempts && awayAttempts >= maxAttempts;
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        ResetGameObjects();
    }

    public void ResetGameObjects()
    {
        if (Ball != null)
        {
            Rigidbody ballRigidbody = Ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.velocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
                ballRigidbody.Sleep();
                Ball.transform.position = initialBallPosition;
                ballRigidbody.WakeUp();
            }

            if (animator != null)
            {
                animator.SetBool("IsJumping", false);
                animator.Play("baslangic", 0, 0);
            }
        }
    }

    public void SwitchTeam()
    {
        isHomeTeamTurn = !isHomeTeamTurn;
        UpdateTurnText();
    }

    public bool IsHomeTeamTurn()
    {
        return isHomeTeamTurn;
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
