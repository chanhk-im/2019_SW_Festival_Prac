using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject[] enemyObj;

    private EnemyManager[] theEnemy;

    public AudioSource beep;

    public bool nearFlag = false;

    void Start()
    {
        theEnemy = new EnemyManager[enemyObj.Length];

        for (int i = 0; i < enemyObj.Length; i++)
        {
            theEnemy[i] = enemyObj[i].GetComponent<EnemyManager>();
        }
    }


    void Update()
    {
        EnemiesNearCheck();

        if (nearFlag)
        {
            //if (!beep.isPlaying)
            beep.Play();
                //StopAndPlaySound(beep);
        }
        else
        {
            StopSounds();
        }

    }


    private void EnemiesNearCheck()
    {
        nearFlag = false;

        for (int i = 0; i < enemyObj.Length; i++)
        {
            if (theEnemy[i].isNear)
            {
                nearFlag = true;
                break;
            }
        }
    }


    private void StopAndPlaySound(AudioSource _audio)
    {
        //StopSounds();
        _audio.Play();
    }


    private void StopSounds()
    {
        beep.Stop();
    }
}
