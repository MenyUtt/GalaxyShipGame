using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Vector2 velocity;

    float next_mov_time;
    float next_spawn_time;

    int par_impar;

    SoundManager sound;

    public GameObject enemy_bullet;

    public GameObject powerUpPrefab; 
    [Range(0, 1)] 
    public float powerUpDropChance = 0.1f;
    

    int hits;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = -0.5f;

        next_mov_time = Time.time;
        next_spawn_time = Time.time + 2f;

        par_impar = 1;

        hits = 0;
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
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

        //Movimiento Izq Der Arri Abajo
        if (Time.time > next_mov_time)
        {
            if (par_impar % 2 == 0)
                {
                    // Mueve hacia arriba
                    velocity.y = +3f; 
                    velocity.x = 2f; 
                }
            else
            {
                // Mueve hacia abajo
                velocity.y = -3f; 
                velocity.x = 2f;
            }

            next_mov_time += 2f;
            par_impar++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Bala2")
        {
            hits++;

            Destroy(collision.gameObject);

            if (hits >= 5)
            {

                if (Random.value < powerUpDropChance)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }

                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
                Destroy(gameObject);
                sound.playExplosionA();

            }
        }
        if (collision.gameObject.tag == "Bala3")
        {
            hits++;

            Destroy(collision.gameObject);

            if (hits == 1)
            {

                if (Random.value < powerUpDropChance)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }

                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
                Destroy(gameObject);
                sound.playExplosionA();
                
            }
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += velocity * Time.deltaTime;
    }
}
