using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject beatTracker;
    public bool moved;

    private Vector3 pos;
    void Start()
    {
      
    }

    void Update()
    {
        bool clap = beatTracker.GetComponent<BeatTracker>().clap;

        if (Input.GetKeyDown(KeyCode.W) && clap && moved)
        {
            pos.y = transform.position.y + 1.5f;
            pos.x = transform.position.x;

            gameObject.transform.position = pos;
            moved = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && clap && moved)
        {
            pos.y = transform.position.y - 1.5f;
            pos.x = transform.position.x;

            gameObject.transform.position = pos;
            moved = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && clap && moved)
        {
            pos.x = transform.position.x + 1.5f;
            pos.y = transform.position.y;

            gameObject.transform.position = pos;
            moved = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && clap && moved)
        {
            pos.x = transform.position.x - 1.5f;
            pos.y = transform.position.y;

            gameObject.transform.position = pos;
            moved = false;
        }
    }

    private void FixedUpdate()
    {
      
    }
}
