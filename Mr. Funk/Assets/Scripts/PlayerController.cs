using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public GameObject beatTracker;
    public bool moved;
    public MoveCache emptyCache;
    public MoveCache moveCache;
    public float funkMeater = 500;
    public float laserDamge = 0.5f;
    public GameObject bulletMass;
    public RectTransform imageMask;
    public GameObject circleLaserFX;

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
    private bool upCheck;
    private bool downCheck;
    private bool rightCheck;
    private bool leftCheck;
    private bool reZero = true;
    GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        emptyCache = new MoveCache();
        emptyCache.moveType = "";
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;

        GetComponent<LineRenderer>().endWidth = 0;
        GetComponent<LineRenderer>().startWidth = 0;
    }

    void Update()
    {
        if (funkMeater <= 0)
            SceneManager.LoadScene(2);

        if (moved)
            reZero = false;

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            reZero = true;

        upCheck = !Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.up, 0.2f);
        downCheck = !Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.down, 0.2f);
        rightCheck = !Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.right, 0.2f);
        leftCheck = !Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), Vector2.left, 0.2f);

        imageMask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(funkMeater / 4.5f, 0, 230));
        imageMask.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(funkMeater / 4.5f, 0, 230));

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
        if ((Input.GetAxisRaw("Vertical") == 1) && !moved && clap && upCheck && reZero)
        {
            moveCache.moveType = "vertical";
            moveCache.direction = 1;
            Move();
            moved = true;
            Perfect();
        }

        //if ((Input.GetAxisRaw("Vertical") == 1) && !clap && !powerUp)
            //funkMeater -= failAmount;

        else if ((Input.GetAxisRaw("Vertical") == -1) && !moved && clap && downCheck && reZero)
        {
            moveCache.moveType = "vertical";
            moveCache.direction = -1;
            Move();
            moved = true;
            Perfect();
        }

        //if ((Input.GetAxisRaw("Vertical") == -1) && !clap && !powerUp)
            //funkMeater -= failAmount;

        else if ((Input.GetAxisRaw("Horizontal") == 1) && !moved && clap && rightCheck && reZero)
        {
            moveCache.moveType = "horizontal";
            moveCache.direction = 1;
            Move();
            moved = true;
            Perfect();
        }

        //if ((Input.GetAxisRaw("Horizontal") == 1) && !clap && !powerUp)
            //funkMeater -= failAmount;


        else if ((Input.GetAxisRaw("Horizontal") == -1) && !moved && clap && leftCheck && reZero)
        {
            moveCache.moveType = "horizontal";
            moveCache.direction = -1;
            Move();
            moved = true;
            Perfect();
        }

        //if ((Input.GetAxisRaw("Horizontal") == -1) && !clap && !powerUp)
            //funkMeater -= failAmount;

        funkMeater = Mathf.Clamp(funkMeater, 0, maxFunk);
        #endregion
        #region Special attack
        if (Input.GetKey(KeyCode.Keypad0))
        {
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetButton("B"))
            {
                // punch right
                if (Physics2D.Raycast(transform.position, Vector2.right, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.right, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.right, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyDamage1>().health -= 5;
                            hit.GetComponent<EnemyDamage1>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater -= 300;
                        }
                    }
                }
                else
                {
                    // laser right
                    if (laserDir == Vector2.zero)
                    {
                        if (powerUp)
                        {
                            GetComponent<CircleCollider2D>().enabled = true;
                            laserDir = new Vector2(1, 1);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetButton("X"))
            {
                // puch left
                if (Physics2D.Raycast(transform.position, Vector2.left, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.left, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.left, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyDamage1>().health -= 5;
                            hit.GetComponent<EnemyDamage1>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater -= 300;
                        }
                    }
                }
                else
                {
                    // laser left
                    if (laserDir == Vector2.zero)
                    {
                        if (powerUp)
                        {
                            GetComponent<CircleCollider2D>().enabled = true;
                            laserDir = new Vector2(1, 1);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetButton("A"))
            {
                // punch down
                if (Physics2D.Raycast(transform.position, Vector2.down, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.down, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.down, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyDamage1>().health -= 5;
                            hit.GetComponent<EnemyDamage1>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater -= 300;
                        }
                    }
                }
                else
                {
                    // laser down
                    if (laserDir == Vector2.zero)
                    {
                        if (powerUp)
                        {
                            GetComponent<CircleCollider2D>().enabled = true;
                            laserDir = new Vector2(1, 1);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetButton("Y"))
            {
                // punch up
                if (Physics2D.Raycast(transform.position, Vector2.up, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.up, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.up, 1).transform.gameObject;

                        if (powerUp)
                        {
                            hit.GetComponent<EnemyDamage1>().health -= 5;
                            hit.GetComponent<EnemyDamage1>().damage = true;
                            punchCoolDown = 0.3f;
                            powerUp = false;
                            funkMeater -= 300;
                        }
                    }
                }
                else
                {
                    // laser up
                    if (laserDir == Vector2.zero)
                    {
                        if (powerUp)
                        {
                            GetComponent<CircleCollider2D>().enabled = true;
                            laserDir = new Vector2(1, 1);
                        }
                    }
                }
            }
            
        }
        #endregion
        #region Normal attack
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetButton("B") && punchCoolDown <= 0)
            {
                // punch right
                if (Physics2D.Raycast(transform.position, Vector2.right, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.right, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.right, 1).transform.gameObject;
                        hit.GetComponent<EnemyDamage1>().health--;
                        hit.GetComponent<EnemyDamage1>().damage = true;
                        punchCoolDown = 0.3f;
                    }
                }
                else
                {
                    // laser right
                    if (laserDir == Vector2.zero)
                    {
                        laserDir = Vector2.right;
                        laserTime = 1;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetButton("X") && punchCoolDown <= 0)
            {
                // puch left
                if (Physics2D.Raycast(transform.position, Vector2.left, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.left, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.left, 1).transform.gameObject;
                        hit.GetComponent<EnemyDamage1>().health--;
                        hit.GetComponent<EnemyDamage1>().damage = true;
                        punchCoolDown = 0.3f;
                    }
                }
                else
                {
                    // laser left
                    if (laserDir == Vector2.zero)
                    {
                        laserDir = Vector2.left;
                        laserTime = 1;
                    }
                }    
            }

            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetButton("A") && punchCoolDown <= 0)
            {
                // punch down
                if (Physics2D.Raycast(transform.position, Vector2.down, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.down, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.down, 1).transform.gameObject;
                        hit.GetComponent<EnemyDamage1>().health--;
                        hit.GetComponent<EnemyDamage1>().damage = true;
                        punchCoolDown = 0.3f;
                    }
                }
                else
                {
                    // laser down
                    if (laserDir == Vector2.zero)
                    {
                        laserDir = Vector2.down;
                        laserTime = 1;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetButton("Y") && punchCoolDown <= 0)
            {
                // punch up
                if (Physics2D.Raycast(transform.position, Vector2.up, 1))
                {
                    if (Physics2D.Raycast(transform.position, Vector2.up, 1).transform.tag == "enemy")
                    {
                        GameObject hit = Physics2D.Raycast(transform.position, Vector2.up, 1).transform.gameObject;
                        hit.GetComponent<EnemyDamage1>().health--;
                        hit.GetComponent<EnemyDamage1>().damage = true;
                        punchCoolDown = 0.3f;
                    }
                }
                else
                {
                    // laser up
                    if (laserDir == Vector2.zero)
                    {
                        laserDir = Vector2.up;
                        laserTime = 1;
                    }
                }   
            }
        }
        #endregion
        #region LaserTicker
        if (laserDir != Vector2.zero && laserDir != new Vector2(1, 1))
        {
            if (laserTime > 0)
            {
                GetComponent<LineRenderer>().endWidth = 0.5f;
                GetComponent<LineRenderer>().startWidth = 0.5f;

                if (Physics2D.Raycast(transform.position, laserDir, 6))
                {
                    Vector2 hitPoint = Physics2D.Raycast(transform.position, laserDir, 6).point;
                    GameObject hit = Physics2D.Raycast(transform.position, laserDir, 6).transform.gameObject;
                    GetComponent<LineRenderer>().SetPosition(1, hitPoint);

                    if (Physics2D.Raycast(transform.position, laserDir, 6).transform.tag == "enemy")
                    {
                        GetComponent<LineRenderer>().SetPosition(1, hitPoint);
                        hit.GetComponent<EnemyDamage1>().health -= laserDamge * Time.deltaTime;
                        hit.GetComponent<EnemyDamage1>().damage = true;
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

        if (laserDir == new Vector2(1, 1))
        {
            GetComponent<CircleCollider2D>().radius += 8 * Time.deltaTime;
            circleLaserFX.transform.localScale += new Vector3(16 * Time.deltaTime, 16 * Time.deltaTime);
        }

        if (GetComponent<CircleCollider2D>().radius >= 4)
        {
            laserDir = Vector2.zero;
            powerUp = false;
            funkMeater -= 300;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().radius = 0;
            circleLaserFX.transform.localScale = new Vector3(0, 0, 1);
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
            collision.GetComponent<EnemyController>().health -= laserDamge;
            collision.GetComponent<EnemyController>().damage = true;
        }

        if (collision.gameObject.CompareTag("end"))
        {
            gm.playerOnEnd = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("end"))
        {
            gm.playerOnEnd = false;
        }
    }
}
