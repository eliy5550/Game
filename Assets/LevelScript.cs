using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    [SerializeField] GameObject lcui;//level complete ui
    [SerializeField] GameObject next;//next level ui

    //Gameobjects of the bounderies
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject ceiling;
    [SerializeField] GameObject floor;

    [SerializeField] Camera cam;

    Vector3 leftWallPos, rightWallPos, ceilingPos,floorPos; //positions
    public static float leftborder, rightborder; //this is static for restricting player movement
    public static int score;

    void Start() {
        SetPositions();
        PlaceBounds();
        score = 0;
        if (lcui) { lcui.SetActive(false); }//disable the level complete UI
        
        UpdateScore();

        next = GameObject.Find("Next");
        if (next) { next.SetActive(false); }//disable the next level UI

    }

    private void FixedUpdate()
    {   // check how many balls are in the scene
        GameObject[] allballs = GameObject.FindGameObjectsWithTag("Ball");
        if (allballs.Length == 0){
            if (lcui) { lcui.SetActive(true); }
            if (next) { next.SetActive(true); }//enable the next level UI
        }//level complete if none

    }

    void SetPositions() {
        leftWallPos = cam.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, -6));
        rightWallPos = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, -6));
        ceilingPos = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 1-6));
        floorPos = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
        //SETTING DEPTH
        leftWallPos.z = rightWallPos.z = ceilingPos.z =  -6;
        floorPos.z = -7;
        //static vars
        leftborder = leftWallPos.x;
        rightborder = rightWallPos.x;
    }
    void PlaceBounds() {
        leftWall.transform.position = leftWallPos;
        rightWall.transform.position = rightWallPos;
        ceiling.transform.position = ceilingPos;
        floor.transform.position = floorPos;
    }

    //Methods for menu UI and one for returning to menu
    public void QuitGame() {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void UpdateScore() {
        GameObject scoreOB = GameObject.Find("Score");
        if (scoreOB)
        {
            Text scoreText = scoreOB.GetComponent<Text>();
            if (scoreText) { scoreText.text = score.ToString(); }
        }
    }

}
