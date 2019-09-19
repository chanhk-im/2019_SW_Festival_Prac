// 제작자 : 이세현, 사용목적 : AI(적)이 쫒아옴 무서움
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI; // AI 기능 활성화 시키는 것 (이게 있어야 AI가 움직여)

public class AIMove : MonoBehaviour
{
	private AIFOV AIFOV;

    NavMeshAgent nav; // NavMeshAgent가 Rigidbody같은 거라서 AI객체 안에 있는 NavMeshAgent를 활성화 시키는 거야
    public GameObject[] target; // target 설정
    public GameObject target_P;
    //public Transform target_p;
    //public Vector3 direction;
    public GameObject AI; // AI설정
    public int count; 
    public int count2;
    int[] destination = { 0, 0, 0, 0, 0, 0, 0, 0 }; // 목적지의 순서와 랜덤함수를 적용시키기 위한 리스트

    private void Awake()
	{
        AIFOV = GetComponent<AIFOV>(); // AIFOV를 가져옴
	}

    void Start()
    {
        nav = GetComponent<NavMeshAgent>(); // NavMeshAgent의 이름
        target_P = GameObject.Find("Player"); // 타겟을 플레이어로 설정
        random_Destination(); // 랜덤함수를 돌림
    }

    void random_Destination()
    {
        while (true)
        {
            count = Random.Range(0, destination.Length);
            if (destination[count] == 0) // 만약 1이면 자신이 있는 곳이므로 다시 돌림
            {
                break;
            }
        }
    }

    void Update()
    {
        //target_p = GameObject.Find("player").transform;
        //direction = (target_p.position - transform.position).normalized;
        //float distance = Vector3.Distance(target_p.position, transform.position);

        if (AIFOV.isTracePlayer()) // AIFOV에서 받아옴
        {
            if (AIFOV.isViewPlayer()) // true
			{
				nav.SetDestination(target_P.transform.position);
			}
            else
			{
				nav.SetDestination(target[count].transform.position); //target의 위치를 찾아 경로를 탐색하여 쫒아옴
			}
        }
        else
        {
            nav.SetDestination(target[count].transform.position); //target의 위치를 찾아 경로를 탐색하여 쫒아옴
        }
    }

    void OnTriggerEnter(Collider other) // 목적지에 도착했을 때
    {
        if (other.tag.Equals("Destination"))
        {
            destination[count] = 1; // 현위치 안되게
            count2 = count;
            random_Destination();
        }
    }

    void OnTriggerExit(Collider other) // 목적지에서 나갔을 때
    {
        destination[count2] = 0;
    }
}
