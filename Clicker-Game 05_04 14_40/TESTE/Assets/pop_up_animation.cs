using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pop_up_animation : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    public void appear()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("Animator not found!");
        anim.SetBool("open", true);
    }
    public void disappear()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("Animator not found!");
        anim.SetBool("open", false);
    }
}
