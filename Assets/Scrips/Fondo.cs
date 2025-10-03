using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    public GameObject asteroid, enemy, powerup, enemy2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAsteroids", 0, 1);

        InvokeRepeating("SpawnEnemy", 0, 3);

        InvokeRepeating("SpawnEnemy2", 0, 3);

        InvokeRepeating("SpawnPowerUp", 0, Random.Range(10, 20));

        if (!PlayerPrefs.HasKey("max"))
        {
            PlayerPrefs.SetInt("max", 0);
        }

        PlayerPrefs.SetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroids()
    {
        Vector2 position = new Vector2(Random.Range(-7f, 7f), 5f);
        Instantiate(asteroid, position, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        Vector2 position = new Vector2(Random.Range(-7f, 7f), 5f);
        Instantiate(enemy, position, Quaternion.identity);
    }
    void SpawnEnemy2()
    {
        Vector2 position = new Vector2(Random.Range(-7f, 7f), 5f);
        Instantiate(enemy2, position, Quaternion.identity);
    }

}
