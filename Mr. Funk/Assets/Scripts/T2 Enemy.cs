using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public transform player;
    public GameObject projectile;

    private float timeBtwShots;
    public float startTimeBtwShots;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.findGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector2.Distance(transform.position, player.position)> stoppingDistance)
        {
            transform.position = Vector2.MoveTorwards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if(Vector2.Distance(transform.position, player.position)> stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }





    }


}
