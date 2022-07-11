using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    //basic currency
    public int player_essence;

    //premium currency
    public int player_rubies;

    //x-length word, 1 for troop owned, 0 for troop not owned
    public string player_troops;

    //value from 0-x, x being the number of levels
    public int player_progress;

    public string player_abilities;

    public bool extraTroopSlot;

    public bool extraAbilitySlot;









}
