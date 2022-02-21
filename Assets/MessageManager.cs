using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{

    public MessageContent messageCanvas;
    public Message[] msg;
    

    public void OnEnable()
    {
        messageCanvas.ShowMessage(msg[0]);
    }
        
}
