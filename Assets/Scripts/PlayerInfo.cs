using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int chronocredits;
    public int gold;

    public bool hasByte;
    public bool hasKilobyte;
    public bool hasMegabyte;
    public bool hasNanotrooper;
    public bool hasSniper;
    public bool hasCommander;
    public bool hasStinger;
    public bool hasBulky;
    public bool hasDecimator;
    public bool hasJuggernaut;
    public bool hasAssassin;

    public int[] ByteUpgrades;         //damage, range, health
    public int[] KilobyteUpgrades;     //damage, range, health
    public int[] MegabyteUpgrades;     //damage, range, health
    public int[] NanotrooperUpgrades;  //damage, range, health, firerate
    public int[] SniperUpgrades;       //damage, health, firerate  
    public int[] CommanderUpgrades;    //damage, range, health, firerate
    public int[] StingerUpgrades;      //damage, health, attackspeed
    public int[] BulkyUpgrades;        //damage, health, attackspeed
    public int[] DecimatorUpgrades;    //damage, health, attackspeed
    public int[] JuggernautUpgrades;   //damage, health, attackspeed
    public int[] AssassinUpgrades;     //damage, health, attackspeed

    void Awake()
    {
        NanotrooperUpgrades = new int[4];
        CommanderUpgrades = new int[4];
        SniperUpgrades = new int[3];
        StingerUpgrades = new int[3];
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
