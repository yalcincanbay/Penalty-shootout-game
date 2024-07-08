using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton; // Exit butonunu tan�mlay�n
    public TMP_Dropdown homeTeamDropdown;
    public TMP_Dropdown awayTeamDropdown;

    void Start()
    {
        // Play butonuna t�klama olay�n� ba�lay�n
        playButton.onClick.AddListener(StartGame);

        // Exit butonuna t�klama olay�n� ba�lay�n
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        // Se�ilen tak�mlar� al
        if (homeTeamDropdown == null || awayTeamDropdown == null)
        {
            Debug.LogError("Dropdown bile�enleri atanmad�!");
            return;
        }

        string homeTeam = homeTeamDropdown.options[homeTeamDropdown.value].text;
        string awayTeam = awayTeamDropdown.options[awayTeamDropdown.value].text;

        // Tak�m se�imlerini debug ile kontrol edebilirsiniz
        Debug.Log("Home Team: " + homeTeam);
        Debug.Log("Away Team: " + awayTeam);

        // Se�ilen tak�mlar� bir �ekilde oyuna aktarabiliriz
        // �rne�in, PlayerPrefs kullanarak
        PlayerPrefs.SetString("HomeTeam", homeTeam);
        PlayerPrefs.SetString("AwayTeam", awayTeam);

        // Oyunun as�l sahnesini y�kleyin
        SceneManager.LoadScene("GameScene"); // "GameScene" yerine do�ru sahne ad�n� yaz�n
    }

    void ExitGame()
    {
        Debug.Log("Oyun kapat�l�yor...");
        Application.Quit();
    }
}
