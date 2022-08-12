using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextBehaviour : MonoBehaviour
{

    public float life;
    public float maxLife;
    public float result;
    public float perc;

    void Start()
    {
        maxLife = 1f;
        life = maxLife;
    }

    void Update()
    {
      

        if(life > 0)
        {
            life -= Time.deltaTime;
            float perc = life / maxLife;
         //   perc = 1 - Mathf.Pow(1 - perc, 5);
            result = Mathf.Lerp(life, maxLife, perc);
            Debug.Log("Perc: " + perc + "   Result: " + result);
        }


          gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x,
            gameObject.GetComponent<RectTransform>().anchoredPosition.y + result);

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
