using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlock : MonoBehaviour
{
    public test player;
    public GameObject[] LockArea;
    public Text[] UpgradeCostText;
    public Notification Notif;
    public int[] UnlockCost;
    public void Start()
    {
        for (int i = 2; i <= 6; i++)
        {
            LockArea[i].SetActive(player.LockResource[i]);
            UpgradeCostText[i].text = (UnlockCost[i]).ToString();
        }
    }
    
    void Update()
    {
        
    }
    public void LockUnlock(int i)
    {
        int cost = player.reward[i] * 2;
        if (player.money >= cost)
        {

            if (!player.LockResource[i - 1])
            {
                player.money -= cost;
                player.LockResource[i] = false;
                Start();
            }
            else
                Notif.appear("Unlock previous resource!");

        }
        else
            Notif.appear("Nu ai destui bani");
    }

}
