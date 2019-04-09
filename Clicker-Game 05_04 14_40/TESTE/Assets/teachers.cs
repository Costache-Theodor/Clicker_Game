using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teachers : MonoBehaviour
{
    public Image[] IMG_TCH;
    public Image[] TCH_AVATAR;
    public Sprite NONE;
    public Text[] EQUIP;
    public test player;
    public Notification NOTIFICATION;
    public GameObject[] TCH;
    private void Start()
    {
        for (int i = 1; i <= 6; i++)
        {
            if (player.isboughtTCH[i])
            {
                TCH[i].SetActive(false);
                EQUIP[i].text = "EQUIP";
            }
                
        }
        for (int i = 1;i<= 5; i++)
        {

            IMG_TCH[i].sprite = NONE;
            if (player.teachers_active[i] != 0)
            {
                Debug.Log("Set avatar " + player.teachers_active[i] + " on position " + i);
                IMG_TCH[i].sprite = TCH_AVATAR[player.teachers_active[i]].sprite;
                EQUIP[i].text = "UNEQUIP";
            }
        }
        

    }
    public void buy_manager_money(int i)
    {
        if (player.money >= player.Manager_Cost[i])
        {
            player.money -= player.Manager_Cost[i];
            TCH[i].SetActive(false);
            player.isboughtTCH[i] = true;
        }
        else
        {
            NOTIFICATION.appear("Nu ai suficienta valuta SARACULE");
            return;
        }
        bool temp = putTeacher(i);

        Debug.Log("teachers_active final:" + player.teachers_active[1] + ", " + player.teachers_active[2] + ", " + player.teachers_active[3] + ", " + player.teachers_active[4] + ", "
                    + player.teachers_active[5] );
    }
    public void buy_manager_diamonds(int i)
    {
        if (player.diamonds >= player.Manager_diamond_cost[i])
        {
            player.diamonds -= player.Manager_diamond_cost[i];
            TCH[i].SetActive(false);
            player.isboughtTCH[i] = true;
        }
        else
        {
            NOTIFICATION.appear("Nu ai suficiente diamante SARACULE");
            return;
        }

        bool temp = putTeacher(i);
    }

    private bool putTeacher(int i)//returneaza FALSE daca cele 5 pozitii sunt ocupate TRUE daca s-a putut pune
    {
        EQUIP[i].text = "EQUIP";
        int j;
        for (j = 1; j <= 5; j++)
            if (player.teachers_active[j] == 0)
                break;
        
        if (j == 6)
            return false;
        Debug.Log("Spatiu gol pe pozitia: " + j);
        player.teachers_active[j] = i;
        IMG_TCH[j].sprite = TCH_AVATAR[i].sprite;
        de_activateManager(i,1);
        EQUIP[i].text = "UNEQUIP";
        return true;
    }

    public void de_activateManager(int i, int actOrdeact)
    {///<summary>actOrDeact = 1 - activation (-1 for deactivation)</summary>

        switch (i)
        {
            case 1:

                Debug.Log("Manager1ACT");
                for (int j = 1; j <= 6; j++)
                    player.reward[j] += actOrdeact;
                    //player.reward[j]++;
                break;

            case 2:

                player.reward[1] += 5 * actOrdeact;
                break;

            case 3:

                player.reward[2] += 10 * actOrdeact;
                break;

            case 4:

                player.reward[3] += 15 * actOrdeact;
                break;

            case 5:

                Debug.Log("Manager5ACT");
                player.reward[4] += 20 * actOrdeact;
                break;

            case 6:

                player.reward[5] += 25 * actOrdeact;
                break;

            case 7:

                player.reward[6] += 30 * actOrdeact;
                break;

            case 8:

                for (int j = 1; j <= 7; j++)
                    if (player.maxValue[j] >= 1)
                        player.maxValue[j] -= 1 * actOrdeact;
                break;

            case 9:

                player.maxValue[1] = 1;///????////
                break;

            case 10:
                ///????////
                player.diamonds += 200;
                player.Manager_Cost[i] += 500;
                player.Manager_diamond_cost[i] += 30;
                break;
                
        }
    }

    public void equip(int i)
    {
        Debug.Log("equip/unequip tch:" + i);
        if (EQUIP[i].text == "EQUIP")
        {
            Debug.Log("EQUIP:" + i);
            if (!putTeacher(i))
                NOTIFICATION.appearWithoutBuy("5 teachers already in use!");
        }
        else
        {
            Debug.Log("UNEQUIP" + i);
            de_activateManager(i, -1);
            int j;
            for (j = 1; j <= 5; j++)
            {
                if (player.teachers_active[j] == i)
                    break;
            }
            if (j == 6)
                Debug.LogError("EQUIP/UNEQUIP ERROR");

            IMG_TCH[j].sprite = NONE;
            EQUIP[i].text = "EQUIP";
            player.teachers_active[j] = 0;
            
        }
        Debug.Log("teachers_active after equip/unequip:" + player.teachers_active[1] + ", " + player.teachers_active[2] + ", " + player.teachers_active[3] 
                    + ", " + player.teachers_active[4] + ", " + player.teachers_active[5]);
    }

    public void unequip_from_avatar(int i)
    {
        for(int j = 1; j <= 6; j++)
        {
            if (IMG_TCH[i].sprite == TCH_AVATAR[j].sprite)
            {
                Debug.Log("unequip tch: " + j);
                equip(j);
                return;
            }
        }
    }
}
