using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{

    public Text damageText;
    

    public void TextSpawn(float dmg, GameObject victim)
    {
        float yOffset = Random.Range(0.35f, 0.45f);
        float xOffset = Random.Range(-.15f, .3f);
        
        Text text = Instantiate(damageText, new Vector3(victim.transform.position.x + xOffset, victim.transform.position.y + yOffset, 0), transform.rotation).GetComponent<Text>();
        text.transform.parent = gameObject.transform;
        text.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        text.text = dmg.ToString();
    }
}
