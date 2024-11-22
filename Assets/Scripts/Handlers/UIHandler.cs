using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    // Name of the Main Menu scene
    [SerializeField]
    private string mainMenuScene = "MainMenu";

    // Name of the Main Game scene
    [SerializeField]
    private string mainGameScene = "MainGameScene";

    [SerializeField]
    public TMP_InputField PlayerName;
    [SerializeField]
    public TMP_InputField ServerAddress;

    private void Start()
    {
        // Check if the current scene is not the Main Menu scene
        if (SceneManager.GetActiveScene().name != mainMenuScene)
        {
            LoadMainMenu();
        }
    }

    public void HostGame()
    {
        var playerName = PlayerName.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("Address", "none");
        LoadMainGame();
    }

    public void JoinGame()
    {
        var playerName = PlayerName.text;
        var address = ServerAddress.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("Address", address);
        LoadMainGame();
    }


    // Method to load the Main Menu scene
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    // Method to load the Main Game scene
    private void LoadMainGame()
    {
        SceneManager.LoadScene(mainGameScene);
    }
}
