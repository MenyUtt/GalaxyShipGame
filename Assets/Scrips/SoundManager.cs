using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource disparo_nave;
    public AudioSource explosion;
    public AudioSource Powerup;

    public AudioSource explosionarcade;

    public AudioSource bomba;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playDisparoNave()
    {
        disparo_nave.Play();
    }

    public void playExplosion()
    {
        explosion.Play();
    }
    public void playPowerup()
    {
        Powerup.Play();
    }
    public void playExplosionA()
    {
        explosionarcade.Play();
    }
    public void playBomba()
    {
        bomba.Play();
    }
}
