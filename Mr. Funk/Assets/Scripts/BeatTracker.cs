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
            miniCoolDown = 0.07f;
            coolDown = 1;
        }

        if (miniCoolDown > 0)
        {
            miniCoolDown -= 1 * Time.deltaTime;
            clap = true;
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
