using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_item : MonoBehaviour
{
    public bool canGetItem = false;
    public bool check_item = false;

    public GameObject item_sub;
    public GameObject thePlayer;

    private float distance;

    public float flashItemsLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(thePlayer.transform.position, transform.position);
    }

    private void OnMouseEnter()
    {
        if (distance <= flashItemsLimit)
        {
            item_sub.SetActive(true);
            canGetItem = true;
        }
            
        if (distance > flashItemsLimit)
        {
            item_sub.SetActive(false);
            canGetItem = false;
        }
            
    }
    private void OnMouseExit()
    {
        item_sub.SetActive(false);
        canGetItem = false;
    }

    private void OnMouseUp()
    {
        if (canGetItem)
        {
            check_item = true;
            this.gameObject.SetActive(false);
        }
        

    }

}
