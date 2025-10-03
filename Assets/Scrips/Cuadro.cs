using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cuadro : MonoBehaviour
{
    Vector2 velocity;
    public GameObject circulo, bala2, bomba;
    public GameObject vida1, vida2, vida3;

    public Image iconoArma;           
    public Sprite spriteBalaNormal;
    public Sprite spriteBala2;
    public Sprite spriteBomba;
    int hits, tipo_disparo;
    private bool hasBala2;
    private bool hasBomba;          
    public SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;

        tipo_disparo = 1;
        hasBala2 = false;
        hasBomba = false;
        ActualizarIconoArma();

        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Llama a la función que cambia de arma
            CambiarArma();
        }
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
                sound.playDisparoNave();
            }
            if (tipo_disparo == 2)
            {
                Instantiate(bala2, transform.position, transform.rotation);
                sound.playDisparoNave();
            }
            if (tipo_disparo == 3)
            {
                Instantiate(bomba, transform.position, transform.rotation);
                sound.playBomba();
            }
            
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow))
        {
            velocity.x = 0;
        }
    }
    void CambiarArma()
    {

        if (tipo_disparo == 1)
        {
            if (hasBala2) tipo_disparo = 2;
            else if (hasBomba) tipo_disparo = 3;
        }
        else if (tipo_disparo == 2)
        {
            if (hasBomba) tipo_disparo = 3;
            else tipo_disparo = 1;
        }
        else if (tipo_disparo == 3)
        {
            tipo_disparo = 1;
        }
        sound.playCambioArma(); 
        ActualizarIconoArma();
    }

    void ActualizarIconoArma()
    {
        switch (tipo_disparo)
        {
            case 1:
                iconoArma.sprite = spriteBalaNormal;
                break;
            case 2:
                iconoArma.sprite = spriteBala2;
                break;
            case 3:
                iconoArma.sprite = spriteBomba;
                break;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "powerup")
        {
            tipo_disparo = 2;
            sound.playPowerup();

            Destroy(collision.gameObject);
            ActualizarIconoArma();
        }
        if (collision.gameObject.tag == "powerup2")
        {
            tipo_disparo = 3;
            sound.playPowerup();

            Destroy(collision.gameObject);
            ActualizarIconoArma();
        }
        if (collision.gameObject.tag == "powerup3")
        {
            bool vidaRecuperada = false;

            // 1. Busca si la vida 1 está inactiva
            if (!vida1.activeSelf) 
            {
                vida1.SetActive(true);
                vidaRecuperada = true;
            }
            // 2. Si no, busca si la vida 2 está inactiva
            else if (!vida2.activeSelf)
            {
                vida2.SetActive(true);
                vidaRecuperada = true;
            }
            // 3. Si no, busca si la vida 3 está inactiva
            else if (!vida3.activeSelf)
            {
                vida3.SetActive(true);
                vidaRecuperada = true;
            }

            // 4. Si se recuperó alguna vida, reproduce el sonido
            if (vidaRecuperada)
            {
                // Llama a tu SoundManager de la forma correcta
                sound.playPowerup();
            }

            // 5. Destruye el power-up siempre al final
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "asteroid"
            || collision.gameObject.tag == "enemybullet"
            || collision.gameObject.tag == "enemy"
            || collision.gameObject.tag == "enemy2")
        {
            tipo_disparo = 1;
            hasBala2 = false;
            hasBomba = false;
            ActualizarIconoArma();
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
