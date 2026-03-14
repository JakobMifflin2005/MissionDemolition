using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameMode
{
    idle,
    playing,
    levelEnd
}
public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; //a private singleton
    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitScore;
    public Vector3 castlePos; //A place to put the castles
    public GameObject[] castles; //An array of the castles
    public AudioSource snapSource;

    [Header("Dynamic")]
    public int score = 0;
    public int level; //The current level
    public int levelMax; //Number of levels
    public int shotsTaken;
    public GameObject gameOverPanel;
    public GameObject castle; //Current Castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam Mode

    void Start()
    {
        S = this; //Define the singleton
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        gameOverPanel.SetActive(false);
        StartLevel();
    }
    void StartLevel()
    {
        //Get rid of the old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }
        //Destroy old projectiles if they exist
        Projectile.DESTROY_PROJECTILES();
        //Instiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        //Reset the goal
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
        //Zoom out to show both
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }
    void UpdateGUI()
    {
        //Show the data in the GUI texts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
        uitScore.text = "Score: " + score;
    }
    void Update()
    {
        UpdateGUI();
        //Check for level end
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            //Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            FollowCam.SWITCH_VIEW(FollowCam.eView.both);
            //Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }
    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            ShowGameOver();
        }
        else
        {
            StartLevel();
        }
    }
    void ShowGameOver()
    {
        mode = GameMode.levelEnd;
        gameOverPanel.SetActive(true); // Show the UI
    }
    public void ReloadGame()
    {

        level = 0;
        shotsTaken = 0;
        gameOverPanel.SetActive(false);
        StartLevel();

    }
    //Static Method that allows code anywhere to increment shots taken
    static public void SHOT_FIRED()
    {
        S.shotsTaken++;
        if (S.snapSource != null)
        {
            S.snapSource.Play();
        }
    }
    //Static method that allows code anywhere to get a reference to S.castle
    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }
    static public void ADD_SCORE(int points)
    {
        S.score += points;
    }
}
