using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject player;
    public float speed = 500;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity = transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().health -= 0.5f;
            collision.gameObject.GetComponent<EnemyController>().damage = true;
        }
        else
            Destroy(gameObject);
    }
}
