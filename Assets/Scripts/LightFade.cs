using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFade : MonoBehaviour
{
    public float baseIntensity;
    public float currIntensity;

    public float currTimer;

    public bool fadingIn;
    public bool loopStarted;
    public float lifetime;
  
    void Awake()
    {
        fadingIn = true;
        loopStarted = true;
        currIntensity = 0;
        currTimer = 0;
    }


    void Update()
    {
        if(fadingIn && currTimer < lifetime/2)
        {
            currTimer += Time.deltaTime;
        }

        if(fadingIn && currTimer >= lifetime / 2)
        {
            fadingIn = false;
        }

        if(!fadingIn && currTimer < lifetime)
        {
            currTimer += Time.deltaTime;
        }

        if (fadingIn && loopStarted)
        {
            loopStarted = false;
            StartCoroutine("FadeIn");
            

        }



        gameObject.GetComponent<Light2D>().intensity = currIntensity;

    }

    IEnumerator FadeIn()
    {
           int delay = Mathf.RoundToInt(lifetime / Time.deltaTime);
           currIntensity += (baseIntensity / delay);
           yield return new WaitForSeconds(Time.deltaTime);
        if (!fadingIn)
        {
            StopCoroutine("FadeIn");
            StartCoroutine("FadeOut");
        } else
        {
            StartCoroutine("FadeIn");
        }
        
    }

    IEnumerator FadeOut()
    {
        int delay = Mathf.RoundToInt(lifetime / Time.deltaTime);
        currIntensity -= (baseIntensity / delay);
        yield return new WaitForSeconds(Time.deltaTime);
        if(currTimer >= lifetime)
        {
            fadingIn = true;
            loopStarted = true;
            currIntensity = 0;
            currTimer = 0;
            StopCoroutine("FadeOut");
        } else
        StartCoroutine("FadeOut");
       

    }
}
