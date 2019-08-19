//해당 스크립트는 특정버튼을 눌렀을때 활성화 되는 코드임
//제작자 김강훈

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_off_item : MonoBehaviour
{
    public GameObject[] off_item; //비활성화된 오브젝트를 활성화 시키기 위해 넣음
    public GameObject thePlayer;

    private CharcterMoving thePlayerMoving;

   
    // Start is called before the first frame update
    void Start()
    {
        thePlayerMoving = thePlayer.GetComponent<CharcterMoving>();
    }

    // Update is called once per frame
    void Update()
    {
        act_item();

    }
    void act_item()
    {
        if (off_item[0] == true)
        {
            if (thePlayerMoving.currentTool == "flashlight")
            {
                off_item[0].SetActive(true);
                off_item[1].SetActive(false);
            }

            if (thePlayerMoving.currentTool == "rader")
            {
                off_item[1].SetActive(true);
                off_item[0].SetActive(false);
            }
        }
    }

}

