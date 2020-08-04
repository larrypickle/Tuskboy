using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class Shoot_Timer : MonoBehaviour
{
    public GameObject bar;
    public int time = 2;

    public Player_Combat pc;

    void Start()
    {
        AnimateBar();
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time).setOnComplete(EmptyBar);

    }

    public void EmptyBar()
    {
        bar.transform.localScale = new Vector3(0, 1, 1);
        AnimateBar();
        //pc.PlayerEmpathySphere();
        Debug.Log("Works! ");
        
        
        
    }
}
