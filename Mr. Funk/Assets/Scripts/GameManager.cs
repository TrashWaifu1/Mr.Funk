using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject paused;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
