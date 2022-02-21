using UnityEngine;
using UnityEngine.UI;

public class MessageContent : MonoBehaviour
{

    #region main

    public Text mainText;
    public Image button;
    public Image button_a;
    public GameObject MB;

    #endregion


    #region simple

    public Text mainTextSimple;
    public Image buttonSimple;
    public Image buttonSimple_a;
    public GameObject simpleMB;

    #endregion

    #region reward

    public Text mainTextReward;
    public Image buttonReward;
    public Image buttonReward_a;
    public GameObject rewardMB;

    #endregion


    public Image panel;
   

    public bool lerping;
    public bool lerpingback;

    public float currWidth;
    public float currHeight;

    public float desiredWidth;
    public float desiredHeight;

    public float c_timer;
    public float m_timer;
   
    
    public void ShowMessage(Message msg)
    {
        switch (msg.type)
        {
            case "simple":
                mainText = mainTextSimple;
                button = buttonSimple;
                button_a = buttonSimple_a;
                MB = simpleMB;
                break;

            case "reward":
                mainText = mainTextReward;
                button = buttonReward;
                button_a = buttonReward_a;
                break;


        }
        desiredWidth = msg.boxWidth;
        desiredHeight = msg.boxHeight;
        mainText.text = msg.content;

        c_timer = 0;
        m_timer = .65f;
        lerping = true;
    }


    public void lerpBack()
    {
        lerpingback = true;
    }

    public void Update()
    {

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
                MB.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);

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


                float perc_a = c_timer / m_timer;
                perc_a = 1 - Mathf.Pow(2, -10 * perc_a);
                float alpha = Mathf.Lerp(1, 0, perc_a);

                float perc_t = c_timer / m_timer;
                perc_t = perc_t * perc_t * perc_t;
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, alpha);



                float perc = c_timer / m_timer;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                currWidth = Mathf.Lerp(desiredWidth, 0, perc);
                currHeight = Mathf.Lerp(desiredHeight, 0, perc);
                MB.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);

                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Clamp(alpha, 0, 0.75f));

                button.color = new Color(button.color.r, button.color.g, button.color.b, Mathf.Clamp(alpha, 0, 0.25f));
                button_a.color = new Color(button_a.color.r, button_a.color.g, button_a.color.b, Mathf.Clamp(alpha, 0, 0.25f));
            }
            else
            {
                lerpingback = false;
                c_timer = 0;
            }
        }



    }
  
}
