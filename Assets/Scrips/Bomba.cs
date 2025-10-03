using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    Vector2 velocity;

    void Start()
    {
        float var2 = PlayerPrefs.GetFloat("var2");
        float var3 = PlayerPrefs.GetFloat("var3");

        velocity.x = var2 * 10;
        velocity.y = var3 * 10;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += velocity * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemybullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
