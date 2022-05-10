using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyII : MonoBehaviour
{
    
    public float speed;
    public MoveCache moveCache;
    public MoveCache emptyCache;

    public GameObject player;
    private Vector3 pos;
    private Rigidbody2D rb;
    private bool atDestination = true;
    public Vector2 targetDir;
    private GameObject gameManager;
    private bool clap;

    
    // Start is called before the first frame update
    void Start()
    {
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
        moveCache = emptyCache;

        targetDir.x = transform.position.x - player.transform.position.x;
        targetDir.y = transform.position.y - player.transform.position.y;


        if (clap)
        {
            // too far check
            if (Mathf.Abs(targetDir.x) < 6 && Mathf.Abs(targetDir.y) < 6)
            {
                // too close check
                if (Mathf.Abs(targetDir.x) > 1)
                {
                    if (Mathf.Abs(targetDir.y) > 1)
                    {
                        if (Mathf.Abs(targetDir.x) > Mathf.Abs(targetDir.y))
                        {
                            if (targetDir.x == Mathf.Abs(targetDir.x))
                            {
                                // right
                                moveCache.moveType = "horizontal";
                                moveCache.direction = -1;
                                Move();
                            }
                            else if (targetDir.x != Mathf.Abs(targetDir.x))
                            {
                                // left
                                moveCache.moveType = "horizontal";
                                moveCache.direction = 1;
                                Move();
                            }
                        }
                        else if (Mathf.Abs(targetDir.x) < Mathf.Abs(targetDir.y))
                        {
                            if (targetDir.y == Mathf.Abs(targetDir.y))
                            {
                                //up
                                moveCache.moveType = "vertical";
                                moveCache.direction = -1;
                                Move();
                            }
                            else if (targetDir.y != Mathf.Abs(targetDir.y))
                            {
                                //down
                                moveCache.moveType = "vertical";
                                moveCache.direction = 1;
                                Move();
                            }
                        }
                        else
                        {
                            if (Random.Range(0, 1) == 1)
                            {
                                if (targetDir.x == Mathf.Abs(targetDir.x))
                                {
                                    //right
                                    moveCache.moveType = "horizontal";
                                    moveCache.direction = -1;
                                    Move();
                                }
                                else if (targetDir.x != Mathf.Abs(targetDir.x))
                                {
                                    //left
                                    moveCache.moveType = "horizontal";
                                    moveCache.direction = 1;
                                    Move();
                                }
                            }
                            else
                            {
                                if (targetDir.y == Mathf.Abs(targetDir.y))
                                {
                                    //up
                                    moveCache.moveType = "vertical";
                                    moveCache.direction = -1;
                                    Move();
                                }
                                else if ((targetDir.y != Mathf.Abs(targetDir.y)))
                                {
                                    //down
                                    moveCache.moveType = "vertical";
                                    moveCache.direction = 1;
                                    Move();
                                }
                            }
                        }
                    }
                }
            }
        }
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
            if (Physics2D.Raycast(new Vector2(transform.position.x, moveCache.direction), Vector2.up, 0.1f) == false)
            {
                pos.y = moveCache.direction + transform.position.y;
                pos.x = transform.position.x;
            }
        }
        //horizontal
        else if (Physics2D.Raycast(new Vector2(moveCache.direction, transform.position.y), Vector2.up, 0.1f) == false)
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
