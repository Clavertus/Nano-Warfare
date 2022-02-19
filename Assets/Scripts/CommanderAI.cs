using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderAI : MonoBehaviour
{
    public Animator anim;
    public bool zoomed = false;
    public bool walk;


    public bool isShooting;
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

    public Collider2D[] allEnemies;
    public Collider2D[] allAllies;
    public Collider2D followThis;


    public float stoppingDistance;
    public float currDistance = 0;
    public float closestDistance = 10;

    public bool cond1;
    public bool cond2;
    public bool cond3;
    //default 5

    Collider2D GetClosestEntity(Collider2D[] enemies)
    {
        Collider2D tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider2D t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    Collider2D GetClosestAlly(Collider2D[] enemies)
    {
        Collider2D tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider2D t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


    void Start()
    {
       
        

        stoppingDistance = .5f;

        bulletSpeed = 8;
        walk = true;
        anim.SetBool("Walking", true);
        isShooting = false;

        GameManager = GameObject.FindGameObjectWithTag("gameManager");
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        upgrades = GameManager.GetComponent<PlayerInfo>().CommanderUpgrades;
        Target target = gameObject.GetComponent<Target>();

        //damage in rifleBullet.cs

        switch (upgrades[1])
        {
            case 0:
                range = 3;
                break;

            case 1:
                range = 3.33f;
                break;

            case 2:
                range = 3.66f;
                break;

            case 3:
                range = 4;
                break;

            case 4:
                range = 4.33f;
                break;

            case 5:
                range = 4.66f;
                break;

            case 6:
                range = 5;
                break;

            case 7:
                range = 5.33f;
                break;

            case 8:
                range = 5.66f;
                break;

            case 9:
                range = 6f;
                break;

            case 10:
                range = 7;
                break;

            default:
                range = 3;
                break;
        }


        target.maxHP = upgrades[2] * 35 + 200;   //base HP 200, 30 for each level

        switch (upgrades[3])    //fireRate
        {
            case 1:
                fireRate = 2f;
                break;

            case 2:
                fireRate = 1.85f;
                break;

            case 3:
                fireRate = 1.7f;
                break;

            case 4:
                fireRate = 1.55f;
                break;

            case 5:
                fireRate = 1.4f;
                break;

            case 6:
                fireRate = 1.25f;
                break;

            case 7:
                fireRate = 1.1f;
                break;

            case 8:
                fireRate = 0.95f;
                break;

            case 9:
                fireRate = 0.7f;
                break;

            case 10:
                fireRate = 0.55f;
                break;

            default:
                fireRate = 2f;
                break;
        }   //fireRate




    }


    void Update()
    {

        allEnemies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y + 1),
            new Vector2(transform.position.x + range, transform.position.y - 1), 1 << 7);

        allAllies = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - .3f, transform.position.y + 1),
           new Vector2(transform.position.x + 10f, transform.position.y - 1), 1 << 8);


        if (allAllies.Length > 1)
            {
                for (int i = 0; i < allAllies.Length; i++)
                {

                    if (allAllies[i].transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1)
                    {

                        followThis = allAllies[i];
                    }
                }


            if (followThis != null)
            {
                if (followThis.transform.parent.gameObject.GetComponent<Priority>().priority == gameObject.GetComponent<Priority>().priority - 1 &&
                          Vector2.Distance(transform.position, GetClosestEntity(allEnemies).transform.position) > stoppingDistance &&
                          Vector2.Distance(transform.position, followThis.transform.position) > stoppingDistance)
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



        if (allEnemies.Length > 0)
        {
            if (!isShooting)
            {
                anim.SetTrigger("SwitchToAim");
                StartCoroutine("Fire");
                isShooting = true;
            }

            if (isShooting && canStepUp)
            {
                walk = true;
                anim.SetBool("Walking", true);
            }
        }


        if (allEnemies.Length == 0 && canStepUp)
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

        if (canStepUp && allEnemies.Length == 0)
        {
            walk = true;
            anim.SetBool("Walking", true);
        }

        if (canStepUp && allAllies.Length == 0)
        {
            walk = true;
            anim.SetBool("Walking", true);
        }


        if (walk)
        {
            rb.velocity = new Vector2(levelManager.GetComponent<GameFlow>().unitMovementSpeed, 0);
        }




    }




    IEnumerator Fire()
    {
        yield return new WaitForSeconds(fireRate);
        ps.Play();
        GameObject bullet = Instantiate(rifleBullet1, muzzle.transform.position, transform.rotation);
        anim.SetTrigger("Fire");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletSpeed, 0);
        yield return new WaitForSeconds(fireRate / 3);

        ps.Play();
        GameObject bullet2 = Instantiate(rifleBullet1, muzzle.transform.position, transform.rotation);
        anim.SetTrigger("Fire");
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(bulletSpeed, 0);
        yield return new WaitForSeconds(fireRate / 3);

        ps.Play();
        GameObject bullet3 = Instantiate(rifleBullet1, muzzle.transform.position, transform.rotation);
        anim.SetTrigger("Fire");
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.velocity = new Vector2(bulletSpeed, 0);
        StartCoroutine("Fire");
    }


    
}
