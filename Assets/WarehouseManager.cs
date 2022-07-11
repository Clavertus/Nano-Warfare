using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseManager : MonoBehaviour
{
    public SaveAndLoad snl;

    public Image[] loadoutTroops;
    public Image[] loadoutAbilities;

    public GameObject[] inventoryTroops;
    public GameObject[] inventoryAbilities;

    public GameObject troopLock;
    public GameObject abilityLock;

    public int troopSlotsLeft; 
    public int abilitySlotsLeft; 

    public int slotSize;

    void Start()
    {
        PlayerData data_mem = snl.dh.LoadData();

        if(data_mem.extraTroopSlot == true)
        {
            troopLock.SetActive(false);
            troopSlotsLeft = 6;
        }
        else
        {
            troopLock.SetActive(true);
            troopSlotsLeft = 5;
        }

        if(data_mem.extraAbilitySlot == true)
        {
            abilityLock.SetActive(false);
            abilitySlotsLeft = 2;
        }
        else
        {
            abilityLock.SetActive(true);
            abilitySlotsLeft = 1;
        }



        char[] c = data_mem.player_troops.ToCharArray();

        for(int i = 0; i < c.Length; i++)
        {
            if(c[i] == '1')
            {
                inventoryTroops[i].SetActive(true);
            } else
            {
                inventoryTroops[i].SetActive(false);
            }
        }

        char[] a = data_mem.player_abilities.ToCharArray();

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == '1')
            {
                inventoryAbilities[i].SetActive(true);
            }
            else
            {
                inventoryAbilities[i].SetActive(false);
            }
        }

    }

    void SwapTroops(int id)
    {
        if (troopSlotsLeft == 0)
        {
            return;
        } else
        {
            
        }
    }
}
