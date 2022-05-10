using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage1 : MonoBehaviour
{
    public GameObject projectile;
    public GameObject Player;
    public int Health = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (1 > transform.position.x - Player.transform.position.x && 1 > transform.position.y - Player.transform.position.y)
        {
            Instantiate(projectile);
        }

    }
}
