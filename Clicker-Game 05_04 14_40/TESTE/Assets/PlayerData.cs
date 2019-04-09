using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public int[] value;
    public int[] maxValue;
    public int[] reward;
    public bool[] IsRunning;
    public int[] sideKick;
    
    public float[] secPassed;
    public DateTime[] timeOld;
    public int money;
    public int diamonds;
    public int[] teachers_active;

    public bool[] isboughtSDK;
    public bool[] isboughtTCH;

    public bool[] LockResource;

    /// QUEST
    public DateTime QuestStart;
    public bool appfirst_start;
    public int[] quests_running;
    public int[] resources_finished;
    public int sidekicks_bought;
    public int upgrades_bought;
    public int money_gathered;
    public int[] quest_reward;
    ///QUEST

    private void init()
    {
        value = new int[7];
        maxValue = new int[7];
        IsRunning = new bool[7];
        reward = new int[7];
        timeOld = new DateTime[7];
        sideKick = new int[7];
        secPassed = new float[7];
        isboughtSDK = new bool[7];
        isboughtTCH = new bool[7];
        teachers_active = new int[6];

        quests_running = new int[4];
        quest_reward = new int[11];
        resources_finished = new int[7];
        LockResource = new bool[7];
    }
    public PlayerData(test player)
    {
        init();
        for ( int i = 1; i <= 6; i++)
        {
            value[i] = player.value[i];
            maxValue[i] = player.maxValue[i];
            IsRunning[i] = player.IsRunning[i];
            reward[i] = player.reward[i];
            sideKick[i] = player.sideKick[i];
            timeOld[i] = player.timeOld[i];
            secPassed[i] = player.secPassed[i];
            isboughtSDK[i] = player.isboughtSDK[i];
            isboughtTCH[i] = player.isboughtTCH[i];

            LockResource[i] = player.LockResource[i];

            resources_finished[i] = player.resources_finished[i];
            if (i <= 3)
                quests_running[i] = player.quests_running[i];
            if (i <= 5)
                teachers_active[i] = player.teachers_active[i];
        }
        for (int i = 1; i <= 10; i++)
            quest_reward[i] = player.quest_reward[i];

        money = player.money;
        diamonds = player.diamonds;

        QuestStart = player.QuestStart;

        
        appfirst_start = player.appfirst_start;
        sidekicks_bought =player.sidekicks_bought;
        upgrades_bought=player.upgrades_bought;
        money_gathered=player.money_gathered;
}
    public PlayerData()
    {
        init();
        for (int i = 1; i <= 6; i++)
        {
            timeOld[i] = System.DateTime.Now;
            value[i] = 0;
            maxValue[i] = 10;
            IsRunning[i] = false;
            reward[i] = i * 100;
            sideKick[i] = 0;
            secPassed[i] = 0;
            isboughtSDK[i] = false;
            isboughtTCH[i] = false;
            LockResource[i] = true;
            if (i <= 5)
                teachers_active[i] = 0;
            resources_finished[i] =0;
            if (i <= 3)
                quests_running[i] =0;
        }
        LockResource[1] = false;
        for (int i = 1; i <= 10; i++)
            quest_reward[i] =i*100;
        money = 1000;
        diamonds = 500;
        QuestStart = System.DateTime.Now;
        appfirst_start = false;
        sidekicks_bought = 0;
        upgrades_bought = 0;
        money_gathered = 0;
    }
   
}

