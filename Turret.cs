using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Changes the turret heads when the player clicks the appropriate key and sets the UI text regarding the turret
 * Also holds the score and health variables. When the turret's heath reaches 0, the game saves your score and loads the Game Over scene.
 */ 
public class Turret : MonoBehaviour {

    public Text scoreUI;
    public Text healthUI;
    public Text modeUI;
    string[] modeUIDescriptions= { "Turret Mode : Single Shot", "Turret Mode : Shotgun", "Turret Mode : Poison", "Turret Mode : Stun" };
    public int turretHealth;
    int score;
    public GameObject turretHeadRifle;
    public GameObject turretHeadShotgun;
    public GameObject turretHeadPoison;
    public GameObject turretHeadStun;
    GameObject currentTurretHead;
    public Transform turretHeadTransform;
    
	void Start ()
    {
        currentTurretHead = Instantiate(turretHeadRifle, turretHeadTransform.position, transform.rotation);
        modeUI.text = modeUIDescriptions[0];

    }
	
	void Update ()
    {
        scoreUI.text = "Score: " + score;
        healthUI.text = "Health: " + turretHealth;
		if (Input.GetKeyDown("1"))
        {
            ChangeTurretHead(turretHeadRifle);
            modeUI.text = modeUIDescriptions[0];
        }
        if (Input.GetKeyDown("2"))
        {
            ChangeTurretHead(turretHeadShotgun);
            modeUI.text = modeUIDescriptions[1];
        }
        if (Input.GetKeyDown("3"))
        {
            ChangeTurretHead(turretHeadPoison);
            modeUI.text = modeUIDescriptions[2];
        }
        if (Input.GetKeyDown("4"))
        {
            ChangeTurretHead(turretHeadStun);
            modeUI.text = modeUIDescriptions[3];
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }

    //Destroys the previous turretHead and replaces it with a new one. It keeps the rotation of the previous turretHead
    void ChangeTurretHead(GameObject newTurretHead)
    {
        Quaternion currentTurretRotation = currentTurretHead.transform.rotation;
        Destroy(currentTurretHead);
        currentTurretHead=Instantiate(newTurretHead, turretHeadTransform.position, currentTurretRotation);
    }

    //Decreases the turret's health when it gets hit, and loads the game over scene when the health reaches 0
    public void GotHit(int damage)
    {
        turretHealth -= damage;
        if(turretHealth==0)
        {
            PlayerPrefs.SetInt("FinalScore", score);
            SceneManager.LoadScene(2);
        }
    }

    //This method is called by a Skeleton when it dies
    public void GainScore()
    {
        score++;
    }
}
