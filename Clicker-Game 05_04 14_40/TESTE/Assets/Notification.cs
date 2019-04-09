using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Text NotificationText;
    private Animator anim;
    public GameObject Buy;
    private GameObject grayBg;
    private void Start()
    {
        grayBg = GameObject.Find("grayBg");
        if (grayBg == null)
            Debug.LogError("GrayBg not found");
        grayBg.SetActive(false);
    }
    public void appear(string message)
    {
        grayBg.SetActive(true);
        Buy.SetActive(true);
        anim = GetComponent<Animator>();
        NotificationText.text = message;

        if (anim == null)
            Debug.LogError("Animator not found!");
        anim.SetBool("open", true);
    }
    public void appearWithoutBuy(string message)
    {
        grayBg.SetActive(true);
        Debug.Log("appear without buy");
        Buy.SetActive(false);
        anim = GetComponent<Animator>();
        NotificationText.text = message;

        if (anim == null)
            Debug.LogError("Animator not found!");
        anim.SetBool("open", true);
    }
    public void disappear()
    {
        grayBg.SetActive(false);
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("Animator not found!");
        anim.SetBool("open", false);
    }
}
