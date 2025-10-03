using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cuadro : MonoBehaviour
{
    Vector2 velocity;
    public GameObject circulo, bala2, bomba;
    public GameObject vida1, vida2, vida3;

    int hits, tipo_disparo;

    public SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;

        tipo_disparo = 1;

        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            velocity.x = -2;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            velocity.x = +2;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.back);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float angulo = transform.rotation.eulerAngles.z;
            Debug.Log("angulo= " + angulo);

            if (angulo >= 0 && angulo <= 90)
            {
                float var1 = (angulo) * 100 / 90;
                float var2 = (var1 * 1) / 100;
                float var3 = 1 - var2;

                PlayerPrefs.SetFloat("var2", -var2);
                PlayerPrefs.SetFloat("var3", var3);
            }

            if (angulo >= 90 && angulo <= 180)
            {
                float var1 = (angulo - 90) * 100 / 90;
                float var2 = (var1 * 1) / 100;
                float var3 = 1 - var2;

                PlayerPrefs.SetFloat("var2", -var3);
                PlayerPrefs.SetFloat("var3", -var2);
            }

            if (angulo >= 180 && angulo <= 270)
            {
                float var1 = (angulo - 180) * 100 / 90;
                float var2 = (var1 * 1) / 100;
                float var3 = 1 - var2;

                PlayerPrefs.SetFloat("var2", var2);
                PlayerPrefs.SetFloat("var3", -var3);
            }

            if (angulo >= 270 && angulo <= 360)
            {
                float var1 = (angulo - 270) * 100 / 90;
                float var2 = (var1 * 1) / 100;
                float var3 = 1 - var2;

                PlayerPrefs.SetFloat("var2", var3);
                PlayerPrefs.SetFloat("var3", var2);
            }

            if (tipo_disparo == 1)
            {
                Instantiate(circulo, transform.position, transform.rotation);
            }
            if (tipo_disparo == 2)
            {
                Instantiate(bala2, transform.position, transform.rotation);
            }
            if (tipo_disparo == 3)
            {
                Instantiate(bomba, transform.position, transform.rotation);
            }

            sound.playDisparoNave();
            
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow))
        {
            velocity.x = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "powerup")
        {
            tipo_disparo = 2;
            sound.playPowerup();

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "powerup2")
        {
            tipo_disparo = 3;
            sound.playPowerup();

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "asteroid"
            || collision.gameObject.tag == "enemybullet"
            || collision.gameObject.tag == "enemy"
            || collision.gameObject.tag == "enemy2")
        {
            tipo_disparo = 1;

            hits++;

            Destroy(collision.gameObject);

            if (hits == 1)
            {
                vida3.SetActive(false);
            }
            if (hits == 2)
            {
                vida2.SetActive(false);
            }
            if (hits == 3)
            {
                vida1.SetActive(false);
            }
            if (hits == 4)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += velocity * Time.deltaTime;
    }
}
