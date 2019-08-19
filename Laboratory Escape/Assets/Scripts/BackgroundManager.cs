using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject exhustedBG;
    public GameObject thePlayer;

    private CharcterMoving charcter;

    void Start()
    {
        charcter = thePlayer.GetComponent<CharcterMoving>();
    }

    // Update is called once per frame
    void Update()
    {
        ExhustedOnOff();
    }


    private void ExhustedOnOff()
    {
        if (charcter.isExhausted)
        {
            exhustedBG.gameObject.SetActive(true);
        }
        else
        {
            exhustedBG.gameObject.SetActive(false);
        }
    }
}
