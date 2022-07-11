using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Message Box", menuName = "Message Box")]
public class Message : ScriptableObject
{
    [TextArea]
    public string content;

    //exclusive to shop type
    public string purchasedItem;
    public Sprite icon;
    public int purchase_e_price;
    public int purchase_r_price;
    public int troop_id;

    public float boxWidth = 350;
    public float boxHeight = 300;

    public string type = "simple";



    // simple, 

}
