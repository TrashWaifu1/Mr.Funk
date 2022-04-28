using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2Enemy : MonoBehaviour
{
    /*
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public GameObject projectile;

    private float timeBtwShots;
    public float startTimeBtwShots;
    */

    public GameObject player;
    public float speed;
    public MoveCache moveCache;
    public MoveCache emptyCache;

    private Vector3 pos;
    private Rigidbody2D rb;
    private bool atDestination = true;
    private Vector2 targetDir;
    private GameObject gameManager;
    private bool clap;

    // Start is called before the first frame update
    void Start()
    {
        /*
            player = GameObject.Find("Player").transform;
            timeBtwShots = startTimeBtwShots;
        */
        gameManager = GameObject.Find("GameManager");

        emptyCache = new MoveCache();
        emptyCache.moveType = "";
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        clap = gameManager.GetComponent<BeatTracker>().clap;

        if (clap)
        {
            targetDir.x = transform.position.x / player.transform.position.x;
            targetDir.y = transform.position.y / player.transform.position.y;

            if (Mathf.Abs(targetDir.x) > Mathf.Abs(targetDir.y))
            {
                if (targetDir.x == Mathf.Abs(targetDir.x))
                {
                    moveCache.moveType = "horizontal";
                    moveCache.direction = 1;
                    Move();
                }
                else
                {
                    moveCache.moveType = "horizontal";
                    moveCache.direction = -1;
                    Move();
                }
            }
            else if (Mathf.Abs(targetDir.x) < Mathf.Abs(targetDir.y))
            {
                if (targetDir.y == Mathf.Abs(targetDir.y))
                {
                    moveCache.moveType = "vertical";
                    moveCache.direction = 1;
                    Move();
                }
                else
                {
                    moveCache.moveType = "vertical";
                    moveCache.direction = -1;
                    Move();
                }
            }
            else
            {
                if (Random.Range(0, 1) == 1)
                {
                    if (targetDir.x == Mathf.Abs(targetDir.x))
                    {
                        moveCache.moveType = "horizontal";
                        moveCache.direction = 1;
                        Move();
                    }
                    else
                    {
                        moveCache.moveType = "horizontal";
                        moveCache.direction = -1;
                        Move();
                    }
                }
                else
                {
                    if (targetDir.y == Mathf.Abs(targetDir.y))
                    {
                        moveCache.moveType = "vertical";
                        moveCache.direction = 1;
                        Move();
                    }
                    else
                    {
                        moveCache.moveType = "vertical";
                        moveCache.direction = -1;
                        Move();
                    }
                }
            }
        }
        


        /*
        if(Vector2.Distance(transform.position, player.position)> stoppingDistance)
        {
            //transform.position = Vector2.MoveTorwards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if(Vector2.Distance(transform.position, player.position)> stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        */
    }

    private void FixedUpdate()
    {
        if (transform.position != pos)
            rb.AddForce((pos - transform.position) * speed);

        if (transform.position.x < pos.x + 0.3f && transform.position.x > pos.x - 0.3f && transform.position.y < pos.y + 0.3f && transform.position.y > pos.y - 0.3f)
        {
            transform.position = pos;
            rb.velocity = Vector3.zero;
        }
    }

    private void Move()
    {
        rb.velocity = Vector3.zero;

        //vertical
        if (moveCache.moveType == "vertical")
        {
            pos.y = moveCache.direction + transform.position.y;
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

    public class MoveCache
    {
        public string moveType = "";
        public float direction = 0;
    }
}
