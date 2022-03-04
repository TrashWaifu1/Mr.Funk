using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject beatTracker;
    public bool moved;
    public MoveChash moveChash;
    public bool early;

    private Vector3 pos;
    public float millCounter;

    void Start()
    {
        moveChash = new MoveChash();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && moved)
        {
            moveChash.moveType = "vertical";
            moveChash.direction = 1;
            moved = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && moved)
        {
            moveChash.moveType = "vertical";
            moveChash.direction = -1;
            moved = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && moved)
        {
            moveChash.moveType = "horizontal";
            moveChash.direction = 1;
            moved = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && moved)
        {
            moveChash.moveType = "horizontal";
            moveChash.direction = -1;
            moved = false;
        }

        if (!moved && early)
        {
            millCounter += 0.001f * Time.deltaTime;
        }

        if (millCounter >= 0.075f && !early)
        {
            early = true;
        }
        else
        {
            millCounter += 0.001f * Time.deltaTime;
        }

        /*
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
        */
    }

    private void FixedUpdate()
    {
      
    }

    public class MoveChash
    {
        public string moveType;
        public int direction;
    }
}
