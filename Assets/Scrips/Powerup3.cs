using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup3 : MonoBehaviour
{
    public float speed = 2f; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
