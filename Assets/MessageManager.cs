using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{

    public MessageContent messageCanvas;
    public Message[] msg;


    
   

    public void Start()
    {
        if (!LevelCheck(0) && PlayerPrefs.GetInt("ot0", 1) == 1)
        {
            messageCanvas.ShowMessage(msg[0]);
            PlayerPrefs.SetInt("ot0", 0);
        }
    }


    bool LevelCheck(int id)
    {
        int progress = PlayerPrefs.GetInt("levelProgress", 0);

        if(id < progress)
        {
            return true;
        } else
        {
            return false;
        }
    }

}
