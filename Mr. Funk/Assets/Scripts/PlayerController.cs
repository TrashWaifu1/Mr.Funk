using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public GameObject beatTracker;
    public float maxMillCount = 0.250f;
    public float pefectMillCount = 0.040f;
    public bool moved;
    public MoveCache emptyCache;
    public MoveCache moveCache;
    public float funkMeater;
    public float laserDamge = 0.5f;
    public GameObject bulletMass;

    private Vector3 pos;
    private Vector2 velocity;
    private Vector2 laserDir = Vector2.zero;
    private Rigidbody2D rb;
    private float punchCoolDown;
    readonly float maxFunk = 1000;
    readonly float failAmount = 50;
    readonly float drain = 30;
    private float laserTime;
    private float laserCoolDown;
    private bool atDestination = true;
    private bool powerUp;
    private float bulletLife;
    private GameObject bulletChash;

    void Start()
    {
        emptyCache = new MoveCache();
        emptyCache.moveType = "";
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;

        GetComponent<LineRenderer>().endWidth = 0;
        GetComponent<LineRenderer>().startWidth = 0;
    }

    void Update()
    {
        if (bulletLife > 0)
        {
            bulletLife -= 1 * Time.deltaTime;
        }
        else
        {
            Destroy(bulletChash);
        }

        moveCache = emptyCache;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (laserTime > 0)
            laserTime -= 1 * Time.deltaTime;

        if (laserCoolDown > 0)
            laserCoolDown -= 1 * Time.deltaTime;

        if (funkMeater != maxFunk)
        {
            funkMeater -= drain * Time.deltaTime;
        }
        else
            powerUp = true;

        if (punchCoolDown > 0)
            punchCoolDown -= 1 * Time.deltaTime;

        bool clap = beatTracker.GetComponent<BeatTracker>().go;

        #region Movement&FunckFail
        if (Input.GetKeyDown(KeyCode.W) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "vertical";
            moveCache.direction = 1;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.W) && !clap && !powerUp)
            funkMeater -= failAmount;

        else if (Input.GetKeyDown(KeyCode.S) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "vertical";
            moveCache.direction = -1;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.S) && !clap && !powerUp)
            funkMeater -= failAmount;

        else if (Input.GetKeyDown(KeyCode.D) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "horizontal";
            moveCache.direction = 1;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.D) && !clap && !powerUp)
            funkMeater -= failAmount;


        else if (Input.GetKeyDown(KeyCode.A) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "horizontal";
            moveCache.direction = -1;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.A) && !clap && !powerUp)
            funkMeater -= failAmount;

        funkMeater = Mathf.Clamp(funkMeater, 0, maxFunk);
        #endregion
        #region Lasers
        if (Input.GetKey(KeyCode.Keypad0))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // laser right
                if (laserDir == Vector2.zero)
                {
                    if (powerUp)
                    {
                        bulletChash = Instantiate(bulletMass, transform.transform);
                        bulletLife = 2;
                        powerUp = false;
                        funkMeater = 0;
                    }
                    else
                    {
                        laserDir = Vector2.right;
                        laserTime = 1;
                    }
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // laser left
                if (laserDir == Vector2.zero)
                {
                    if (powerUp)
                    {
                        bulletChash = Instantiate(bulletMass);
                        bulletLife = 2;
                        powerUp = false;
                        funkMeater = 0;
                    }
                    else
                    {
                        laserDir = Vector2.left;
                        laserTime = 1;
                    }
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                // laser down
                if (laserDir == Vector2.zero)
                {
                    if (powerUp)
                    {
                        bulletChash = Instantiate(bulletMass);
                        bulletLife = 2;
                        powerUp = false;
                        funkMeater = 0;
                    }
                    else
                    {
                        laserDir = Vector2.down;
                        laserTime = 1;
                    }
                }
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                // laser up
                if (laserDir == Vector2.zero)
                {
                    if (powerUp)
                    {
                        bulletChash = Instantiate(bulletMass);
                        bulletLife = 2;
                        powerUp = false;
                        funkMeater = 0;
                    }
                    else
                    {
                        laserDir = Vector2.up;
                        laserTime = 1;
                    }
                }
            }
            
        }
        #endregion
        #region Punch
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && punchCoolDown <= 0)
            {
                // punch right
                if (Physics2D.Raycast(transform.position, Vector2.right, 1))
                    if (Physics2D.Raycast(transform.position, Vector2.right, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.right, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyController>().health -= 5;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater = 0;
                        }
                        else
                        {
                            hit.GetComponent<EnemyController>().health--;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                        }
                        
                    }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && punchCoolDown <= 0)
            {
                // puch left
                if (Physics2D.Raycast(transform.position, Vector2.left, 1))
                    if (Physics2D.Raycast(transform.position, Vector2.left, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.left, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyController>().health -= 5;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater = 0;
                        }
                        else
                        {
                            hit.GetComponent<EnemyController>().health--;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                        }

                    }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && punchCoolDown <= 0)
            {
                // punch down
                if (Physics2D.Raycast(transform.position, Vector2.down, 1))
                    if (Physics2D.Raycast(transform.position, Vector2.down, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.down, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyController>().health -= 5;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater = 0;
                        }
                        else
                        {
                            hit.GetComponent<EnemyController>().health--;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                        }

                    }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && punchCoolDown <= 0)
            {
                // punch up
                if (Physics2D.Raycast(transform.position, Vector2.up, 1))
                    if (Physics2D.Raycast(transform.position, Vector2.up, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.up, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyController>().health -= 5;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater = 0;
                        }
                        else
                        {
                            hit.GetComponent<EnemyController>().health--;
                            hit.GetComponent<EnemyController>().damage = true;
                            punchCoolDown = 0.3f;
                        }

                    }
            }
        }
        #endregion
        #region LaserTicker
        if (laserDir != Vector2.zero)
        {
            if (laserTime > 0)
            {
                GetComponent<LineRenderer>().endWidth = 0.1f;
                GetComponent<LineRenderer>().startWidth = 0.1f;

                if (Physics2D.Raycast(transform.position, laserDir, 6))
                {
                    GameObject hit = Physics2D.Raycast(transform.position, laserDir, 6).transform.gameObject;
                    GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);

                    if (Physics2D.Raycast(transform.position, laserDir, 6).transform.tag == "enemy")
                    {
                        GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
                        hit.GetComponent<EnemyController>().health -= laserDamge * Time.deltaTime;
                        hit.GetComponent<EnemyController>().damage = true;
                    }
                }
                else
                {
                    GetComponent<LineRenderer>().SetPosition(1, transform.position + new Vector3(laserDir.x, laserDir.y, 0) * 6);
                }
                    
            }
            else if (laserCoolDown > 0)
            {
                laserCoolDown = 1;
            }

            if (laserTime <= 0 && laserCoolDown <= 0)
            {
                laserDir = Vector2.zero;
                GetComponent<LineRenderer>().endWidth = 0;
                GetComponent<LineRenderer>().startWidth = 0;
            }
                
        }
        #endregion
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

    private void Perfect()
    {
        funkMeater += 120; 
    }

    public class MoveCache
    {
        public string moveType = "";
        public float direction = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("test");
            collision.GetComponent<EnemyController>().health -= laserDamge;
            collision.GetComponent<EnemyController>().damage = true;
            laserDir = Vector2.zero;
            powerUp = false;
            funkMeater = 0;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            laserDir = Vector2.zero;
            powerUp = false;
            funkMeater = 0;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
