using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject currentTab;
    public Image[] tabIcons;
    public GameObject[] tabs;
    public GameObject lp;

    public void pickIcon(Image c)
    {
        foreach (Image i in tabIcons)
        {
            if (i != c)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, 0.35f);
            } else
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            }
        }
    }

    public void pickTab(GameObject c)
    {

        lp.SetActive(false);
        foreach (GameObject i in tabs)
        {
            if (i != c)
            {
                i.SetActive(false);
            }
            else
            {
                i.SetActive(true);
            }
        }
    }



    public void switchToTroops()
    {
        pickIcon(tabIcons[0]);
        pickTab(tabs[0]);
        
    }

    public void switchToPotions()
    {
        pickIcon(tabIcons[1]);
        pickTab(tabs[1]);
    }

    public void switchToWeapons()
    {
        pickIcon(tabIcons[2]);
        pickTab(tabs[2]);
    }

    public void switchToUtilities()
    {
        pickIcon(tabIcons[3]);
        pickTab(tabs[3]);
    }

    public void switchToPremium()
    {
        pickIcon(tabIcons[4]);
        pickTab(tabs[4]);

    }

}
