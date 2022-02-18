using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRifleBullet : MonoBehaviour
{
    public float impactForce;
    public ParticleSystem BulletHit;

    void Start()
    {
        GameObject GameManager = GameObject.FindGameObjectWithTag("gameManager");
        impactForce = GameManager.GetComponent<PlayerInfo>().NanotrooperUpgrades[0] * 7 + 20;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ally")
        {
            if (col.collider.transform.parent.GetComponent<Target>() != null)
            {
                col.collider.transform.parent.GetComponent<Target>().TakeDamage(impactForce);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                BulletHit.Play();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;



                Destroy(gameObject, .5f);
            }
            else
            {
               // Debug.Log
                 //   ("This object is not a Target");
            }
        }
    }
}
