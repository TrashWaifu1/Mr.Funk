using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTracker : MonoBehaviour
{
    public float bpm = 100;
    public bool clap;
    public GameObject thing;
   

    private float coolDown;
    private float miniCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        else
            thing.SetActive(false);
    }
}
