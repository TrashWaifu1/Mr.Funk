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
    public MoveCache moveCache;
    public bool early = true;
    public float funkMeater;

    private Vector3 pos;
    public float millCounter;
    private bool wait;
    private bool perfect;
    private bool good;
    private bool fail;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool atDestination = true;

    void Start()
    {
        moveCache = new MoveCache();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(early);
        bool clap = beatTracker.GetComponent<BeatTracker>().clap;

        if (Input.GetKeyDown(KeyCode.W) && !moved && !wait)
        {
            moveCache.moveType = "vertical";
            moveCache.direction = 1.5f;
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && !moved && !wait)
        {
            moveCache.moveType = "vertical";
            moveCache.direction = -1.5f;
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && !moved && !wait)
        {
            moveCache.moveType = "horizontal";
            moveCache.direction = 1.5f;
            moved = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && !moved && !wait)
        {
            moveCache.moveType = "horizontal";
            moveCache.direction = -1.5f;
            moved = true;
        }

        //early to late
        if (clap && early)
        {
            Debug.Log("early to late");

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
            Debug.Log("late to early");
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
        
        if (moveCache.moveType == "vertical" && (transform.position.y > pos.y + 0.3f || transform.position.y < pos.y + -0.3f))
        {
            atDestination = true;
            transform.position = pos;
            velocity = Vector2.zero;
            rb.velocity = velocity;

            moveCache.direction = 0;
            moveCache.moveType = "";
        }

        if (moveCache.moveType == "horizontal" && (transform.position.x > pos.x + 0.3f || transform.position.x < pos.x + -0.3f))
        {
            atDestination = true;
            transform.position = pos;
            velocity = Vector2.zero;
            rb.velocity = velocity;

            moveCache.direction = 0;
            moveCache.moveType = "";
        }
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;

        if (moveCache.moveType == "vertical" && !atDestination)
        {
            velocity = new Vector2(0, moveCache.direction * speed);
        }
        else
        {
            velocity = new Vector2(moveCache.direction * speed, 0);
        }

        rb.velocity = velocity;
    }

    private void Move()
    {
        //vertical
        if (moveCache.moveType == "vertical")
        {
            pos.y =  moveCache.direction + transform.position.y;
            pos.x = transform.position.x;
        }
        //horizontal
        else
        {
            pos.x = moveCache.direction + transform.position.x;
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

    public class MoveCache
    {
        public string moveType;
        public float direction;
    }
}
