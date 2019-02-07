using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Contains the Listeners for the Main Menu Buttons. When the selects a mode, his choice will be written on file and the game will start.
 * The game modes are saved as : 0 for mouse mode , 1 for keyboard mode , 2 for AI mode
 */
public class MainMenu : MonoBehaviour {

    public Button mouseButton;
    public Button keyboardButton;
    public Button AIButton;
    public Button QuitGame;
    
	void Start ()
    {
        mouseButton.onClick.AddListener(ClickedMouseButton);
        keyboardButton.onClick.AddListener(ClickedKeyboardButton);
        AIButton.onClick.AddListener(ClickedAIButton);
        QuitGame.onClick.AddListener(ClickedQuitGameButton);
    }

    void ClickedQuitGameButton()
    {
        Application.Quit();
    }

    void ClickedMouseButton()
    {
        StartGame(0);
    }

    void ClickedKeyboardButton()
    {

        StartGame(1);

    }

    void ClickedAIButton()
    {
        StartGame(2);
    }

   
    void StartGame(int selectedMode)
    {
        PlayerPrefs.SetInt("GameMode", selectedMode);
        SceneManager.LoadScene(1);
    }

   
}
