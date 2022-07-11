using UnityEngine;
using UnityEngine.UI;

public class MessageContent : MonoBehaviour
{

    #region main

    public Text mainText;
    public Image button;
    public Image button_a;
    public Button[] buttons;
    public Text[] texts;
    public Image[] images;
    public GameObject MB;

    #endregion


    #region simple

    public Button buttonSimple;
    public GameObject simpleMB;

    #endregion


    #region shop

    public Text mainTextShop;
    public Text purchasedItemText;
    public Button purchase_e;
    public Button purchase_r;
    public Button exit_purchase;
    public Image icon;
    public int purchase_e_Val;
    public int purchase_r_Val;


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

    public float alpha;
    public float perc_t;

    public SaveAndLoad snl;
    public ShopManager shopManager;
   
    
    public void ShowMessage(Message msg)
    {
        

        gameObject.GetComponent<GraphicRaycaster>().enabled = true;
        switch (msg.type)
        {
            case "simple":
                buttons = new Button[1];             
                buttons[0] = buttonSimple;
                mainText.text = msg.content;
                texts = new Text[1];
                texts[0] = mainText;
                MB = simpleMB;
                break;


            case "shop":
                buttons = new Button[3];
                texts = new Text[2];
                images = new Image[1];
                purchase_e.transform.GetChild(0).GetComponent<Text>().text = msg.purchase_e_price.ToString();
                purchase_r.transform.GetChild(0).GetComponent<Text>().text = msg.purchase_r_price.ToString();

                if(msg.purchase_e_price > snl.data.player_essence)
                {
                    purchase_e.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                    purchase_e.interactable = false;
                } else
                {
                    purchase_e.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                    purchase_e.interactable = true;
                    purchase_e.onClick.RemoveAllListeners();
                    purchase_e.onClick.AddListener(() => shopManager.buyTroop(msg.troop_id, false));

                }

                if (msg.purchase_r_price > snl.data.player_rubies)
                {
                    purchase_r.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                    purchase_r.interactable = false;
                }
                else
                {
                    purchase_r.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                    purchase_r.interactable = true;
                    purchase_r.onClick.RemoveAllListeners();
                    purchase_r.onClick.AddListener(() => shopManager.buyTroop(msg.troop_id, true));

                }

                mainTextShop.text = msg.content;
                texts[0] = mainTextShop;
                texts[1] = purchasedItemText;
                images[0] = icon;
                images[0].sprite = msg.icon;
                purchasedItemText.text = msg.purchasedItem;
                MB = simpleMB;
                buttons[0] = purchase_e;
                buttons[1] = purchase_r;
                buttons[2] = exit_purchase;
                break;


        }
        desiredWidth = msg.boxWidth;
        desiredHeight = msg.boxHeight;
        

        c_timer = 0;
        m_timer = .35f;
        lerping = true;
    }


    public void lerpBack()
    {
        lerpingback = true;
    }

    void Buttons(string behaviour = "hide")
    {
        switch (behaviour)
        {
            case "hide":

                foreach (Button b in buttons)
                {

                    Image im = b.gameObject.GetComponent<Image>();
                    im.color = new Color(im.color.r, im.color.g, im.color.b, Mathf.Clamp(alpha, 0, 0.25f));

                    //changes the opacity of Text child object, like the price on items in the Shop.
                    if(b.gameObject.transform.GetChild(0).GetComponent<Text>() != null)
                    {
                        Text t = b.gameObject.transform.GetChild(0).GetComponent<Text>();
                        t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp(alpha, 0, 1));
                    }

                    //changes the opacity of Image child object, like the 'X' symbol in Shop Exit.
                    if (b.gameObject.transform.GetChild(0).GetComponent<Image>() != null)
                    {
                        Image im_ = b.gameObject.transform.GetChild(0).GetComponent<Image>();
                        im_.color = new Color(im_.color.r, im_.color.g, im_.color.b, Mathf.Clamp(alpha, 0, 0.25f));
                    }
                    

                }

                break;

            case "show":

                foreach (Button b in buttons)
                {
                   
                    Image im = b.gameObject.GetComponent<Image>();
                    im.color = new Color(im.color.r, im.color.g, im.color.b, Mathf.Clamp(perc_t, 0, 0.25f));

                    if (b.gameObject.transform.GetChild(0).GetComponent<Text>() != null)
                    {
                        Text t = b.gameObject.transform.GetChild(0).GetComponent<Text>();
                        t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp(perc_t, 0, 1));
                    }

                    if(b.gameObject.transform.GetChild(0).GetComponent<Image>() != null)
                    {
                        Image im_ = b.gameObject.transform.GetChild(0).GetComponent<Image>();
                        im_.color = new Color(im_.color.r, im_.color.g, im_.color.b, Mathf.Clamp(perc_t, 0, 0.25f));
                    }

                }

                break;

            default:
                Buttons("hide");
                break;
        }
    }

    void Texts(string behaviour = "hide")
    {
        switch (behaviour)
        {
            case "hide":

                foreach (Text t in texts)
                {
                 
                    t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp(alpha, 0, 1));
               
                }

                break;

            case "show":

                foreach (Text t in texts)
                {

                    t.color = new Color(t.color.r, t.color.g, t.color.b, Mathf.Clamp(perc_t, 0, 1));

                }

                break;

            default:
                Texts("hide");
                break;
        }
    }

    void Images(string behaviour = "hide")
    {
        switch (behaviour)
        {
            case "hide":

                foreach (Image i in images)
                {

                    i.color = new Color(i.color.r, i.color.g, i.color.b, Mathf.Clamp(alpha, 0, 1));

                }

                break;

            case "show":

                foreach (Image i in images)
                {

                    i.color = new Color(i.color.r, i.color.g, i.color.b, Mathf.Clamp(perc_t, 0, 1));

                }

                break;

            default:
                Images("hide");
                break;
        }
    }

    public void Update()
    {

        if (lerping)
        {
           
            if (c_timer < m_timer)
            {
                c_timer += Time.deltaTime;

                perc_t = c_timer / m_timer;
                perc_t = perc_t * perc_t * perc_t;
                Texts("show");

                float perc = c_timer / m_timer;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                currWidth = Mathf.Lerp(10, desiredWidth, perc);
                currHeight = Mathf.Lerp(10, desiredHeight, perc);
                MB.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);

                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Clamp(perc, 0, 0.75f));

                Buttons("show");
                Images("show");
               
           
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
                alpha = Mathf.Lerp(1, 0, perc_a);

                perc_t = c_timer / m_timer;
                perc_t = perc_t * perc_t * perc_t;
                Texts("hide");



                float perc = c_timer / m_timer;
                perc = 1 - Mathf.Pow(1 - perc, 4);
                currWidth = Mathf.Lerp(desiredWidth, 0, perc);
                currHeight = Mathf.Lerp(desiredHeight, 0, perc);
                MB.GetComponent<RectTransform>().sizeDelta = new Vector2(currWidth, currHeight);

                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Clamp(alpha, 0, 0.75f));


                Buttons("hide");
                Images("hide");

            }
            else
            {
                lerpingback = false;
                c_timer = 0;
                gameObject.GetComponent<GraphicRaycaster>().enabled = false;
                buttons = new Button[0];
                images = new Image[0];
                texts = new Text[0];
            }
        }



    }
  
}
