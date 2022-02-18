using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public bool buttonHeld_l;
    public bool buttonHeld_r;

    public void press_l()
    {
        buttonHeld_l = true;
    }

    public void release_l()
    {
        buttonHeld_l = false;
    }

    public void press_r()
    {
        buttonHeld_r = true;
    }

    public void release_r()
    {
        buttonHeld_r = false;
    }

    void FixedUpdate()
    {


        if (buttonHeld_l)
        {
            CameraLeft();
        }


        if (buttonHeld_r)
        {
            CameraRight();
        }

    }

    public void CameraLeft()
    {
        if (transform.position.x > -3f)
        transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
    }


    public void CameraRight()
    {
        if(transform.position.x < 6)
        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

    }
}
