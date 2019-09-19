using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFOV : MonoBehaviour
{

    public float viewRange = 15.0f; // 시야의 크기
    public float viewAngle = 120.0f; // 시야각

    private Transform AITr; // AI좌표
    private Transform playerTr; // 플레이어 좌표
    private int playerLayer; // 플레이어 레이어
    private int obstacleLayer; // 장애물 레이어
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        AITr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        playerLayer = LayerMask.NameToLayer("Player");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }

    public Vector3 CirclePoint(float angle) // AI를 중심으로 원
    {
        angle += transform.eulerAngles.y; 
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),
                                     0,
                                     Mathf.Cos(angle * Mathf.Deg2Rad)); // 휴우..
    }

    public bool isTracePlayer() // isTracePlayer를 얻기 위한 함수
    {
        bool isTrace = false; // 아닐경우

        Collider[] colls = Physics.OverlapSphere(AITr.position
                                               , viewRange
                                               , 1 << playerLayer); // 거리확인

        if (colls.Length == 1)
        {
            Vector3 dir = (playerTr.position - AITr.position).normalized; // 플레이어와 AI의 거리를 벡터화

            if (Vector3.Angle(AITr.forward, dir) < viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }
        return isTrace;
    }

    public bool isViewPlayer() // isViewPlayer를 얻기 위한 함수
    {
        bool isView = false;
        RaycastHit hit;

        Vector3 dir = (playerTr.position - AITr.position).normalized; // 플레이어와 AI의 거리를 벡터화

        if (Physics.Raycast(AITr.position, dir, out hit, viewRange, layerMask))
        {
            isView = (hit.collider.CompareTag("Player")); // 사이에 장애물이 없을 경우
        }
        return isView;
    }
}
