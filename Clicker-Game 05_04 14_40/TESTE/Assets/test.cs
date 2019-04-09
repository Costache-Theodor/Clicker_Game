using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] Sidekick_Cost;
    public int[] Sidekick_diamond_cost;
    public int[] Manager_Cost;
    public int[] Manager_diamond_cost;
    public Text[] sidekick_Cost_Text;
    public Text[] sidekick_diamonds_Text;
    public Text[] manager_Cost_Text;
    public Text[] manager_diamonds_Text;
    public Text[] Upgrade_cost;
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

    public Slider[] HP;



    public Button[] Upgr_avatars;
    public Image[] Sdk_avatars;
    public Button CLOSE_SIDEKICK;
    public DateTime[] timeOld = new DateTime[7];
    public Sprite NONE;
    public Notification NOTIFICATION;
    public sidekick replace_buttons;

    public float[] sideKick_time;
    public int[] value;
    public int[] maxValue;
    public int[] reward;
    public bool[] IsRunning;
    public int[] sideKick;/// 0 nici un sidekick, i= sidekickul i; sideKick[i]=-1  <>  a fost deschis sidekickul de carte el idin upgrade
    public bool[] isboughtSDK;
    public bool[] isboughtTCH;
    public int[] teachers_active;

    public float[] secPassed;

    public int diamonds;
    public int money;
    public Text Money;
    public Text Diamonds;
    public dailyquest Quest;

    public bool[] LockResource;

    public void on_click(int i)
    {
        if (!IsRunning[i])
            IsRunning[i] = true;
        value[i]++;
        if (value[i] == maxValue[i])
        {
            value[i] = 0;
            IsRunning[i] = false;
            money += reward[i];
            money_gathered+=reward[i];
            resources_finished[i]++;

            Quest.daily_resources();

        }
        if (sideKick[i] > 0)
        {
            secPassed[i] += 0.5f;
        }
        HP[i].maxValue = maxValue[i];
        HP[i].value = value[i];

    }

    private void Awake()
    {
        Load();
    }

    public void Start()
    {
        for( int i = 1; i <= 6; i++)
        {
            HP[i].maxValue = maxValue[i];
            HP[i].value = value[i];
            sidekick_Cost_Text[i].text = Sidekick_Cost[i].ToString();
            sidekick_diamonds_Text[i].text = Sidekick_diamond_cost[i].ToString();
            manager_diamonds_Text[i].text = Manager_diamond_cost[i].ToString();
            manager_Cost_Text[i].text = Manager_Cost[i].ToString();
            Upgrade_cost[i].text = (reward[i] * 2).ToString();
        }
        InvokeRepeating("decrease", 0.1f, 0.5f);
        Money.text = money.ToString();
        Diamonds.text = diamonds.ToString();
        

        //set sidekicks
        for (int i = 1; i <= 6; i++)
            if (sideKick[i] != 0)
                Upgr_avatars[i].image.sprite = Sdk_avatars[sideKick[i]].sprite;
            else
                Upgr_avatars[i].image.sprite = NONE;
        calculateTermination();
        
        // lock/unlocl resource
            
    }

    private void Update()
    {
        Money.text = money.ToString();
        Diamonds.text = diamonds.ToString();
    }

    public void Save()
    {
        SaveSystem.savePlayer(this);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        for(int i = 1; i <= 6; i++)
        {
            value[i] = data.value[i];
            maxValue[i] = data.maxValue[i];
            IsRunning[i] = data.IsRunning[i];
            reward[i] = data.reward[i];
            sideKick[i] = data.sideKick[i];
            timeOld[i] = data.timeOld[i];
            secPassed[i] = data.secPassed[i];
            isboughtSDK[i] = data.isboughtSDK[i];
            isboughtTCH[i] = data.isboughtTCH[i];

            LockResource[i] = data.LockResource[i];

            if (i <= 5)
                teachers_active[i] = data.teachers_active[i];

            resources_finished[i] = data.resources_finished[i];
            if (i <= 3)
                quests_running[i] = data.quests_running[i];
        }
        for (int i = 1; i <= 10; i++)
            quest_reward[i] = data.quest_reward[i];
        money = data.money;
        diamonds = data.diamonds;

        QuestStart = data.QuestStart;
        appfirst_start = data.appfirst_start;
        sidekicks_bought = data.sidekicks_bought;
        upgrades_bought = data.upgrades_bought;
        money_gathered = data.money_gathered;

    }

    void quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    private void OnApplicationQuit()
    {
        close_sidekick();
        for (int i = 1; i <= 6; i++)
            if (sideKick[i] > 0)
                timeOld[i] = System.DateTime.Now;

        Save();
    }

    void decrease()
    {
        for( int i = 1; i <= 6; i++)
        {
            if (IsRunning[i])
            {
                on_click(i);
               if( !IsRunning[i])
               {
                    if (sideKick[i] > 0)
                        if (secPassed[i] <= sideKick_time[sideKick[i]])
                            IsRunning[i] = true;
                        else
                            deactivateSDK(i);
               }
            }
                
        }
    }

    public void open_sidekick(int i)
    {
        sideKick[i] = -1;
    }

    public void close_sidekick()
    {
        for (int i = 1; i <= 6; i++)
            if (sideKick[i] == -1)
                sideKick[i] = 0;
    }

    public void buy_sidekick(int i)
    {
        for( int j = 1; j <= 6; j++)
            if (sideKick[j] == i)
            {
                NOTIFICATION.appearWithoutBuy("This Sidekick is already in use");
                return;
            }

        if (Sidekick_Cost[i] <= money)
        {
            for (int j = 1; j <= 6; j++)
            {
                if (sideKick[j] == -1)
                {
                    if (Upgr_avatars[j].image.sprite != NONE)
                    {
                        for(int k=1;k<=6;k++)
                            if (Upgr_avatars[j].image.sprite == Sdk_avatars[k].sprite)
                            {
                                secPassed[k] = 0;
                                 replace_buttons.replaceButtonSDK(k, false);
                                 break;
                            }
                    }
                    sideKick[j] = i;
                    Upgr_avatars[j].image.sprite = Sdk_avatars[i].sprite;
                    timeOld[j] = System.DateTime.Now;
                    secPassed[j] = 0;
                    IsRunning[j] = true;
                    replace_buttons.set_active(i, false);
                    CLOSE_SIDEKICK.onClick.Invoke();
                    
                }
            }

            money -= Sidekick_Cost[i];
            replace_buttons.replaceButtonSDK(i, true);
            sidekicks_bought++;
            Quest.daily_sidekick();
        }
        else
            NOTIFICATION.appear("Nu ai suficienti bani SARACULE");
    }
    //Buy diferit pt diamonds
    public void buy_sidekick_diamonds(int i)
    {
        for (int j = 1; j <= 6; j++)
            if (sideKick[j] == i)
            {
                NOTIFICATION.appearWithoutBuy("This Sidekick is already in use");
                return;
            }
        if (Sidekick_diamond_cost[i] < diamonds)
        {
            for (int j = 1; j <= 6; j++)
            {
                if (sideKick[j] == -1)
                {
                    if (Upgr_avatars[j].image.sprite != NONE)
                    {
                        for (int k = 1; k <= 6; k++)
                            if (Upgr_avatars[j].image.sprite == Sdk_avatars[k].sprite)
                            {
                                secPassed[k] = 0;
                                replace_buttons.replaceButtonSDK(k, false);
                                break;
                            }
                    }
                    sideKick[j] = i;
                    Upgr_avatars[j].image.sprite = Sdk_avatars[i].sprite;
                    timeOld[j] = System.DateTime.Now;
                    secPassed[j] = 0;
                    IsRunning[j] = true;
                    replace_buttons.set_active(i, false);
                    CLOSE_SIDEKICK.onClick.Invoke();
                }
            }
            diamonds -= Sidekick_diamond_cost[i];

            replace_buttons.replaceButtonSDK(i, true);
            sidekicks_bought++;
            Quest.daily_sidekick();

        }
        else
            NOTIFICATION.appear("Nu ai suficiente DIAMANTE SARACULE");
    }

    public void equip (int i)
    {

        for (int j = 1; j <= 6; j++)
        {
            if (sideKick[j] == -1)
            {
                sideKick[j] = i;
                Upgr_avatars[j].image.sprite = Sdk_avatars[i].sprite;
                timeOld[j] = System.DateTime.Now;
                secPassed[j] = 0;
                IsRunning[j] = true;
                replace_buttons.set_active(i, false);
                return;
            }
        }

        
    }

    public void calculateTermination()
    {
        for( int i = 1; i <= 6; i++)
        {
            if (sideKick[i]!=0)
            {
                Debug.Log("I:" + i + "secPased: "+secPassed[i]);
                Debug.Log(" timeOld:" + timeOld[i]);
                if (secPassed[i]+(System.DateTime.Now-timeOld[i]).TotalSeconds>=sideKick_time[sideKick[i]])
                {
                    Debug.Log("Sidekick " + i + " terminat Reward: " + ((int)(sideKick_time[sideKick[i]] - secPassed[i]) / maxValue[i]) * reward[i]);
                    //s-a TERMINAT sidekick
                    money += ((int)(sideKick_time[sideKick[i]] - secPassed[i]) / maxValue[i]) * reward[i];
                    deactivateSDK(i);
                }
                else 
                if((int)((System.DateTime.Now-timeOld[i]).TotalSeconds) + value[i] >= maxValue[i])
                {
                    Debug.Log("Slidere incarcate complet:" + ((int)(System.DateTime.Now - timeOld[i]).TotalSeconds + value[i]) / maxValue[i]);
                    Debug.Log("Extra Add value[i]: " + ((int)((System.DateTime.Now - timeOld[i]).TotalSeconds) + value[i]) % maxValue[i]);
                    money += ((int)(System.DateTime.Now - timeOld[i]).TotalSeconds + value[i])/ maxValue[i] * reward[i];
                    secPassed[i] += (float)(System.DateTime.Now - timeOld[i]).TotalSeconds;
                    value[i] = ((int)((System.DateTime.Now - timeOld[i]).TotalSeconds) + value[i]) % maxValue[i];
                }
                else
                {
                    Debug.Log("nici un slider incarcat complet; Val ADD:"+(int)((System.DateTime.Now - timeOld[i]).TotalSeconds));
                    value[i] += (int)((System.DateTime.Now - timeOld[i]).TotalSeconds);
                    secPassed[i] +=(float) (System.DateTime.Now - timeOld[i]).TotalSeconds;
                }
                    
            }
        }
    }

    public void deactivateSDK( int i)
    {
        secPassed[i] = 0f;
        replace_buttons.replaceButtonSDK(sideKick[i], false);
        sideKick[i] = 0;
        Upgr_avatars[i].image.sprite = NONE;
        
    }

    //Astea doua sunt doar functiile de cumparat
    public void buy_manager_money(int i)
    {
        if (money >= Manager_Cost[i])
        {
            activateManager(i);
            money -= Manager_Cost[i];
        }
        else
            NOTIFICATION.appear("Nu ai suficienta valuta SARACULE");
    }
    public void buy_manager_diamonds(int i)
    {
        if (diamonds >= Manager_diamond_cost[i])
        {
            diamonds -= Manager_diamond_cost[i];
            activateManager(i);
        }
        else
            NOTIFICATION.appear("Nu ai suficiente diamante SARACULE");
    }

    public void activateManager(int i)
    {
        switch (i)
        {
            case 1:
                
                Debug.Log("Manager1ACT");
                for (int j = 1; j <= 6; j++)
                    reward[j]++;
                break;
                
            case 2:
                
                reward[1] += 5;
                break;
                
            case 3:
                
                reward[2] += 10;
                break;
                
            case 4:

                reward[3] += 15;
                break;

            case 5:

                Debug.Log("Manager5ACT");
                reward[4] += 20;
                break;

            case 6:

                reward[5] += 25;
                break;

            case 7:

                reward[6] += 30;
                break;

            case 8:

                for (int j = 1; j <= 7; j++)
                    if (maxValue[j] >= 1)
                        maxValue[j]--;
                break;

            case 9:

                maxValue[1] = 1;
                break;

            case 10:

                diamonds += 200;
                Manager_Cost[i] += 500;
                Manager_diamond_cost[i] += 30;
                break;

        }
    }
    public void Upgrade_res(int i)
    {
        if (money >= reward[i] * 2)
        {
            money -= reward[i] * 2;
            reward[i] += 10;
            if (maxValue[i] > 1)
                maxValue[i]--;
            Upgrade_cost[i].text = (reward[i] * 2).ToString();
            upgrades_bought++;
            Quest.daily_upgrade();
        }
        else
            NOTIFICATION.appear("Resurse insuficiente!");
    }



    public void cheat()
    {
        money += 1000;
    }
}

