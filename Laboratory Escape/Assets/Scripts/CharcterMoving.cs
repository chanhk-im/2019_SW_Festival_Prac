/*
 * 제작자: 임찬혁
 * 마지막 수정 일자: 190730
 * 기능: 플레이어의 도구 사용 관리
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterMoving : MonoBehaviour
{
    [SerializeField] // private는 인스펙트 창에서 나오지 않지만 SerializeField 를 써주면 수정 가능
    private float walkSpeed;
    private float subWalkSpeed;
    [SerializeField]
    private float runSpeed;
    private float subRunSpeed;
    private float applySpeed;
    // applySpeed를 사용하는 이유 : walkSpeed와 runSpeed를 대입해서 사용 할 수 있기 때문 이게 아니면 함수 2개를 써야한다

    [SerializeField]
    private float jumpForce; // 순간적으로 얼마만큼의 힘으로 위로 튀어오를것이냐

    // 상태 변수
    private bool isRun = false; // 걷는건지 뛰는건지 판단 기본값은 false
    private bool isGround = true; // 땅에 있는지 없는지 가장먼저 땅에서 시작하기 때문에 true
    public bool isExhausted; // 탈진상태이면 true
    // 땅 착지 여부
    private CapsuleCollider capsuleCollider;
    public int runcount = 300;


    [SerializeField]
    private float lookSensitvity; // 카메라의 민감도

    [SerializeField]
    private float cameraRotionLimit; // 고개를 돌릴때 360도 계속 돌아가면 안되기 때문에 한정 각도를 만들어줌aa
    private float currentCameraRotationX = 0; // 우선 앞을 쳐다보아야하기 때문에 0으로 선언 하지만 선언 안해줘도 어차피 0으로 선언되어있음aa

    [SerializeField]
    private Camera theCamera; // SerializedField 를 쓴 이유는 player에게는 카메라가 없고 자식개체에 있으므로 불러오기위함

    private Rigidbody myRigid; // Rigidbody 란 실제 플레이어의 육체적인 몸을 의미 (물리적인 힘을 받음)

    // 게임오브젝트
    public GameObject[] tools;  // 게임오브젝트 배열

    // 게임오브젝트 정보
    private ToolInformation[] toolsInfo; // 도구들의 정보를 가져오기 위해 선언함

    public string currentTool;
    public string walkCheck = "stop";

    //사운드
    public AudioSource walk, run, exhausted;
    
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        //theCamera = FindObjectOfType<Camera>(); // 이런식으로 불러와도 됨
        myRigid = GetComponent<Rigidbody>(); // Rigidbody component를 myRigid 에 넣겠다.
        applySpeed = walkSpeed; // 달리기전까진 무조건 걷는상태

        toolsInfo = new ToolInformation[tools.Length];

        subRunSpeed = runSpeed;
        subWalkSpeed = walkSpeed;

        //사운드 초기화
        //초기화를 이미 밖에서 했기 때문에!! 필요가 없다!
        //walk = GetComponent<AudioSource>();
        //run = GetComponent<AudioSource>();
        //exhausted = GetComponent<AudioSource>();

        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(false);
            toolsInfo[i] = tools[i].GetComponent<ToolInformation>();
        }
    }

    
    void Update()
    {
        ToolManager();

        IsGround();
        TryJump();
        TryRun(); // 반드시 TryRun은 Move위에 있어야함 먼저 뛰는지 걷는지 판별을 해야하기 때문aa
        Move();
        CameraRotation();
        CharacterRotation();
        Sound();

    }
    void Sound()
    {
        if (walkCheck == "stop")
        {
            StopPlay();

        }
        else if (!walk.isPlaying && walkCheck == "walk")
        {
            StopPlay();
            walk.Play();
        }
        else if(!run.isPlaying && walkCheck == "run")
        {
            StopPlay();
            run.Play();
        }
        else if(!exhausted.isPlaying && walkCheck == "exhausted")
        {
            StopPlay();
            exhausted.Play();
        }
        
    }

    private void StopPlay()
    {
        walk.Stop();
        run.Stop();
        exhausted.Stop();
    }

    private void ToolManager()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeTools(0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeTools(1);
        }
    }


    private void ChangeTools(int _on)  // 도구 교체
    {
        if (toolsInfo[_on].isActive)
        {
            for (int i = 0; i < tools.Length; i++)
            {
                if (i == _on)
                {
                    tools[i].SetActive(true);
                }
                else
                {
                    tools[i].SetActive(false);
                }
            }
            currentTool = toolsInfo[_on].toolName;
        }
            
    }


    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        // 고정된 좌표를 사용 : 무조건 아래로 레이져를 쏴야하기 때문
        // bounds : 캡슐의 영역 , extents : bounds 크기의 반
        // 이유 : capsuleCollider.bounds.extents.y 란 캡슐 y축 크기 전체의 반. 따라서 레이져를 캡슐을 중앙에서 땅까지 쏜다는 것이다aa
    }

    private void TryJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround) // isGround 가 true일때만 실행(땅에 붙어있을 때)
        {
            Jump();
        }
    }

    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce; // transform.up = (0, 1, 0) : 공중. 따라서 공중방향으로 jumpForce만큼 뜀aa
        // velocity는 myRigid가 현재 움직이고 있는 속도
    }


    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)) // GetKey : 키가 계속 눌러지고 있는 상태
        {
            if (runcount > 0 && runcount <= 300)
            {
                Running();
                runcount--;
                MoveCheck("run");
            }
        }
        else
        {
            if (runcount >= 0 && runcount < 300)
            {
                RunningCancel();
                runcount++;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }

        if (runcount == 0)
        {
            walkSpeed = 0;
            runSpeed = 0;
            applySpeed = 0;

            isExhausted = true;
            walkCheck = "exhausted";
        }
        if (runcount > 150)
        {
            walkSpeed = subWalkSpeed;
            runSpeed = subRunSpeed;

            isExhausted = false;
        }
    }

    private void Running()
    {
        isRun = true; // 달리고있는 상태 : true
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    private void Move()
    {
        // 유니티에서는 x가 좌우이고,z가 앞뒤이다. 
        float moveDrirX = Input.GetAxisRaw("Horizontal");
        float moveDrirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDrirX;
        //플룻값을 3개 가지고 있는 변수 Vector3 에는 (1, 0, 0) 이 저장되어있음 따라서 오른쪽 방향키를 누르면
        //(1, 0, 0) * 1 = (1, 0, 0) 이 되므로 오른쪽으로 1 나아가게 된다 (왼쪽은 그 반대)
        Vector3 moveVerical = transform.forward * moveDrirZ;
        //요것도 똑같이 움직임임
        Vector3 velocity = (moveHorizontal + moveVerical).normalized * applySpeed;
        // nomalized를 하는 이유 (1, 0, 1)이 (0.5, 0, 0.5)가 되기 때문이다
        // 삼각함수로 볼때 (1, 1) 과 (0.5, 0.5) 의 진행방향은 같기 때문에 좀 더 빠르게 인식하려고 nomalized를 사용
        // 합이 1이 나오는게 가장 이상적이다
        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
        // deltaTime를 쓰는 이유 1초동안 velocity만큼 움직이게 하겠다
        if ((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0))
        {
            MoveCheck("stop");
        }
        if(!Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            MoveCheck("walk");
        }
    }
    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitvity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));
        // 우리가 구한 값을 Quaternion 으로 바꿔줌

        if (Input.GetAxisRaw("Mouse X") == 0)
        {
            myRigid.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            myRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

    }

    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        // 앞에는 x고 뒤에는 y인 이유 : 마우스는 3차원이 아님
        float cameraRotationX = xRotation * lookSensitvity;
        // 마우스를 쭉올렸다고 머리도 쭉올라가면 안되기 때문에 민감도를 설정
        currentCameraRotationX -= cameraRotationX;
        // 여기서 더하기와 빼기는 Y축 반전이라고 생각하면 된다
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotionLimit, cameraRotionLimit);
        //Mathf.Clamp 라는 함수로 리미트(-45 ~ 45)만큼 currentCameraRotationX를 가두기

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0.0f, 0.0f);
        // 위아래로만 움직이고 좌우로는 움직이지 않을것이기 때문에 다른것은 0으로 고정aa
    }


    private void MoveCheck(string _check)
    {
        if (!isExhausted)
        {
            walkCheck = _check;
        }
        else
        {
            walkCheck = "exhausted";
        }
    }



}
