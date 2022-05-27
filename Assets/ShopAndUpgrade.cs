using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAndUpgrade : MonoBehaviour
{


    public GameObject mainCanvas;
    public GameObject snuCanvas;
    public GameObject shopBox;
    public GameObject upgradeBox;
    public void OpenShop() {

        mainCanvas.SetActive(false);
        snuCanvas.SetActive(true);      //shop n upgrade
        shopBox.SetActive(true);
    }

    public void OpenUpgrade()
    {
        mainCanvas.SetActive(false);
        snuCanvas.SetActive(true);      //shop n upgrade
        upgradeBox.SetActive(true);
    }

    public void closeSNU()
    {
        mainCanvas.SetActive(true);
        snuCanvas.SetActive(false);
        shopBox.SetActive(false);
        upgradeBox.SetActive(false);
    }
}