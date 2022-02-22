using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{

    public Text[] mb_texts;
    public Image[] mb_imgs;

    public GameObject MB;

    public float currWidth = 600;
    public float currHeight = 200;

    public bool lerping;
    public float c_timer;
    public float m_timer = 0.65f;

    public void show()
    {
        lerping = true;
    }

    void Update()
    {
        if (lerping)
        {
            if (c_timer < m_timer)
            {
                c_timer += Time.deltaTime;

                float perc_t = c_timer / m_timer;
                perc_t = perc_t * perc_t * perc_t;

                for(int i = 0; i < mb_texts.Length; i++)
                {
                    mb_texts[i].color = new Color(mb_texts[i].color.r, mb_texts[i].color.g, mb_texts[i].color.b, perc_t);
                }

                for (int i = 0; i < mb_imgs.Length; i++)
                {
                    mb_imgs[i].color = new Color(mb_imgs[i].color.r, mb_imgs[i].color.g, mb_imgs[i].color.b, Mathf.Clamp(perc_t, 0, 0.25f));
                }

                float perc = c_timer / m_timer;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                currWidth = Mathf.Lerp(0, 600, perc);
                currHeight = Mathf.Lerp(0, 200, perc);
                MB.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);


                
              
            }
            else
            {
                lerping = false;
                c_timer = 0;
            }
        }
    }


}
