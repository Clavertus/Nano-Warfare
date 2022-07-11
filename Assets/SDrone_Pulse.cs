using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDrone_Pulse : MonoBehaviour
{

    public float newX;
    public float newY;
    public Rigidbody2D rb;
    public bool fire;

    public GameObject[] ps;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        newY = 0.04f;
        Destroy(gameObject, 1f);

        if (fire)
        {
            foreach(GameObject p in ps)
            {
                p.SetActive(true);
            }
        }


    }

    void Update()
    {
        newX += Time.deltaTime;
        newY += Time.deltaTime / 10;
        transform.localScale = new Vector3(newX, newY, transform.localScale.z);

        rb.velocity = new Vector2(0, -700 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            if(col.gameObject.transform.parent.GetComponent<Target>() != null)
            {
                Target target = col.gameObject.transform.parent.GetComponent<Target>();
                target.TakeDamage(10f);
            }
        }
    }
}
