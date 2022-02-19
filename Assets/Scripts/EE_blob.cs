using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EE_blob : MonoBehaviour
{


    public int EEprovided;
    public float lifeTime;


    int value;
    float lerpTime;
    float c_lerpTime;
    bool lerping = false;
    [HideInInspector]
    public bool slerping = false;
    Color currColor;
    Color startingColor;
    Color transparent;
    Transform ps_transform;
    Transform crystal;
    float c_slerpingTime;
    float slerpingTime;
    Vector2 center;
    Vector2 startRelCenter;
    Vector2 crystalRelCenter;
    Vector2 startPos;

    

    void Start()
    {
        if(lifeTime == 0)
        {
            lifeTime = 3;
        }
        crystal = GameObject.Find("blob_collapse_point").transform;
        slerpingTime = 1;
        lerping = false;
        slerping = false;
        StartCoroutine(LifeTime());
        lerpTime = 1f;
        c_lerpTime = 0;
        ps_transform = gameObject.transform.Find("ps").transform;
        Transform col_transform = gameObject.transform.Find("TapCollider").transform;
        value = Random.Range(1, 100);

        if(value < 20)
        {
            ps_transform.localScale = new Vector2(0.2f, 0.2f);
            EEprovided = 2;
        } else
        {
            ps_transform.localScale = new Vector2(value / 100f, value / 100f);
            EEprovided = Mathf.RoundToInt(value / 10);
        }
 
    }

    void Update()
    {

        if (lerping)
        {
            if (c_lerpTime < lerpTime)
            {
                c_lerpTime += Time.deltaTime;
                float perc = c_lerpTime / lerpTime;
                var mainModule = ps_transform.gameObject.GetComponent<ParticleSystem>().main;
                mainModule.startColor = Color.Lerp(startingColor, transparent, perc);



            } else
            {
                lerping = false;
            }
        }

        if (slerping)
        {
          
            center = (new Vector2(crystal.position.x, crystal.position.y) + new Vector2(startPos.x, startPos.y))  * 0.5f;        
            center -= new Vector2(0, 10f);

            crystalRelCenter = new Vector2(crystal.position.x, crystal.position.y) - center;
            startRelCenter = new Vector2(startPos.x, startPos.y) - center;

           

            if (c_slerpingTime < slerpingTime)
            {
                c_slerpingTime += Time.deltaTime;
                float perc = c_slerpingTime / slerpingTime;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                transform.position = Vector3.Slerp(startRelCenter, crystalRelCenter, perc);
                transform.position += new Vector3(center.x, center.y, 0);

                if (perc >= 0.99f)
                {
                    GameObject.Find("LevelManager").GetComponent<GameFlow>().addEE(EEprovided);
                    Destroy(gameObject, 2f);
                    slerping = false;
                }
            }
            

            
        }

    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        lerping = true;
        yield return new WaitForSeconds(lerpTime*3);
        Destroy(gameObject);
        
    }

    public void pickup()
    {
        StopAllCoroutines();

        slerpingTime = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(crystal.position.x, crystal.position.y)) / 10f;

        slerping = true;
        startPos = new Vector2(transform.position.x, transform.position.y);
       
           

    }


    
}
