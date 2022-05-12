using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 500;
    public float lifeTime = 30;

    private void FixedUpdate()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity = transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = velocity;

        lifeTime--;

        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().funkMeater -= 10;
        }
    }
}
