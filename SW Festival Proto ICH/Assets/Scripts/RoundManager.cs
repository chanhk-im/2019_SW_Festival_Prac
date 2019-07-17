using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject[] text1;
    public Transform[] spawn1;

    private int textArrLen1;

    private int round = 1;
    public int allRounds;

    void Start()
    {
        textArrLen1 = text1.Length;
        ShuffleArray(text1);
    }

    // Update is called once per frame
    void Update()
    {
        if (round == 1)
        {
            SpawnText(text1, spawn1);
        }

        round = 0;
    }

    private void SpawnText(GameObject[] _text, Transform[] _spawn)
    {
        int _textArrLen = _text.Length;

        for (int i = 0; i < _textArrLen; i++)
        {
            Instantiate(_text[i], _spawn[i]);
        }
    }

    public static void ShuffleArray<T>(T[] array)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < array.Length; ++index)
        {
            random1 = UnityEngine.Random.Range(0, array.Length);
            random2 = UnityEngine.Random.Range(0, array.Length);

            tmp = array[random1];
            array[random1] = array[random2];
            array[random2] = tmp;
        }
    }

}
