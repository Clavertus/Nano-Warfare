using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_1 : MonoBehaviour
{

    public GameObject panel;
    public GameObject msg1;
    public EE_bursts EEgen;

    public GameObject trainingBlob;

    bool checkForStatus;

    float threshold = .1f;
    public GameObject h; //highlight

    public EnemySpawner spawner;

    public bool triggered;


    void Start()
    {
        triggered = false;
        checkForStatus = true;
        EEgen.instantStart = false;
        StartCoroutine(StartRegardless());
    }

    void Update()
    {
        
        if (h != null)
        {
            SpriteRenderer sr = h.GetComponent<SpriteRenderer>();

            float alpha = Mathf.PingPong(Time.time * 0.3f, threshold);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }

        if (trainingBlob != null)
        {
            if (trainingBlob.GetComponent<EE_blob>().slerping && checkForStatus)
            {
                checkForStatus = false;
                unpause();
                h.SetActive(false);
            }
        }

    }


    public void unpause()
    {
        if (!triggered)
        {
            triggered = true;
            panel.SetActive(false);
            msg1.SetActive(false);
            EEgen.StartCoroutine(EEgen.Generate());
            if(spawner.gameObject.activeSelf)
                //  spawner.StartCoroutine(spawner.AnalyzeSeed("s8ss93sn88ss88snn|999sn88ss88n"));
                spawner.StartCoroutine(spawner.AnalyzeSeed("|n9"));
        }
    }

    public IEnumerator StartRegardless()
    {
        yield return new WaitForSeconds(10f);
        unpause();
    }
    

    

}
