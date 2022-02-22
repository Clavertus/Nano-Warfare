using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public bool e_haloSpin;
    public GameObject e_halo;

    public bool haloSpin;   
    public GameObject halo;

    public bool endLerp;

    public float spinVal;
    public float spinTime;
    public float c_spinTime;

    public float lerpTime;
    public float c_lerpTime;

    public float endLerpTime;
    public float c_endLerpTime;
    public float endLerpVal;

    public float c_UpTimer;
    public float m_UpTimer;
    public float upVal;
    public bool lerpUp;

    public Vector2 startPos;

    public GameObject UI_Canvas;
    public GameObject e_spawner;
    public GameFlow gameflow;
    public GameObject leftScroll;
    public GameObject rightScroll;

    public Collider2D[] arrA;
    public Collider2D[] arrE;

    public GameObject ex_light;
    public GameObject ex_light_e;
    public GameObject ex_ps;
    public GameObject ex_ps_e;
    public GameObject idle_ps;
    public GameObject e_idle_ps;

    public GameOverMessage gameOverMessage;

    public Animator a_anim;
    public Animator e_anim;

    public SpriteRenderer crystalSprite;
    public SpriteRenderer e_crystalSprite;

    //FIX UNITS IN GAMEOVER!!!!

    public GameObject endPanel;
    public Image defeat;
    public Image victory;

    public void Start()
    {
        endLerpTime = 3f;
        spinTime = 3f;
        lerpTime = 3f;
        m_UpTimer = 1f;
    }


    public void endGame(int result)
    {
        UI_Canvas.SetActive(false);
        gameflow.enabled = false;
        e_spawner.SetActive(false);
        leftScroll.SetActive(false);
        rightScroll.SetActive(false);

        foreach(Collider2D c in arrA)
        {
            if (c.transform.parent.name != "Player Crystal")
            {
                if (c.transform.parent.GetComponent<Animator>() != null)
                {
                    c.transform.parent.GetComponent<Animator>().enabled = false;
                }

                if (c.transform.parent.GetComponent<Rigidbody2D>() != null)
                {
                    c.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }

                if (c.transform.parent.GetComponent<Target>() != null)
                {
                    c.transform.parent.GetComponent<Target>().enabled = false;
                }

                if (c.transform.parent.GetComponent<StingerAI>() != null)
                {
                    c.transform.parent.GetComponent<StingerAI>().StopAllCoroutines();
                    c.transform.parent.GetComponent<StingerAI>().enabled = false;
                }

                if (c.transform.parent.GetComponent<NanotrooperAI>() != null)
                {
                    c.transform.parent.GetComponent<NanotrooperAI>().StopAllCoroutines();
                    c.transform.parent.GetComponent<NanotrooperAI>().enabled = false;
                }
            }


        }

        foreach (Collider2D c in arrE)
        {
            if (c.transform.parent.name != "Enemy Crystal")
            {
                if (c.transform.parent.GetComponent<Animator>() != null)
                {
                    c.transform.parent.GetComponent<Animator>().enabled = false;
                }

                if (c.transform.parent.GetComponent<Rigidbody2D>() != null)
                {
                    c.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }

                if (c.transform.parent.GetComponent<Target>() != null)
                {
                    c.transform.parent.GetComponent<Target>().enabled = false;
                }

                if (c.transform.parent.GetComponent<EnemyStingerAI>() != null)
                {
                    c.transform.parent.GetComponent<EnemyStingerAI>().StopAllCoroutines();
                    c.transform.parent.GetComponent<EnemyStingerAI>().enabled = false;
                }

                if (c.transform.parent.GetComponent<EnemyNanotrooperAI>() != null)
                {
                    c.transform.parent.GetComponent<EnemyNanotrooperAI>().StopAllCoroutines();
                    c.transform.parent.GetComponent<EnemyNanotrooperAI>().enabled = false;
                }
            }


        }




        if (result == 0) //lose
        {
            haloOn();        
        }
        if(result == 1) //win
        {
            e_haloOn();
        }
    }

 

    public void e_haloOn()
    {
        e_halo.SetActive(true);
        e_haloSpin = true;
        startPos = Camera.main.transform.position;
    }

    public void haloOn()
    {
        halo.SetActive(true);
        haloSpin = true;
        startPos = Camera.main.transform.position;
    }

    public void FixedUpdate()
    {
        arrE = Physics2D.OverlapAreaAll(new Vector2(-12, -6), new Vector2(12, 2), 1 << 7); //enemies
        arrA = Physics2D.OverlapAreaAll(new Vector2(-12, -6), new Vector2(12, 2), 1 << 8); //allies  

        if (e_haloSpin)
        {
            float perc = c_spinTime / spinTime;

            perc = 1 - Mathf.Pow(1 - perc, 5);
            spinVal = Mathf.Lerp(0, 7, perc);
            e_halo.transform.Rotate(new Vector3(0, 0, spinVal));

            if (c_spinTime < spinTime)
            {
                c_spinTime += Time.deltaTime;
            }
            else
            {
                explode_e();
            }



            float m_perc = c_lerpTime / lerpTime;
            if (c_lerpTime < lerpTime)
            {
                c_lerpTime += Time.deltaTime;
            }


            m_perc = 1 - Mathf.Pow(1 - m_perc, 5);

            Camera.main.transform.position = Vector3.Lerp(startPos, new Vector2(6, startPos.y), m_perc);

        }

        if (haloSpin)
        {
            float perc = c_spinTime / spinTime;

            perc = 1 - Mathf.Pow(1 - perc, 5);
            spinVal = Mathf.Lerp(0, 7, perc);
            halo.transform.Rotate(new Vector3(0, 0, spinVal));

            if (c_spinTime < spinTime)
            {
                c_spinTime += Time.deltaTime;
            }
            else
            {
                explode_a();
            }



            float m_perc = c_lerpTime / lerpTime;
            if (c_lerpTime < lerpTime)
            {
                c_lerpTime += Time.deltaTime;
            }


            m_perc = 1 - Mathf.Pow(1 - m_perc, 5);

            Camera.main.transform.position = Vector3.Lerp(startPos, new Vector2(-3, startPos.y), m_perc);

        }

        if (endLerp)
        {
            float perc = c_endLerpTime / endLerpTime;

            perc = 1 - Mathf.Pow(1 - perc, 5);
            endLerpVal = Mathf.Lerp(0, 1, perc);

            if (victory.enabled)
            {
                victory.color = new Color(victory.color.r, victory.color.g, victory.color.b, endLerpVal);
            }
            else
            {
                defeat.color = new Color(defeat.color.r, defeat.color.g, defeat.color.b, endLerpVal);
            }




            if (c_endLerpTime < endLerpTime)
            {
                c_endLerpTime += Time.deltaTime;
            }
            else
            {
                endLerp = false;
                gameOverMessage.show();
                lerpUp = true;
            }
        }

        if (lerpUp)
        {
            if (c_UpTimer < m_UpTimer)
            {
                c_UpTimer += Time.deltaTime;
                upVal = c_UpTimer / m_UpTimer;
                upVal = -(Mathf.Cos(Mathf.PI * upVal) - 1) / 2;
                if (victory.enabled) {
                    victory.GetComponent<RectTransform>().anchoredPosition = new Vector2(victory.GetComponent<RectTransform>().anchoredPosition.x, Mathf.Lerp(70f, 200f, upVal));
                        } else
                {
                    defeat.GetComponent<RectTransform>().anchoredPosition = new Vector2(defeat.GetComponent<RectTransform>().anchoredPosition.x, Mathf.Lerp(0, 130f, upVal));
                }
            } else
            {
                lerpUp = false;
            }
           
          
        

        }
    }

    public void explode_a()
    {
        ex_ps.SetActive(true);
        haloSpin = false;
        a_anim.Play("Explode_Light");
        crystalSprite.enabled = false;
        halo.SetActive(false);
        ex_light.SetActive(true);
        idle_ps.SetActive(false);
        endPanelOn(0); //lose
    }

    public void explode_e()
    {
        ex_ps_e.SetActive(true);
        e_haloSpin = false;
        e_anim.Play("Explode_Light_E");
        e_crystalSprite.enabled = false;
        e_halo.SetActive(false);
        e_idle_ps.SetActive(false);
        ex_light_e.SetActive(true);
        endPanelOn(1); //win
    }

    public void endPanelOn(int condition)
    {
        if(condition == 0)
        {
            StartCoroutine(Defeat());
        } else
        {
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1.5f);
        endPanel.SetActive(true);
        victory.enabled = true;
        endLerp = true;
    }

    IEnumerator Defeat()
    {
        yield return new WaitForSeconds(1.5f);
        endPanel.SetActive(true);
        defeat.enabled = true;
        endLerp = true;
    }



}
