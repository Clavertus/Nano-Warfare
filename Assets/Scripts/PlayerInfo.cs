using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int chronocredits;
    public int essence;


    public bool[] playerTroops;

    /*
    public int[] ByteUpgrades;         //damage, range, health
    public int[] KilobyteUpgrades;     //damage, range, health
    public int[] MegabyteUpgrades;     //damage, range, health
   
    public int[] SniperUpgrades;       //damage, health, firerate  
    public int[] CommanderUpgrades;    //damage, range, health, firerate
  
    public int[] BulkyUpgrades;        //damage, health, attackspeed
    public int[] DecimatorUpgrades;    //damage, health, attackspeed
    public int[] JuggernautUpgrades;   //damage, health, attackspeed
    public int[] AssassinUpgrades;     //damage, health, attackspeed

    */

     public int[] NanotrooperUpgrades;  //damage, range, health, firerate
     public int[] StingerUpgrades;      //damage, health, attackspeed

    void Awake()
    {
        playerTroops = new bool[10];
        NanotrooperUpgrades = new int[4];
        StingerUpgrades = new int[3];


        essence = PlayerPrefs.GetInt("essence", 0);
        chronocredits = PlayerPrefs.GetInt("cc", 0);
        AnalyzePlayerTroops(PlayerPrefs.GetString("troops", "0000000000"));
        AnalyzePlayerUpgrades(StingerUpgrades, PlayerPrefs.GetString("upgrades_stinger", "000"));
        AnalyzePlayerUpgrades(NanotrooperUpgrades, PlayerPrefs.GetString("upgrades_nanotrooper", "000"));
        

    }


    void AnalyzePlayerUpgrades(int[] upgrades, string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            upgrades[i] = (int)s[i];          
        }

    }


    void AnalyzePlayerTroops(string s)
    {
        for(int i = 0; i < s.Length; i++)
        {
           
            if(s[i] == '1')
            {
                playerTroops[i] = true;
            } else
            {
                playerTroops[i] = false;
            }
        }
    }


    void Update()
    {
        /*
        if (Input.GetKeyDown("z"))
        {
            StingerUpgrades[2] += 1; //attackspeed
        }

        if (Input.GetKeyDown("x"))
        {
            StingerUpgrades[0] += 1; //damage
        }

        if (Input.GetKeyDown("a"))
        {
            StingerUpgrades[1] += 1; //range
        }

        if (Input.GetKeyDown("r"))
        {
            StingerUpgrades[3] += 1; //fireRate
        }
        */
}

}
