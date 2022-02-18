using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float maxHP;
    public float hp;

    public string entityTypeLayer;
    //aerial
    //ground
    //

    public bool isCamo;
    public GameObject levelObject;
    public GameObject levelManager;



    void Start()
    {
        hp = maxHP;
        levelObject = GameObject.Find("LEVEL");
        levelManager = GameObject.Find("LevelManager");

      
    }

   // public Animator anim; //assign an animator from the GameObject
    

    public void TakeDamage(float x)
    {
        hp -= x;

    }

    void Update()
    {
        if (hp <= 0)
        {


            if (tag == "Enemy")
            {
                levelObject.GetComponent<LevelStats>().enemiesKilled++;
                levelManager.GetComponent<GameFlow>().ee += Mathf.RoundToInt(maxHP / 20);
                levelManager.GetComponent<GameFlow>().EEanim.SetTrigger("EEblue");
            }


            if (gameObject.GetComponent<DeathAnimation>() != null)
            {
                gameObject.GetComponent<DeathAnimation>().trigger();
                this.enabled = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
