using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private StageManager sm;  // stagemanager에 있는 destroyFlag 참조하기 위해 만듦

    void Start()
    {
        sm = FindObjectOfType<StageManager>();
    }


    void Update()
    {
        if (sm.destroyFlag == true)
        {
            Destroy(this.gameObject);
        }
    }
}
