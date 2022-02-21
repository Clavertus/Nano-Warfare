using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Message Box", menuName = "Message Box")]
public class Message : ScriptableObject
{
    [TextArea]
    public string content;

    public float boxWidth = 350;
    public float boxHeight = 300;

}
