using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GageDown : MonoBehaviour
{
    public int size;
    public float Y;

    private CharcterMoving charcter;

    public GameObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        charcter = thePlayer.GetComponent<CharcterMoving>();
        
    }

    // Update is called once per frame
    void Update()
    {
        size = charcter.runcount;

        SizeDown();
    }
    void SizeDown()
    {
        gameObject.transform.localScale = new Vector3(size, Y, 0);
        
    }
}
