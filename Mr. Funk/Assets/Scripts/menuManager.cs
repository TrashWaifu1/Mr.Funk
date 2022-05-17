using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public float time = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);

        if (Input.anyKey && SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(0);

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            time -= 1 * Time.deltaTime;

            if (time <= 0)
                SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            SceneManager.LoadScene(3);
    }
}
