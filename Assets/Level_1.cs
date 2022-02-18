using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : MonoBehaviour
{
    /*
        1-1
        Tutorial level.
        Player learns basic rules, how to navigate the battlefield and how to spawn entities.
        Tutorial consists of three waves of enemies.
        Wave 1: ssss
        Wave 2: snn
        Wave 3: ssnnn
    */

    public GameObject a_controller;
    public GameObject e_controller;

    public bool stage1;
    public bool stage2;
    public bool stage3;

    LevelStats ls;
    EnemySpawner es;

    public GameObject[] msg;
    public GameObject panel;
    public GameObject[] highlight;

    public GameObject battleLog;
    public GameObject queue;
    public GameObject EEcount;
    public GameObject healthbar_a;
    public GameObject healthbar_e;
    public GameObject scroll_L;
    public GameObject scroll_R;

    public GameObject Stinger;
    public GameObject Nanotrooper;

    public Vector2 initialCamPos;


    public bool messageShown;
    public bool oneTime;
    public bool stage1beaten = false;
    public bool stage2beaten = false;
    public bool stage3beaten = false;

    public bool lerping;
    public float c_lerpTime;
    public float lerpTime;

    public float alpha;
    public float threshold;



    void Start()
    {
        ls = GetComponent<LevelStats>();
        es = e_controller.GetComponent<EnemySpawner>();
        messageShown = false;
        oneTime = true;
        lerping = false;

        alpha = 0;
        threshold = 0.1f;

        c_lerpTime = 0;
        lerpTime = 1.5f;

        StartGame();
    }


    void StartGame()
    {
        

        panel.SetActive(true);
        msg[0].SetActive(true);

    }


    public void msg1()
    {

        highlight[0].SetActive(true);
        msg[0].SetActive(false);
        msg[1].SetActive(true);

    }

    public void msg2()
    {

        highlight[0].SetActive(false);
        highlight[1].SetActive(true);
        highlight[2].SetActive(true);

        scroll_L.SetActive(true);
        scroll_R.SetActive(true);

        msg[1].SetActive(false);
        msg[2].SetActive(true);
      

    }

    public void msg4()
    {
       
        msg[3].SetActive(false);
        highlight[3].SetActive(false);
        panel.SetActive(false);

        initialCamPos = Camera.main.transform.position;
        lerping = true;
    }

    public void msg4_2()
    {
        msg[4].SetActive(true);
        highlight[4].SetActive(true);
        panel.SetActive(true);
        battleLog.SetActive(true);
    }

    public void msg5()
    {
        msg[4].SetActive(false);
        highlight[4].SetActive(false);

        Stinger.SetActive(true);
        msg[5].SetActive(true);
        highlight[5].SetActive(true);
    }

    public void msg5_2()
    {
        if (oneTime)
        {

            oneTime = false;
            msg[5].SetActive(false);
            highlight[5].SetActive(false);

            msg[6].SetActive(true);
            highlight[6].SetActive(true);
            queue.SetActive(true);

            a_controller.SetActive(true);
            e_controller.SetActive(true);
            es.StartCoroutine(es.AnalyzeSeed("ss"));




        }

    }

    IEnumerator Timer()
       {

        yield return new WaitForSeconds(3f);
        msg[7].SetActive(true);
        highlight[7].SetActive(true);
        panel.SetActive(true);
        EEcount.SetActive(true);
        Pause();

    }

    public void msg5_3()
    {
        StartCoroutine(Timer());
        msg[6].SetActive(false);
        highlight[6].SetActive(false);
        panel.SetActive(false);
        healthbar_a.SetActive(true);
        healthbar_e.SetActive(true);

    }

    public void msg6()
    {
        msg[7].SetActive(false);
        highlight[7].SetActive(false);
        panel.SetActive(false);
        Unpause();
    }



    public void msg7()
    {
        msg[8].SetActive(true);
        highlight[8].SetActive(true);
        panel.SetActive(true);
        Nanotrooper.SetActive(true);
        a_controller.GetComponent<GameFlow>().ee += 50;
        Pause();


    }

    public void msg7_2()
    {
        msg[8].SetActive(false);
        highlight[8].SetActive(false);
        panel.SetActive(false);
        Unpause();

        es.StartCoroutine(es.AnalyzeSeed("snn"));
    }

    public void msg8()
    {
        msg[9].SetActive(true);
        panel.SetActive(true);
        a_controller.GetComponent<GameFlow>().ee += 80;
        Pause();

    }


    public void msg8_2()
    {
        msg[9].SetActive(false);
        panel.SetActive(false);
        es.StartCoroutine(es.AnalyzeSeed("ssnnn"));
        Unpause();

    }


    public void msg9()
    {
        msg[10].SetActive(true);
        panel.SetActive(true);
        Pause();
    }

    public void msg9_2()
    {
        msg[10].SetActive(false);
        panel.SetActive(false);
        Unpause();
    }



    void Pause()
    {
        Time.timeScale = 0;
    }

    void Unpause()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

        

        if (lerping)
        {
            if(c_lerpTime < lerpTime)
            {
                c_lerpTime += Time.deltaTime;
                float perc = c_lerpTime / lerpTime;
                perc = -(Mathf.Cos(Mathf.PI * perc) - 1) / 2;                                                           //easeInOutSine
                Vector2 camPos = Vector2.Lerp(initialCamPos, new Vector2(-3, Camera.main.transform.position.y), perc);
                Camera.main.transform.position = camPos;
            } else
            {
                msg4_2();
                lerping = false;            
            }
        }

        if (Camera.main.transform.position.x > 5 && !messageShown)
        {
            messageShown = true;

            msg[2].SetActive(false);
            msg[3].SetActive(true);

            highlight[1].SetActive(false);
            highlight[2].SetActive(false);

            highlight[3].SetActive(true);

        }

        foreach(GameObject g in highlight)
        {
            if (g.GetComponent<SpriteRenderer>() != null)
            {


                SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
                
                alpha = Mathf.PingPong(Time.time * 0.3f, threshold);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);


            } else if (g.GetComponent<Image>() != null)
            {


                Image i = g.GetComponent<Image>();            
        
                alpha = Mathf.PingPong(Time.time * 0.3f, threshold);
                i.color = new Color(i.color.r, i.color.g, i.color.b, alpha);


            }
        }



        if(ls.enemiesKilled == 2 && !stage1beaten)
        {
            stage1beaten = true;
            msg7();
        }

        if (ls.enemiesKilled == 5 && !stage2beaten)
        {
            stage2beaten = true;
            msg8();
        }

        if (ls.enemiesKilled == 10 && !stage3beaten)
        {
            stage3beaten = true;
            msg9();
        }





    }
}
