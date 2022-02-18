using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanotrooperAI : MonoBehaviour
{

    public Animator anim;
    public bool zoomed = false;
    public bool walk;


    public bool isShooting = false;
    public bool isWaiting = false;

    public Rigidbody2D rb;

    public float range;

    public GameObject rifleBullet1;
    public GameObject muzzle;
    public GameObject GameManager;
    public GameObject levelManager;
    public int[] upgrades;

    public bool canStepUp = false;

    public float bulletSpeed;
    public float fireRate = 1;

    public ParticleSystem ps;

    public Collider2D[] enemiesInRange;
    public Collider2D[] alliesInRange;
    public Collider2D[] allEnemies;
    public Collider2D followThis;
    public Collider2D currentTarget;
    public float stoppingDistance;

    public bool cond1;
    public bool cond2;
    public bool cond3;
    //default 5

    private Collider2D GetClosestEnemy(Collider2D[] allEnemies)
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



    void Start()
    {


        stoppingDistance = .5f;

    bulletSpeed = 5;
        walk = true;
        anim.SetBool("Walking", true);
        isShooting = false;

        GameManager = GameObject.FindGameObjectWithTag("gameManager");
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        upgrades = GameManager.GetComponent<PlayerInfo>().NanotrooperUpgrades;
        Target target = gameObject.GetComponent<Target>();

        //damage in rifleBullet.cs

        switch (upgrades[1])
        {
            case 0:
                range = 4;
                break;

            case 1:
                range = 4.33f;
                break;

            case 2:
                range = 4.66f;
                break;

            case 3:
                range = 5;
                break;

            case 4:
                range = 5.33f;
                break;

            case 5:
                range = 5.66f;
                break;

            case 6:
                range = 6;
                break;

            case 7:
                range = 6.33f;
                break;

            case 8:
                range = 6.66f;
                break;

            case 9:
                range = 7f;
                break;

            case 10:
                range = 8;
                break;

            default:
                range = 4;
                break;
        }


        target.maxHP = upgrades[2] * 30 + 200;   //base HP 200, 30 for each level

        switch (upgrades[3])    //fireRate
        {
            case 1:
                fireRate = 1.4f;
                break;

            case 2:
                fireRate = 1.3f;
                break;

            case 3:
                fireRate = 1.2f;
                break;

            case 4:
                fireRate = 1.1f;
                break;

            case 5:
                fireRate = 1f;
                break;

            case 6:
                fireRate = 0.9f;
                break;

            case 7:
                fireRate = 0.8f;
                break;

            case 8:
                fireRate = 0.7f;
                break;

            case 9:
                fireRate = 0.6f;
                break;

            case 10:
                fireRate = 0.5f;
                break;

            default:
                fireRate = 1.5f;
                break;
        }   //fireRate




    }


    void Update()
    {

        currentTarget = GetClosestEnemy(allEnemies);


        allEnemies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y + 1),
            new Vector2(transform.position.x + 100, transform.position.y - 1), 1 << 7);

        enemiesInRange = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y + 1),
            new Vector2(transform.position.x + range, transform.position.y - 1), 1 << 7);

        alliesInRange = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - .3f, transform.position.y + 1),
           new Vector2(transform.position.x + 10f, transform.position.y - 1), 1 << 8);


        if (alliesInRange.Length > 1)
        {
            for (int i = 0; i < alliesInRange.Length; i++)
            {

                if(alliesInRange[i].transform.parent.gameObject.name != "Player Crystal") {

                    if (alliesInRange[i].transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1)
                    {

                        followThis = alliesInRange[i];
                    }
                }
            }

            if (followThis != null)
            {
                if (followThis.transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1 &&
                          Vector2.Distance(transform.position, GetClosestEnemy(allEnemies).transform.position) > stoppingDistance * 2 &&
                          Vector2.Distance(transform.position, followThis.transform.position) > stoppingDistance)
                {
                    canStepUp = true;
                }
                else
                {
                    canStepUp = false;
                }
            } else
            {
                if (Vector2.Distance(transform.position, GetClosestEnemy(allEnemies).transform.position) > stoppingDistance * 2)
                {
                    canStepUp = true;
                } else
                {
                    canStepUp = false;
                }
            }
        }
        else if (enemiesInRange.Length == 0)
        {
            canStepUp = true;
        }
        else
        {
            if (Vector2.Distance(transform.position, GetClosestEnemy(allEnemies).transform.position) > stoppingDistance * 2)
            {
                canStepUp = true;
            }
            else
            {
                canStepUp = false;
            }


        }





        if (enemiesInRange.Length > 0)
        {
            if (!isShooting)
            {
              //  walk = false;
               // anim.SetBool("Walking", false);
                anim.SetTrigger("SwitchToAim");
                StartCoroutine("Fire");
                isShooting = true;
            }

            if(isShooting && canStepUp)
            {
                walk = true;
                anim.SetBool("Walking", true);
            }
        }


        if (enemiesInRange.Length == 0 && canStepUp)
        {
            if (isShooting)
            {
                walk = true;
                anim.SetBool("Walking", true);
                anim.SetTrigger("SwitchToIdle");
                StopCoroutine("Fire");
                isShooting = false;
            }
        }




        if (!canStepUp)
        {
            walk = false;
            anim.SetBool("Walking", false);

        }

        if (canStepUp && enemiesInRange.Length == 0)
        {
            walk = true;
            anim.SetBool("Walking", true);
        }

        if (canStepUp && alliesInRange.Length == 0)
        {
            walk = true;
            anim.SetBool("Walking", true);
        }


        if (walk)
        {
            rb.velocity = new Vector2(levelManager.GetComponent<GameFlow>().unitMovementSpeed, 0);
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }




    }


    //git test v2



    IEnumerator Fire()
    {
        yield return new WaitForSeconds(fireRate);
        ps.Play();
        GameObject bullet = Instantiate(rifleBullet1, muzzle.transform.position, transform.rotation);
        anim.SetTrigger("Fire");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletSpeed, 0);
        StartCoroutine("Fire");
    }
}
