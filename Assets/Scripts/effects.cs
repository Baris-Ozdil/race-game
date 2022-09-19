using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour
{


    public ParticleSystem[] smoke = new ParticleSystem[4];
    private CarControler carControler;
    public aiControler AI;
    private bool smokeActive = false;
    private bool smokeFlag = false;
    public ParticleSystem[] nitroSmoke = new ParticleSystem[2];
    public TrailRenderer[] tRend = new TrailRenderer[4];
    private bool isTrack = false;
    public AudioSource trackSound;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "Player")
        {
            carControler = gameObject.GetComponent<CarControler>();
        }
        else if (this.gameObject.tag == "AiCar")
        {
            AI = gameObject.GetComponent<aiControler>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (this.gameObject.tag == "Player")
        {
            smokee();
            nitor();
            tierTrack();
        }
        else if (this.gameObject.tag == "AiCar")
        {
            smokeeAI();
            nitorAI();
            tierTrackAI();
        }

        
    }


    public void tierTrack()
    {
        if (carControler.track) 
        {
            if (isTrack) { return; }

            for (int i = 0; i < tRend.Length; i++)
            {
                tRend[i].emitting = true;
            }
            isTrack = true;

            trackSound.Play();
        }
        else
        {
            for (int i = 0; i < tRend.Length; i++)
            {
                tRend[i].emitting = false;
            }
            isTrack = false;

            trackSound.Stop();
        }
    }

    public void startSmoke()
    {
        if (smokeFlag) return;
        smokeActive = true;
        for (int i = 0; i < smoke.Length; i++)
        {
            var emission = smoke[i].emission;
            emission.rateOverTime = ((int)carControler.KphH * 2 >= 2000) ? (int)carControler.KphH * 2 : 2000;
            smoke[i].Play();
        }
        smokeFlag = true;

    }

    public void stopSmoke()
    {
        if (!smokeFlag) return;
        smokeActive = false;
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].Stop();
        }


    }
    public void smokee()
    {

        if (carControler.smoke && !smokeActive)
        {
            startSmoke();
        }
        else if (!carControler.smoke && smokeActive)
        {
            stopSmoke();
        }

        if (smokeFlag)
        {
            for (int i = 0; i < smoke.Length; i++)
            {
                var emission = smoke[i].emission;
                emission.rateOverTime = ((int)carControler.KphH * 10 <= 2000) ? (int)carControler.KphH * 10 : 2000;
            }
        }

    }


    public void nitor()
    {
        if (carControler.nitroSmoke)
        {
            for (int i= 0; i< 2; i++)
            {
                nitroSmoke[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                nitroSmoke[i].Stop();
            }
        }
    }

    //------------------------------------------------------------------------------------------


    public void tierTrackAI()
    {
        if (AI.track)
        {
            if (isTrack) { return; }

            for (int i = 0; i < tRend.Length; i++)
            {
                tRend[i].emitting = true;
            }
            isTrack = true;

            trackSound.Play();
        }
        else
        {
            for (int i = 0; i < tRend.Length; i++)
            {
                tRend[i].emitting = false;
            }
            isTrack = false;

            trackSound.Stop();
        }
    }

    public void startSmokeAI()
    {
        if (smokeFlag) return;
        smokeActive = true;
        for (int i = 0; i < smoke.Length; i++)
        {
            var emission = smoke[i].emission;
            emission.rateOverTime = ((int)AI.KphH * 2 >= 2000) ? (int)AI.KphH * 2 : 2000;
            smoke[i].Play();
        }
        smokeFlag = true;

    }

    public void stopSmokeAI()
    {
        if (!smokeFlag) return;
        smokeActive = false;
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].Stop();
        }


    }
    public void smokeeAI()
    {

        if (AI.smoke && !smokeActive)
        {
            startSmokeAI();
        }
        else if (!AI.smoke && smokeActive)
        {
            stopSmokeAI();
        }

        if (smokeFlag)
        {
            for (int i = 0; i < smoke.Length; i++)
            {
                var emission = smoke[i].emission;
                emission.rateOverTime = ((int)AI.KphH * 10 <= 2000) ? (int)AI.KphH * 10 : 2000;
            }
        }

    }


    public void nitorAI()
    {
        if (AI.nitroSmoke)
        {
            for (int i = 0; i < 2; i++)
            {
                nitroSmoke[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                nitroSmoke[i].Stop();
            }
        }
    }

}
