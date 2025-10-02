using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = -2.5f;

        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += velocity * Time.deltaTime;
    }
}
