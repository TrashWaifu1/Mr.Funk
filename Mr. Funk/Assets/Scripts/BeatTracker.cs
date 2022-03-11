using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTracker : MonoBehaviour
{
    public float bpm = 100;
    public bool clap;
    public bool go;
    public GameObject thing;
    public float millPerBeat;
    public float beatTicker;

    private float coolDown;
    private float miniCoolDown;
    private float lastTime;
    private bool clapbetween;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        millPerBeat = bpm / 60 * 1000;

        if (coolDown > 0)
        {
            coolDown -= (bpm / 60) * Time.deltaTime;
            clap = false;
        }
            
        if (coolDown <= 0)
        {
            miniCoolDown = 0.001f;
            coolDown = 1;
        }

        if (miniCoolDown > 0)
        {
            miniCoolDown -= 1 * Time.deltaTime;
            clap = true;
            GameObject.Find("Player").GetComponent<PlayerController>().moved = false;
            GameObject.Find("Player").GetComponent<PlayerController>().early = false;

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

        if (beatTicker < lastTime / 2 )
        {
            go = true; 
        }
            

        if (clapbetween && beatTicker > lastTime / 2)
        {
            go = false;
            clapbetween = false;
        }
            

        //((lastTime / 2) + (lastTime / 3))
        // beatTicker > lastTime / 2
    }

    private void FixedUpdate()
    {
        beatTicker += 0.0001f;

        if (clap == true)
        {
            lastTime = beatTicker;
            beatTicker = 0;
        }
    }
}
