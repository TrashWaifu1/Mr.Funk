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

    private Vector3 pos;
    public float millCounter;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool atDestination = true;
    private bool fail;
    private bool powerUp;

    void Start()
    {
        emptyCache = new MoveCache();
        emptyCache.moveType = "";
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    void Update()
    {
        moveCache = emptyCache;

        if (funkMeater != 1000)
        {
            funkMeater -= 100 * Time.deltaTime;
            powerUp = true;
        }

            

        bool clap = beatTracker.GetComponent<BeatTracker>().go;


        if (Input.GetKeyDown(KeyCode.W) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "vertical";
            moveCache.direction = 1.5f;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.W) && !clap && !powerUp)
            funkMeater -= 100;

        else if (Input.GetKeyDown(KeyCode.S) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "vertical";
            moveCache.direction = -1.5f;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.S) && !clap && !powerUp)
            funkMeater -= 100;

        else if (Input.GetKeyDown(KeyCode.D) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "horizontal";
            moveCache.direction = 1.5f;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.D) && !clap && !powerUp)
            funkMeater -= 100;


        else if (Input.GetKeyDown(KeyCode.A) && !moved && clap)
        {
            rb.velocity = Vector3.zero;
            moveCache.moveType = "horizontal";
            moveCache.direction = -1.5f;
            Move();
            moved = true;
            Perfect();
        }

        if (Input.GetKey(KeyCode.A) && !clap && !powerUp)
            funkMeater -= 100;
            

        funkMeater = Mathf.Clamp(funkMeater, 0, 1000);
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
}
