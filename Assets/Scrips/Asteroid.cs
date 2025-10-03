using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector2 velocity, scale;
    float float_scale;
    int hits;

    public GameObject powerUpPrefab; 
    [Range(0, 1)] 
    public float powerUpDropChance = 0.1f;

    public Sprite asteroid2, asteroid3;

    SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = -1f;

        float_scale = Random.Range(0.1f, 0.8f);
        scale = new Vector2(float_scale, float_scale);

        transform.localScale = scale;
        hits = 0;

        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            hits++;
            Destroy(collision.gameObject);

            if (hits == 1)
            {
                GetComponent<SpriteRenderer>().sprite = asteroid2;
            }
            if (hits == 2)
            {
                GetComponent<SpriteRenderer>().sprite = asteroid3;
            }
            if (hits == 3)
            {
                sound.playExplosion();
                if (Random.value < powerUpDropChance)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }

                int var_score = PlayerPrefs.GetInt("score");
                int var_max = PlayerPrefs.GetInt("max");

                var_score += 10;

                if (var_score >= var_max)
                {
                    PlayerPrefs.SetInt("max", var_score);
                }

                PlayerPrefs.SetInt("score", var_score);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Bala3")
        {
            hits++;
            Destroy(collision.gameObject);
            if (hits == 1)
            {
                sound.playExplosion();
                if (Random.value < powerUpDropChance)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }

                int var_score = PlayerPrefs.GetInt("score");
                int var_max = PlayerPrefs.GetInt("max");

                var_score += 10;

                if (var_score >= var_max)
                {
                    PlayerPrefs.SetInt("max", var_score);
                }

                PlayerPrefs.SetInt("score", var_score);
                Destroy(gameObject);
            }
        }
    }
}
