using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class dailyquest : MonoBehaviour
{
    public Text[] quest_description;
    public Text[] quest_complete;
    public Sprite[] Resources;
    public Image[] Avatar;
    public GameObject[] Task;
    public GameObject Noquest;
    private Text NoQuestText;
    private DateTime temp;
    public test p;
    private void Start()
    {
        give_daily_quest();
        NoQuestText = Noquest.GetComponent<Text>();
    }
    private void Update()
    {
        //temp = p.QuestStart.AddDays(1) - System.DateTime.Now;

        NoQuestText.text = "Next Daily quests in:\n" +(p.QuestStart.AddDays(1) - System.DateTime.Now).Hours.ToString()+":"
                                                     +(p.QuestStart.AddDays(1) - System.DateTime.Now).Minutes.ToString()+":"
                                                     +((int)(p.QuestStart.AddDays(1) - System.DateTime.Now).Seconds).ToString();
        if ((p.appfirst_start == false) || ((System.DateTime.Now - p.QuestStart).Days >= 1))
            give_daily_quest();
       
    }
    public void give_daily_quest()
    {
            
        if ((p.appfirst_start == false) || ((System.DateTime.Now- p.QuestStart).Days >= 1))
        {
            int random;
            p.QuestStart = System.DateTime.Now;
            p.appfirst_start = true;
            Noquest.SetActive(false);
            for (int i = 1; i <= 3; i++)
            {
                while (p.quests_running[i] == 0)
                {
                    random = UnityEngine.Random.Range(1, 10);
                    Debug.Log("random:" + random);
                    if (random != p.quests_running[1] && random != p.quests_running[2] && random != p.quests_running[3])
                        p.quests_running[i] = random;

                }
            }
                p.upgrades_bought = 0;
                p.sidekicks_bought = 0;
                p.money_gathered = 0;
                for (int j = 1; j <= 6; j++)
                    p.resources_finished[j] = 0;

        }
        for(int i = 1; i <= 3; i++)
        {
            if(p.quests_running[i]!=0)
                Task[i].SetActive(true);
        }
        abc_quest_description();
    }
    // Asta e verificarea daca s-au indepplinit conditiile questurilor

    public void daily_resources()
    {
        for (int i = 1; i <= 3; i++)
        {
            if ((p.quests_running[i] == 1) && (p.resources_finished[1] >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                Task[i].SetActive(false);
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                p.quests_running[i] = 0;
                p.resources_finished[1] = 0;
            }
            if ((p.quests_running[i] == 2) && (p.resources_finished[2] >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                Task[i].SetActive(false);
                p.quests_running[i] = 0;
                p.resources_finished[2] = 0;
            }
            if ((p.quests_running[i] == 3) && (p.resources_finished[3] >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                Task[i].SetActive(false);
                p.quests_running[i] = 0;
                p.resources_finished[3] = 0;
            }

            if ((p.quests_running[i] == 4) && (p.resources_finished[4] >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                Task[i].SetActive(false);
                p.quests_running[i] = 0;
                p.resources_finished[4] = 0;
            }
            if ((p.quests_running[i] == 5) && (p.resources_finished[5] >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                Task[i].SetActive(false);
                p.quests_running[i] = 0;
                p.resources_finished[5] = 0;
            }
            if ((p.quests_running[i] == 6) && (p.resources_finished[6] >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                Task[i].SetActive(false);
                p.quests_running[i] = 0;
                p.resources_finished[6] = 0;
            }
            if ((p.quests_running[i] == 9) && (p.money_gathered >= 5000))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                Task[i].SetActive(false);
                p.quests_running[i] = 0;
                p.money_gathered = 0;
            }
        }
        abc_quest_description();
    }
    public void daily_sidekick()
    {
        for (int i = 1; i <= 3; i++)
        {
            if ((p.quests_running[i] == 7) && (p.sidekicks_bought >= 3))
            {
                bool ok;
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!"); 
                p.quests_running[i] = 0;
                ok = false;
                
                for (int j = 1; j <= 3; j++)
                    if (p.quests_running[j] == 10)
                        ok = true;
                if (ok == false)
                    p.sidekicks_bought = 0;
                Task[i].SetActive(false);

            }


            if ((p.quests_running[i] == 10) && (p.sidekicks_bought >= 5))
            {
                p.money += p.quest_reward[p.quests_running[i]];
                p.NOTIFICATION.appearWithoutBuy("Congratulation! you earn " + p.quest_reward[p.quests_running[i]] + " money from daily quest!");
                p.quests_running[i] = 0;
                p.sidekicks_bought = 0;
                
                Task[i].SetActive(false);
            }
        }abc_quest_description();
    }
    public void daily_upgrade()
    {
        for (int i = 0; i <= 3; i++)
            if ((p.quests_running[i] == 8) && (p.upgrades_bought >= 10))
            {
                p.quests_running[i] = 0;
                p.upgrades_bought = 0;
                p.money += p.quest_reward[p.quests_running[i]];
                Task[i].SetActive(false);   
            }
        abc_quest_description();
    }
    //Descrierile pt fiecare quest, in sine o sa trb sa ma mai ocup mult mai mult la ele la econimie, gen in special pt aia cu money
    public void abc_quest_description()
    {
        for (int i = 1; i <= 3; i++)
        {
            switch (p.quests_running[i])
            {
                case 1:
                    quest_description[i].text = "Finish the first resource 5 times";
                    quest_complete[i].text = p.resources_finished[1].ToString() + " out of 5";
                    break;
                case 2:
                    quest_description[i].text = "Finish the second resource 5 times";
                    quest_complete[i].text = p.resources_finished[2].ToString() + " out of 5";
                    break;
                case 3:
                    quest_description[i].text = "Finish the third resource 5 times";
                    quest_complete[i].text = p.resources_finished[3].ToString() + " out of 5";
                    break;
                case 4:
                    quest_description[i].text = "Finish the fourth resource 5 times";
                    quest_complete[i].text = p.resources_finished[4].ToString() + " out of 5";
                    break;
                case 5:
                    quest_description[i].text = "Finish the fifth resource 5 times";
                    quest_complete[i].text = p.resources_finished[5].ToString() + " out of 5";
                    break;
                case 6:
                    quest_description[i].text = "Finish the sixth resource 5 times";
                    quest_complete[i].text = p.resources_finished[6].ToString() + " out of 5";
                    break;
                case 7:
                    quest_description[i].text = "Buy 3 sidekicks";
                    quest_complete[i].text = p.sidekicks_bought.ToString() + " out of 3";
                    break;
                case 8:
                    quest_description[i].text = "Buy 10 upgrades";
                    quest_complete[i].text = p.upgrades_bought.ToString() + " out of 10";
                    break;
                case 9:
                    quest_description[i].text = "Gather 5000 bucks";
                    quest_complete[i].text = p.money_gathered.ToString() + " out of 5000";
                    break;
                case 10:
                    quest_description[i].text = "Buy 5 sidekicks";
                    quest_complete[i].text = p.sidekicks_bought.ToString() + " out of 5";
                    break;
            }

        }
        if (p.quests_running[1] == p.quests_running[2] && p.quests_running[2] == p.quests_running[3])
            Noquest.SetActive(true);
    }
}
