using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sidekick : MonoBehaviour
{
    public GameObject[] Buy_area;
    public GameObject[] Equip_area;
    public test player;
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Start Sidekick");
        
        for (int i = 1; i <= 6; i++)
        {
            if (player.isboughtSDK[i])
            {
                Buy_area[i].SetActive(false);
                Equip_area[i].SetActive(true);
            }
            else
            {
                Buy_area[i].SetActive(true);
                Equip_area[i].SetActive(false);
            }
        }
    }


    public void replaceButtonSDK( int i, bool bought)
    {
        Debug.Log("ReplaceSDKbutton: I:" + i + "bought:" + bought);
        player.isboughtSDK[i] = bought;
        if (bought)
        {
            Buy_area[i].SetActive(false);
            Equip_area[i].SetActive(true);
           
        }
        else
        {
            Buy_area[i].SetActive(true);
            Equip_area[i].SetActive(false);
            set_active(i, true);
        }
    }
    public void set_active(int i, bool state)
    {
        Button bt = Equip_area[i].GetComponentInChildren<Button>();
        if (bt == null)
            Debug.LogError("SET_ACTIVE not found button " + i);
        bt.interactable = state;
        Text txt = bt.GetComponentInChildren<Text>();
        if (txt == null)
            Debug.LogError("SET_ACTIVE not found text " + i);
        if (state)
            txt.text = "Equip";
        else
            txt.text = "Equipped";
    }
         
}
