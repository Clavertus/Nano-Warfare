using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public DataHandler dh;
    public PlayerData data;

    void Start()
    {
        data = dh.LoadData();
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            dh.SaveData(data);
        }

        if (Input.GetKeyDown("l"))
        {
            data = dh.LoadData();
        }
    }
}
