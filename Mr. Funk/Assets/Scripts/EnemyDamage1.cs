using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage1 : MonoBehaviour
{
    public GameObject projectile;
    public int Health = 10;
    public float offset = 210;

    private GameObject gameManager;
    private GameObject Player;
    private Rigidbody2D myRB;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
