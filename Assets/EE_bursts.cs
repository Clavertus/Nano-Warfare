using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EE_bursts : MonoBehaviour
{

    public GameObject EEblob;
    Vector2 randomizedPos;
    public bool instantStart;

    void Start()
    {
        if(instantStart)
        StartCoroutine(Generate());
    }

    public IEnumerator Generate()
    {
        yield return new WaitForSeconds(Random.Range(3f, 6f)); 
        randomizedPos = new Vector2(Random.Range(-10f, 10f), Random.Range(-4f, 1f));
        Instantiate(EEblob, randomizedPos, transform.rotation);
        StartCoroutine(Generate());
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Ended && Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y)) != null)
            {
                if (Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y)).name == "TapCollider")
                {
                    Collider2D col = Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y));
                    col.transform.parent.gameObject.GetComponent<EE_blob>().pickup();
                    
                }

            }

        }
    }

}
