using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 velocity;

    float next_mov_time;
    float next_spawn_time;

    int par_impar;

    public GameObject enemy_bullet;

    int hits;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = -0.5f;

        next_mov_time = Time.time;
        next_spawn_time = Time.time + 2f;

        par_impar = 1;

        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn bullet
        if (Time.time > next_spawn_time)
        {
            Instantiate(enemy_bullet, transform.position, Quaternion.identity);

            next_spawn_time += Random.Range(0.5f, 1.5f);
        }

        //Movimiento Izq Der
        if (Time.time > next_mov_time)
        {
            if (par_impar % 2 == 0)
            {
                velocity.x = +5f;
            }
            else
            {
                velocity.x = -5f;
            }

            next_mov_time += 2f;
            par_impar++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            hits++;

            Destroy(collision.gameObject);

            if (hits == 5)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += velocity * Time.deltaTime;
    }
}
