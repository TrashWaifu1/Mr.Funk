using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);

        if (Input.anyKey && SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(0);
    }
}
