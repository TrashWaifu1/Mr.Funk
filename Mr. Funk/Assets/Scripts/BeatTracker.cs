using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTracker : MonoBehaviour
{
    public float bpm = 100;
    public bool clap;
    public bool go;
    public GameObject thing;
    public float beatTicker;

    private float coolDown;
    private float miniCoolDown;
    private float lastTime;
    private bool clapbetween;

    void Update()
    {
        if (clap == true)
        {
            lastTime = beatTicker;
            beatTicker = 0;
        }

        if (coolDown <= 0)
        {
            miniCoolDown = 0.0001f;
            coolDown = 1;
        }

        if (miniCoolDown > 0)
        {
            miniCoolDown -= 1 * Time.deltaTime;
            clap = true;
            GameObject.Find("Player").GetComponent<PlayerController>().moved = false;

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else
            clap = false;

        if (clap)
        {
            thing.SetActive(true);
            
            if (go)
                clapbetween = true;
        }
        else
            thing.SetActive(false);

        if (beatTicker < (lastTime / 2 + lastTime / 4))
        {
            go = true; 
        }
            
        if (clapbetween && beatTicker > lastTime / 2.5f)
        {
            go = false;
            clapbetween = false;
        }
            
    }

    private void FixedUpdate()
    {
        if (coolDown > 0)
        {
            coolDown -= (bpm / 60) * Time.deltaTime;
            clap = false;
        }

        beatTicker += 0.0001f;
    }
}
