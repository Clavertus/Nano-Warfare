using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarAlignment : MonoBehaviour
{
    public Collider2D[] arrE;
    public Collider2D[] arrA;


    public Color color_fullhp;
    public Color color_zerohp;

    public Color a_Crystal_full;
    public Color a_Crystal_zero;

    public Color e_Crystal_full;
    public Color e_Crystal_zero;

    public RectTransform a_bar;
    public GameObject a_crystal;

    public RectTransform e_bar;
    public GameObject e_crystal;




    void Update()
    {
        UpdateUnits();
        UpdateCrystals();
    }




    void UpdateUnits()
    {
        arrE = Physics2D.OverlapAreaAll(new Vector2(-12, -6), new Vector2(12, 2), 1 << 7); //enemies
        arrA = Physics2D.OverlapAreaAll(new Vector2(-12, -6), new Vector2(12, 2), 1 << 8); //allies     
        

        foreach(Collider2D c in arrA)
        {
           
            if(c.transform.parent.gameObject.GetComponent<Target>() != null)
            {

                Target target = c.transform.parent.gameObject.GetComponent<Target>();

                if (c.transform.parent.gameObject.transform.Find("HealthBar") != null)
                {
                    GameObject hb = c.transform.parent.gameObject.transform.Find("HealthBar").gameObject;
                    Transform hb_t = hb.transform;
                    SpriteRenderer hb_sr = hb.GetComponent<SpriteRenderer>();
                    hb_t.localScale = new Vector3((target.hp / target.maxHP) * 50, hb_t.localScale.y, hb_t.localScale.z);

                    float perc = target.hp / target.maxHP;
                    perc = -(Mathf.Cos(Mathf.PI * perc) - 1) / 2;                    //easeInOutSine
                    hb_sr.color = Color.Lerp(color_zerohp, color_fullhp, perc);      //swapped because perc goes from 1 to 0
                }
                  


                              
            } else
            {


               // Warning("Object inside array 'arrA' is not a Target!");
            }
        }


        foreach (Collider2D c in arrE)
        {

            if (c.transform.parent.gameObject.GetComponent<Target>() != null)
            {

                Target target = c.transform.parent.gameObject.GetComponent<Target>();

                if (c.transform.parent.gameObject.transform.Find("HealthBar") != null)
                {
                    GameObject hb = c.transform.parent.gameObject.transform.Find("HealthBar").gameObject;
                    Transform hb_t = hb.transform;
                    SpriteRenderer hb_sr = hb.GetComponent<SpriteRenderer>();


                    hb_t.localScale = new Vector3((target.hp / target.maxHP) * 50, hb_t.localScale.y, hb_t.localScale.z);

                    float perc = target.hp / target.maxHP;
                    perc = -(Mathf.Cos(Mathf.PI * perc) - 1) / 2;                    //easeInOutSine
                    hb_sr.color = Color.Lerp(color_zerohp, color_fullhp, perc);      //swapped because perc goes from 1 to 0
                }
               

            }
            else
            {
               // Debug.LogWarning("Object inside array 'arrA' is not a Target!");
            }
        }
    }

    void UpdateCrystals()
    {

        Target target_a = a_crystal.GetComponent<Target>();
        Target target_e = e_crystal.GetComponent<Target>();

        a_bar.localScale = new Vector3(target_a.hp/target_a.maxHP, a_bar.localScale.y, a_bar.localScale.z);
        e_bar.localScale = new Vector3(target_e.hp/target_e.maxHP, e_bar.localScale.y, e_bar.localScale.z);
    }

}
