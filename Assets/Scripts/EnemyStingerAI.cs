using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStingerAI : MonoBehaviour
{
    public Animator anim;
    public bool walk;

    public Transform sting;
    public GameObject stabParticle;

    public bool isShooting;
    public bool isWaiting = true;
    public bool canStepUp;
    public float stoppingDistance;


    public Rigidbody2D rb;

    public float range;

    public GameObject GameManager;
    public GameObject levelManager;
    public int[] upgrades;


    public float attackSpeed = 1;

    public ParticleSystem ps;
    public GameObject psParent;

    public Collider2D[] allEnemies;
    public Collider2D[] allAllies;
    public Collider2D followThis;
    //default 5


    public float slashDamage;

    public bool cond1;
    public bool cond2;
    public bool cond3;
    public bool canAttack = true;

    private Collider2D GetClosestEntity(Collider2D[] allEnemies)
    {
        Collider2D tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Collider2D t in allEnemies)
        {
            if (t != null)
            {
                float dist = Vector2.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }

    void Awake()
    {      
    }


    void Start()
    {
        walk = true;

        ps = psParent.transform.GetChild(0).GetComponent<ParticleSystem>();

        GameManager = GameObject.FindGameObjectWithTag("gameManager");
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        upgrades = GameManager.GetComponent<PlayerInfo>().StingerUpgrades;
        Target target = gameObject.GetComponent<Target>();
        slashDamage = upgrades[0] * 5 + 30;





        target.maxHP = upgrades[1] * 30 + 100;   //base HP 100, 30 for each level

        switch (upgrades[2])    //attackSpeed
        {
            case 1:
                attackSpeed = 2f;
                break;

            case 2:
                attackSpeed = 1.85f;
                break;

            case 3:
                attackSpeed = 1.7f;
                break;

            case 4:
                attackSpeed = 1.55f;
                break;

            case 5:
                attackSpeed = 1.4f;
                break;

            case 6:
                attackSpeed = 1.25f;
                break;

            case 7:
                attackSpeed = 1.1f;
                break;

            case 8:
                attackSpeed = 0.95f;
                break;

            case 9:
                attackSpeed = 0.7f;
                break;

            case 10:
                attackSpeed = 0.55f;
                break;

            default:
                attackSpeed = 2f;
                break;
        }   //fireRate




    }



    void Update()
    {

        allEnemies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y + 1),
            new Vector2(transform.position.x - 100, transform.position.y - 1), 1 << 8);

        allAllies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + 1f, transform.position.y + 1),
           new Vector2(transform.position.x - 10, transform.position.y - 1), 1 << 7);


        

        if (allEnemies.Length > 0)
        {

            if (Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) <= stoppingDistance * 2 && canAttack)
            {
                canAttack = false;
                StartCoroutine("Attack");
            }
        }



        if (allAllies.Length > 1)
        {
            for (int i = 0; i < allAllies.Length; i++)
            {
                if (allAllies[i].transform.parent.gameObject.name != "Enemy Crystal")
                {
                    if (allAllies[i].transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1)
                    {

                        followThis = allAllies[i];
                    }
                }
                
            }


            if (followThis != null)
            {
              
                if (followThis.transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1 &&
                          Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) > stoppingDistance * 2 &&
                          Vector2.Distance(transform.position, followThis.transform.position) > stoppingDistance * 2)
                {
                    canStepUp = true;
                }
                else
                {
                    canStepUp = false;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) > stoppingDistance * 2)
                {
                    canStepUp = true;
                }
                else
                {
                    canStepUp = false;
                }
            }
        }
        else if (allEnemies.Length == 0)
        {
            canStepUp = true;
        }
        else
        {
            if (Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) > stoppingDistance * 2)
            {
                canStepUp = true;
            }
            else
            {
                canStepUp = false;
            }


        }




        /* if (Input.GetKeyDown("z")) {

              Debug.Log(Vector2.Distance(transform.position, GetClosestAlly(allAllies).transform.position));

          }

          */



        if (allEnemies.Length > 0)
        {
            rb.velocity = new Vector2(0, 0);
        }


        if (allEnemies.Length == 0 && canStepUp)
        {

            walk = true;
            StopCoroutine("Attack");

        }


        if (allAllies.Length > 0)
        {


        }




        if (!canStepUp)
        {
            walk = false;

        }
        else
        {
            walk = true;
        }


        if (walk)
        {
            rb.velocity = new Vector2(-levelManager.GetComponent<GameFlow>().unitMovementSpeed, 0);
        }




    }





    IEnumerator Attack()
    {
        if (rb.velocity == new Vector2(0, 0))
        {
            anim.SetTrigger("Sting");


            yield return new WaitForSeconds(0.375f);

            //singletarget damage

            if (GetClosestEntity(allEnemies).transform.parent.GetComponent<Target>() != null)
            {

                GetClosestEntity(allEnemies).transform.parent.GetComponent<Target>().TakeDamage(slashDamage);
                ps.Play();
            }


            /*
           for piercing damage


            for(int i = 0; i < allEnemies.Length; i++)
           {

           }

             */

            yield return new WaitForSeconds(attackSpeed);
            StartCoroutine("Attack");
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine("Attack");
        }






    }



}
