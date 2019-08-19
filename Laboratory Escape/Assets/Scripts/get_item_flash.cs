using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_item_flash : MonoBehaviour
{
    public GameObject[] on_item;
    public GameObject[] get_item;
    public GameObject[] tools;

    public bool[] check;

    private int[] connectToToolInfo;

    private ToolInformation[] toolsInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        toolsInfo = new ToolInformation[tools.Length];
        connectToToolInfo = new int[tools.Length];

        for (int i = 0; i < tools.Length; i++)
        {
            toolsInfo[i] = tools[i].GetComponent<ToolInformation>();
            connectToToolInfo[i] = toolsInfo[i].connectToOnOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        flash_item();
        
    }
    void flash_item()
    {
        for (int i = 0; i < on_item.Length; i++)
        {
            if (get_item[i])
            {
                check[i] = get_item[i].GetComponent<get_item>().check_item;  
                
            }
            if (check[i])
            {
                on_item[i].SetActive(true);
                for (int j = 0; j < tools.Length; j++)
                {
                    if (i == connectToToolInfo[j])
                    {
                        toolsInfo[j].isActive = true;
                        break;
                    }
                }
            }
        }

    }
}
