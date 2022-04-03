using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BeatTracker : MonoBehaviour
{
    public float bpm = 100;
    public bool clap;
    public bool go;
    public float beatTicker;
    public GameObject funkVol;
    public GameObject labVol;
    public float fxSpeed = 0.03f;

    private float coolDown;
    private float miniCoolDown;
    private float lastTime;
    private bool clapbetween;
    private float fxCoolDown;
    private bool fxOnce;
    private ChromaticAberration chromAber;
    private LensDistortion lensDistor;
    private ColorGrading hue;

    private void Start()
    {
        funkVol.GetComponent<PostProcessVolume>().profile.TryGetSettings(out chromAber);
        funkVol.GetComponent<PostProcessVolume>().profile.TryGetSettings(out hue);

        labVol.GetComponent<PostProcessVolume>().profile.TryGetSettings(out lensDistor);
    }

    void Update()
    {
        if (clap == true)
        {
            lastTime = beatTicker;
            beatTicker = 0;
            fxCoolDown = fxSpeed;

            TileStep();
        }

        if (chromAber.intensity.value == 1)
        {
            chromAber.intensity.value = 0;
            lensDistor.intensity.value = 0;
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
            if (go)
                clapbetween = true;
        }

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

    private void TileStep()
    {
        if (hue.hueShift.value == 180)
            hue.hueShift.value = 0;
        else
            hue.hueShift.value += 50;
    }
}
