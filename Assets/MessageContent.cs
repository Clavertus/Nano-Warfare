using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageContent : MonoBehaviour
{

    public Text mainText;
    public Message msg;

    public Image panel;
    public Image button;
    public Image button_a;

    public bool lerping;
    public bool lerpingback;

    public float currWidth;
    public float currHeight;

    public float desiredWidth;
    public float desiredHeight;

    public float c_timer;
    public float m_timer;
   
    
    void Start()
    {

        desiredWidth = msg.boxWidth;
        desiredHeight = msg.boxHeight;
        mainText.text = msg.content;
        lerping = true;
        c_timer = 0;
        m_timer = .65f;
    }

    public void Update()
    {

        //message box resize
        if (lerping)
        {
           
            if (c_timer < m_timer)
            {
                c_timer += Time.deltaTime;

                float perc_t = c_timer / m_timer;
                perc_t = perc_t * perc_t * perc_t;
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, perc_t);
                
                float perc = c_timer / m_timer;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                currWidth = Mathf.Lerp(10, desiredWidth, perc);
                currHeight = Mathf.Lerp(10, desiredHeight, perc);
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);

                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Clamp(perc, 0, 0.75f));

                button.color = new Color(button.color.r, button.color.g, button.color.b, Mathf.Clamp(perc_t, 0, 0.25f));
                button_a.color = new Color(button_a.color.r, button_a.color.g, button_a.color.b, Mathf.Clamp(perc_t, 0, 0.25f));
            } else
            {
                lerping = false;
                c_timer = 0;
            }
        }

        if (lerpingback)
        {
            if (c_timer < m_timer)
            {
                c_timer += Time.deltaTime;

                

                float perc_t = c_timer / m_timer;
                perc_t = perc_t * perc_t * perc_t;
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, perc_t);

                float perc = c_timer / m_timer;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                currWidth = Mathf.Lerp(desiredWidth, 0, perc);
                currHeight = Mathf.Lerp(desiredHeight, 0, perc);
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);

                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Clamp(perc, 0, 0.75f));

                button.color = new Color(button.color.r, button.color.g, button.color.b, Mathf.Clamp(perc_t, 0, 0.25f));
                button_a.color = new Color(button_a.color.r, button_a.color.g, button_a.color.b, Mathf.Clamp(perc_t, 0, 0.25f));
            }
            else
            {
                lerping = false;
            }
        }



    }
  
}
