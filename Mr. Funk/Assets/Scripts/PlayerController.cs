using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject beatTracker;
    public float maxMillCount = 0.250f;
    public float pefectMillCount = 0.040f;
    public bool moved;
    public MoveChash moveChash;
    public bool early;
    public float funkMeater;

    private Vector3 pos;
    private float millCounter;
    private bool wait;
    private bool perfect;
    private bool good;
    private bool fail;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool atDestination = true;

    void Start()
    {
        moveChash = new MoveChash();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool clap = beatTracker.GetComponent<BeatTracker>().clap;

        if (Input.GetKeyDown(KeyCode.W) && !moved && !wait)
        {
            moveChash.moveType = "vertical";
            moveChash.direction = 1.5f;
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && !moved && !wait)
        {
            moveChash.moveType = "vertical";
            moveChash.direction = -1.5f;
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && !moved && !wait)
        {
            moveChash.moveType = "horizontal";
            moveChash.direction = 1.5f;
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && !moved && !wait)
        {
            moveChash.moveType = "horizontal";
            moveChash.direction = -1.5f;
            moved = true;
        }

        //early to late
        if (clap && early)
        {
            if (perfect && !fail)
            {
                Perfect();
                Move();
            }
            else if (!fail)
            {
                Good();
                Move();
            }

            early = false;
            millCounter = 0;
            wait = false;
            perfect = false;
            good = false;
            fail = false;
        }

        //early add
        if (moved && early && wait)
        {
            millCounter += 0.001f * Time.deltaTime;

            //max early
            if (millCounter > maxMillCount)
            {
                wait = true;
                fail = true;
            }
            else if ( millCounter <= pefectMillCount)
            {
                perfect = true;
                wait = true;
            }
            else
                wait = true;
        }
        //late to early
        else if (millCounter > maxMillCount && !early)
        {
            early = true;
            millCounter = 0;
            wait = false;
        }
        //late add
        else if (wait)
        {
            millCounter += 0.001f * Time.deltaTime;
            //perfect tap
            if (moved && millCounter <= pefectMillCount)
            {
                Perfect();
                Move();
                wait = true;
            } 
            //good tap
            else if (moved)
            {
                Good();
                Move();
                wait = true;
            }
        }
        
        
        if (moveChash.moveType == "vertical" && transform.position.y > pos.y + 0.3f || transform.position.y < pos.y + -0.3f)
        {
            atDestination = true;
            transform.position = pos;
            velocity = Vector2.zero;
            rb.velocity = velocity;
        }

        if (moveChash.moveType == "horizontal" && transform.position.x > pos.x + 0.3f || transform.position.x < pos.x + -0.3f)
        {
            atDestination = true;
            transform.position = pos;
            velocity = Vector2.zero;
            rb.velocity = velocity;
        }
        

    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;

        if (moveChash.moveType == "vertical" && !atDestination)
        {
            velocity = new Vector2(0, moveChash.direction * speed);
        }
        else
        {
            velocity = new Vector2(moveChash.direction * speed, 0);
        }

        rb.velocity = velocity;
    }

    private void Move()
    {
        //vertical
        if (moveChash.moveType == "vertical")
        {
            pos.y =  moveChash.direction + transform.position.y;
            pos.x = transform.position.x;
        }
        //horizontal
        else
        {
            pos.x = moveChash.direction + transform.position.x;
            pos.y = transform.position.y;
        }

        atDestination = false;
    }

    private void Good()
    {
        funkMeater += 100;
    }

    private void Perfect()
    {
        if (early)
        {
            funkMeater += 300;
        }
            
    }

    public class MoveChash
    {
        public string moveType;
        public float direction;
    }
}
