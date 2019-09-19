using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject thePlayer;

    public float reactDist;
    private float distance;

    public bool isNear = false;

    void Start()
    {
        
    }


    void Update()
    {
        distance = Vector3.Distance(thePlayer.transform.position, transform.position);

        NearCheck();
    }

    private void NearCheck()
    {
        if (distance <= reactDist)
        {
            isNear = true;
        }
        else
        {
            isNear = false;
        }
    }
}
