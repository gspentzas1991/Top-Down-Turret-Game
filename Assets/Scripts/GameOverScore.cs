using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Shows you the score at the end of the game, and checks when the player presses the Escape key to return to the main menu
public class GameOverScore : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = "Final Score : " + PlayerPrefs.GetInt("FinalScore");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
