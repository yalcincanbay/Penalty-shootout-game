using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton; // Exit butonunu tanýmlayýn
    public TMP_Dropdown homeTeamDropdown;
    public TMP_Dropdown awayTeamDropdown;

    void Start()
    {
        // Play butonuna týklama olayýný baðlayýn
        playButton.onClick.AddListener(StartGame);

        // Exit butonuna týklama olayýný baðlayýn
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        // Seçilen takýmlarý al
        if (homeTeamDropdown == null || awayTeamDropdown == null)
        {
            Debug.LogError("Dropdown bileþenleri atanmadý!");
            return;
        }

        string homeTeam = homeTeamDropdown.options[homeTeamDropdown.value].text;
        string awayTeam = awayTeamDropdown.options[awayTeamDropdown.value].text;

        // Takým seçimlerini debug ile kontrol edebilirsiniz
        Debug.Log("Home Team: " + homeTeam);
        Debug.Log("Away Team: " + awayTeam);

        // Seçilen takýmlarý bir þekilde oyuna aktarabiliriz
        // Örneðin, PlayerPrefs kullanarak
        PlayerPrefs.SetString("HomeTeam", homeTeam);
        PlayerPrefs.SetString("AwayTeam", awayTeam);

        // Oyunun asýl sahnesini yükleyin
        SceneManager.LoadScene("GameScene"); // "GameScene" yerine doðru sahne adýný yazýn
    }

    void ExitGame()
    {
        Debug.Log("Oyun kapatýlýyor...");
        Application.Quit();
    }
}
