using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByteAI : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float timePassed;
    public bool keepGoing = true;
    public ParticleSystem explode;

    public GameObject explosion;

    public GameObject body;
    public GameObject eye;

    public Collider2D[] results;

    public float explosionForce;


    IEnumerator Move()
    {

        yield return new WaitForSeconds(3f);
       anim.SetBool("JumpNow", true);
        yield return new WaitForSeconds(0.333f); // windup animation time
        anim.SetBool("JumpNow", false);
        rb.AddForce(new Vector2(2, 5), ForceMode2D.Impulse);
        anim.SetBool("SpringNow", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("SpringNow", false);
        yield return new WaitForSeconds(0.799f);//flight time
        anim.SetBool("LandNow", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("LandNow", false);

        if (keepGoing)
        {
            StartCoroutine("Move");
        }
    }

    void Start()
    {
        keepGoing = true;
        StartCoroutine("Move");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            keepGoing = false;
            StartCoroutine("Fuse");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Fuse()
    {
        anim.SetBool("FuseNow", true);
        yield return new WaitForSeconds(3f);
        explode.Play();
        eye.SetActive(false);
        body.SetActive(false);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f, ~7);
        for(int i=0; i < colliders.Length; i++)
        {
            
            if (colliders[i].GetComponent<Target>() != null)
            {
                Target target = colliders[i].GetComponent<Target>();
              //  Debug.Log(colliders[i].name + " has been hit with an explosion!");
                float distanceDamage = Vector2.Distance(colliders[i].transform.position, transform.position);
                target.TakeDamage(explosionForce + 10/distanceDamage);
            } 
        }
        

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
         
    }

    


 

}
