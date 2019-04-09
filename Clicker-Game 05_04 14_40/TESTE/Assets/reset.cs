using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class reset : MonoBehaviour
{
    public test player;

    public Text CostText;
    private int cost_reset = 1000;
    public unlock unlk;
    public sidekick sdk;
    private void Start()
    {
        CostText.text = "Upgrade Student\nCost: " + cost_reset.ToString();
    }
    public void clickReset()
    {
        if (player.money >= cost_reset)
        {
            for (int i = 1; i <= 6; i++)
            {
                player.timeOld[i] = System.DateTime.Now;
                player.value[i] = 0;
                player.maxValue[i] = 10;
                player.IsRunning[i] = false;
                player.reward[i] = i * 100;
                player.sideKick[i] = 0;
                player.secPassed[i] = 0;
                player.isboughtSDK[i] = false;
                player.LockResource[i] = true;
                player.resources_finished[i] = 0;
                if (i <= 3)
                    player.quests_running[i] = 0;
            }
            player.LockResource[1] = false;
            for (int i = 1; i <= 10; i++)
                player.quest_reward[i] = i * 100;
            player.money = 1000;
            player.QuestStart = System.DateTime.Now;
            player.appfirst_start = false;
            player.sidekicks_bought = 0;
            player.upgrades_bought = 0;
            player.money_gathered = 0;
            unlk.Start();
            sdk.Start();
            player.Start();
        }
        else
            player.NOTIFICATION.appear("Not enough money to promote!");
    }
}
