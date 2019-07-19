using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] text1;  // 텍스트들 담는 배열   
    public Transform[] spawn1;  // 스폰지점 담는 배열

    private int stage = 1;  // 스테이지
    public int allStages;  // 총 스테이지 수

    private bool spawnFlag = true;  // 스폰할 때 쓰는 불린 변수

    private bool instance = true;  // 처음에만 true
    public bool destroyFlag = false;  // text 다시 생성할 때 기존 text를 파괴하기 위해 만듦


    void Start()
    {
        
    }


    void Update()
    {
        SpawnFlagManage();

        if (spawnFlag)
        {
            spawnFlag = false;
            StartCoroutine(SpawnManage());
        }

    }


    private void SpawnFlagManage()  // 문자 생성 활성화
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawnFlag = true;
        }
    }


    IEnumerator SpawnManage()  // 문자 생성 관리
    {
        if (stage == 1)
        {
            if (!instance)
            {
                destroyFlag = true;
            }

            yield return new WaitForSeconds(Time.deltaTime);

            destroyFlag = false;

            SpawnText(text1, spawn1);

            if (instance)
                instance = false;
        }

    }


    private void SpawnText(GameObject[] _text, Transform[] _spawn)  // 문자 랜덤한 위치에 생성
    {
        int _textArrLen = _text.Length;
        ShuffleArray(_spawn);

        for (int i = 0; i < _textArrLen; i++)
        {
            Instantiate(_text[i], _spawn[i]);
        }
    }


    public static void ShuffleArray<T>(T[] array)  // 랜덤하게 배열 섞는 함수(퍼옴...)
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
