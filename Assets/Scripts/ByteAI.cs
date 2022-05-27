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
    public GameObject hb;
    public GameObject antennae;
    public GameObject antennaebulb;

    public bool startFuse = false;
    public bool waiting = false;

    public Collider2D[] results;
    public Collider2D hitbox;

    public float explosionForce;            // -15x^2 + 100


    IEnumerator Move()
    {

        waiting = false;
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
        waiting = true;
        yield return new WaitForSeconds(3f);
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

    void Update()
    {

        results = Physics2D.OverlapCircleAll(transform.position, 1.5f, 1<<7);

        if (startFuse && !anim.GetBool("JumpNow") && !anim.GetBool("SpringNow") && !anim.GetBool("LandNow") && waiting)
        {
            keepGoing = false;
            StopCoroutine("Move");
            StartCoroutine("Fuse");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            startFuse = false;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            startFuse = true;
        }
    }

    IEnumerator Fuse()
    {
        anim.SetBool("FuseNow", true);
        yield return new WaitForSeconds(3f);
        explode.Play();
        eye.SetActive(false);
        hitbox.enabled = false;
        antennae.SetActive(false);
        antennaebulb.SetActive(false);
        body.SetActive(false);
        hb.SetActive(false);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f, 1<<7);
        
        for(int i=0; i < colliders.Length; i++)
        {
            
            if (colliders[i].transform.parent.GetComponent<Target>() != null)
            {
                if (colliders[i] != gameObject.GetComponent<Collider2D>())
                {
                    Target target = colliders[i].transform.parent.GetComponent<Target>();

                    //float distance = Vector2.Distance(colliders[i].transform.position, transform.position);
                    float distance = Vector2.Distance(new Vector2(colliders[i].transform.position.x, colliders[i].transform.position.y),
                        new Vector2(transform.position.x, transform.position.y)
                        );
                    target.TakeDamage((distance * distance) * -44.444f + 100);
                    Debug.Log((distance * distance) * -44.444f + 100);
                    
                }
            } 
        }
        

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
         
    }

    


 

}
