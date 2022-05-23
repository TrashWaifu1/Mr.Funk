using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text enemyCount;
    public GameObject paused;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject quitButton;
    public int enemyCounter = 0;
    public bool playerOnEnd = false;
    public int BossCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter = GameObject.FindObjectsOfType<EnemyController>().Length;

        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 4)
        {

            enemyCount.text = "Enemy Count: " + enemyCounter;


        }

        if (playerOnEnd == true && BossCounter == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ResumeButton()
    {
        //HidePauseUI();
        Debug.Log("Resume");
    }

    public void RestartButton()
    {
        Debug.Log("Restart");
    }

    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
