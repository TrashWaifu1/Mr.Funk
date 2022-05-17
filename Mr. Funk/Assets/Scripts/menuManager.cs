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

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
           // if (gameObject.GetComponent<>)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            SceneManager.LoadScene(3);
    }
}
