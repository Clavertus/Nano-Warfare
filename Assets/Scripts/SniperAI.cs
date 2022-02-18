using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAI : MonoBehaviour
{
    public Animator anim;
    public bool zoomed = false;
    public bool walk;

    public bool isShooting;

    public float range;


    public GameObject rifleBullet1;
    public GameObject muzzle;

    public GameObject GameManager;
    public int[] upgrades;


    public float bulletSpeed;
    public float fireRate = 1;

    public ParticleSystem ps;

    public Collider2D[] enemiesInRange;
    public Collider2D[] alliesInRange;


    void Start()
    {

        bulletSpeed = 20;

        GameManager = GameObject.FindGameObjectWithTag("gameManager");
        upgrades = GameManager.GetComponent<PlayerInfo>().SniperUpgrades;
        Target target = gameObject.GetComponent<Target>();


        target.maxHP = upgrades[1] * 20 + 150;   //base HP 200, 30 for each level

        switch (upgrades[2])    //fireRate
        {
            case 1:
                fireRate = 3.5f;
                break;

            case 2:
                fireRate = 3.3f;
                break;

            case 3:
                fireRate = 3.1f;
                break;

            case 4:
                fireRate = 2.9f;
                break;

            case 5:
                fireRate = 2.7f;
                break;

            case 6:
                fireRate = 2.5f;
                break;

            case 7:
                fireRate = 2.3f;
                break;

            case 8:
                fireRate = 2.1f;
                break;

            case 9:
                fireRate = 1.9f;
                break;

            case 10:
                fireRate = 1.7f;
                break;

            default:
                fireRate = 3.7f;
                break;
        }   //fireRate

        range = 8;

        anim.SetTrigger("GoIdle");

    }


    void Update()
    {
        enemiesInRange = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y + 1),
            new Vector2(transform.position.x + range, transform.position.y - 1), 1 << 7);

        if(enemiesInRange.Length > 0)
        {
            if (!isShooting)
            {
                anim.SetTrigger("GoReady");
                StartCoroutine("Fire");
                isShooting = true;
            }
        } else
        {
            if (isShooting)
            {
                anim.SetTrigger("GoIdle");
                StopCoroutine("Fire");
                isShooting = false;
            }
        }
    }
        



       

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(fireRate);
        ps.Play();
        GameObject bullet = Instantiate(rifleBullet1, muzzle.transform.position, transform.rotation);
        anim.SetTrigger("SniperFire");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletSpeed, 0);
        StartCoroutine("Fire");
    }



}
