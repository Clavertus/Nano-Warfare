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

    public ParticleSystem celebrate;

    public SaveAndLoad snl;
    public Text currEssence;
    public Text currRubies;

    public Text text_stingerPrice;
    public Text text_nanoPrice;
    public Text text_bytePrice;
    public Text text_commanderPrice;
    public Text text_sdronePrice;

    public int price_stinger = 10;
    public int price_nano = 20;
    public int price_byte = 1000;
    public int price_commander = 3000;
    public int price_sdrone = 10000;

    public int[] prices;
    public int[] prices_premium;

    public Button button_stinger;
    public Button button_nano;
    public Button button_byte;
    public Button button_commander;
    public Button button_sdrone;

    public MessageContent msgContent;
    public Message ownedMsg;

    public Color ownedCol = new Color(0.278f, 1, 0.435f);
    public Color notEnoughCol = new Color(1, 0.443f, 0.411f);
    public Color essenceCol = new Color(0, 0.58f, 1);

    public Button bt; 

    public Button.ButtonClickedEvent msgOwned;

    public void OnEnable()
    {
        UpdateCurrencies();
        UpdateTroops(snl.data.player_troops);
        
        
    }
 

    void UpdateCurrencies()
    {
        currEssence.text = snl.data.player_essence.ToString();
        currRubies.text = snl.data.player_rubies.ToString();
    }

    void UpdateTroops(string seed)
    {
        for(int i = 0; i < seed.Length; i++)
        {
            switch (i)
            {
                case 0: //stinger
                    if(seed[i] == '0')
                    {
                        text_stingerPrice.text = prices[i].ToString();
                        if(snl.data.player_essence >= prices[i])
                        {
                            text_stingerPrice.color = new Color(essenceCol.r, essenceCol.g, essenceCol.b, text_stingerPrice.color.a);
                        } else
                        {
                            text_stingerPrice.color = new Color(notEnoughCol.r, notEnoughCol.g, notEnoughCol.b, text_stingerPrice.color.a);
                        }
                    }

                    if(seed[i] == '1')
                    {
                        text_stingerPrice.text = "OWNED";
                        text_stingerPrice.color = new Color(ownedCol.r, ownedCol.g, ownedCol.b, text_stingerPrice.color.a);
                        button_stinger.onClick = msgOwned;
                    }
                    break;

                case 1: //nanotrooper
                    if (seed[i] == '0')
                    {
                        text_nanoPrice.text = prices[i].ToString();
                        if (snl.data.player_essence >= prices[i])
                        {
                            text_nanoPrice.color = new Color(essenceCol.r, essenceCol.g, essenceCol.b, text_nanoPrice.color.a);

                        }
                        else
                        {
                            text_nanoPrice.color = new Color(notEnoughCol.r, notEnoughCol.g, notEnoughCol.b, text_nanoPrice.color.a);
                        }
                    }

                    if (seed[i] == '1')
                    {
                        text_nanoPrice.text = "OWNED";
                        text_nanoPrice.color = new Color(ownedCol.r, ownedCol.g, ownedCol.b, text_nanoPrice.color.a);
                        button_nano.onClick = msgOwned;

                    }
                    break;

                case 2: //byte
                    if (seed[i] == '0')
                    {
                        text_bytePrice.text = prices[i].ToString();
                        if (snl.data.player_essence >= prices[i])
                        {
                            text_bytePrice.color = new Color(essenceCol.r, essenceCol.g, essenceCol.b, text_bytePrice.color.a);

                        }
                        else
                        {
                            text_bytePrice.color = new Color(notEnoughCol.r, notEnoughCol.g, notEnoughCol.b, text_bytePrice.color.a);
                        }
                    }

                    if (seed[i] == '1')
                    {
                        text_bytePrice.text = "OWNED";
                        text_bytePrice.color = new Color(ownedCol.r, ownedCol.g, ownedCol.b, text_bytePrice.color.a);
                        button_byte.onClick = msgOwned;

                    }
                    break;

                case 3: //commander
                    if (seed[i] == '0')
                    {
                        text_commanderPrice.text = prices[i].ToString();
                        if (snl.data.player_essence >= prices[i])
                        {
                            text_commanderPrice.color = new Color(essenceCol.r, essenceCol.g, essenceCol.b, text_commanderPrice.color.a);

                        }
                        else
                        {
                            text_commanderPrice.color = new Color(notEnoughCol.r, notEnoughCol.g, notEnoughCol.b, text_commanderPrice.color.a);
                        }
                    }

                    if (seed[i] == '1')
                    {
                        text_commanderPrice.text = "OWNED";
                        text_commanderPrice.color = new Color(ownedCol.r, ownedCol.g, ownedCol.b, text_commanderPrice.color.a);
                        button_commander.onClick = msgOwned;

                    }
                    break;

                case 4: //scouting drone
                    if (seed[i] == '0')
                    {
                        text_sdronePrice.text = prices[i].ToString();
                        if (snl.data.player_essence >= prices[i])
                        {
                            text_sdronePrice.color = new Color(essenceCol.r, essenceCol.g, essenceCol.b, text_sdronePrice.color.a);

                        }
                        else
                        {
                            text_sdronePrice.color = new Color(notEnoughCol.r, notEnoughCol.g, notEnoughCol.b, text_sdronePrice.color.a);
                        }

                    }

                    if (seed[i] == '1')
                    {
                        text_sdronePrice.text = "OWNED";
                        text_sdronePrice.color = new Color(ownedCol.r, ownedCol.g, ownedCol.b, text_sdronePrice.color.a);
                        button_sdrone.onClick = msgOwned;

                    }
                    break;

            }
        }
    }

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


    public void buyTroop(int id, bool premium)
    {
        PlayerData data_mem = snl.dh.LoadData();
        char[] c = data_mem.player_troops.ToCharArray();
        c[id] = '1';
        data_mem.player_troops = new string(c);
        if(premium == false)
        {
            data_mem.player_essence -= prices[id];
        } else
        {
            data_mem.player_rubies -= prices_premium[id];
        }
        snl.dh.SaveData(data_mem);
        snl.data = snl.dh.LoadData();
        UpdateCurrencies();
        UpdateTroops(snl.data.player_troops);
        celebrate.Play();
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
