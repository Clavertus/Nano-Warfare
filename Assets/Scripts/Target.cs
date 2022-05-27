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
    public bool specialShutdown;
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
        Debug.Log(gameObject.name + " has taken " + x + " damage.");
    }

    void Update()
    {
        if (hp <= 0)
        {


            if (tag == "Enemy")
            {
                levelObject.GetComponent<LevelStats>().enemiesKilled++;
                int bonusVal = Mathf.RoundToInt(maxHP / 20);
                levelManager.GetComponent<GameFlow>().ee += bonusVal;
                levelObject.GetComponent<RewardTracker>().AddDestructionBonus(bonusVal);
                levelManager.GetComponent<GameFlow>().EEanim.SetTrigger("EEblue");
            }


            if (gameObject.GetComponent<DeathAnimation>() != null)
            {
                gameObject.GetComponent<DeathAnimation>().trigger();
                this.enabled = false;
            }
            else
            {
              //  if(!specialShutdown)
                Destroy(gameObject);
            }
        }
    }

}
