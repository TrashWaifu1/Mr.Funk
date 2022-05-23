using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage1 : MonoBehaviour
{
    public GameObject projectile;
    public float health = 10;
    public bool damage;
    public float offset = 210;
    public float colorTime;

    GameManager gm;
    private GameObject gameManager;
    private GameObject Player;
    private Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        myRB = GetComponent<Rigidbody2D>();
        gm = GetComponent<GameManager>();
        gm.enemyCounter = +1;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            gm.enemyCounter = -1;
        }

        if (colorTime > 0)
            colorTime -= 1 * Time.deltaTime;
        else if (damage)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            damage = false;
        }
            

        if (damage && colorTime < 0.001)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            colorTime = 0.1f;
        }

        myRB.rotation = Mathf.Atan2(transform.position.x - Player.transform.position.x, Player.transform.position.y - transform.position.y) * Mathf.Rad2Deg + offset;
        //transform.LookAt(Player.transform);

        if (gameManager.GetComponent<BeatTracker>().clap)
        {
            if (1 > transform.position.x - Player.transform.position.x && 1 > transform.position.y - Player.transform.position.y)
            {
                Instantiate(projectile, gameObject.transform);
            }
        }
    }
}
