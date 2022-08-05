using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform camTr;
    private Rigidbody rb;
    private Animator animator;
    public GooseGrab goosegrab;

    public GardenerBT npc;

    public LayerMask layer;
    public SphereCollider gooseSphereCollider;
    public Transform groundCastPoint;
    private bool isGround;

    public bool isSneck;
    private bool isRun = false;
    private bool isWing = false;
    public static bool isHonk = false;

    private float wing = 0f;
    private float sneak = 0f;
    private float run = 0f;
    public float curspeed = 5;
    public float maxSpeed = 1;


    private Vector3 dir;
    private bool input = false;
    private Quaternion targetRot;


    public AudioClip honk;


    public GameManager gamemanager;
    void Start()
    {
        npc = GameObject.FindObjectOfType<GardenerBT>();
        camTr = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        InputSet();
        CheckForward();
        //CheckForward();

        sneak = Mathf.Lerp(sneak, isSneck ? 1f : 0f, Time.deltaTime * 10f);
        animator.SetFloat("Sneak", sneak);

        wing = Mathf.Lerp(wing, isWing ? 1f : 0f, Time.deltaTime * 10f);
        animator.SetFloat("Wing", wing);

        run = Mathf.Lerp(run, isRun ? 1f : 0f, Time.deltaTime * 10f);
        animator.SetFloat("Run", run);

    }

    private void FixedUpdate()
    {
        if (input)
        {
            rb.velocity = dir * curspeed;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 10f);
        }
    }

    public void Move()
    {
        var v = Input.GetAxisRaw("Vertical");
        var h = Input.GetAxisRaw("Horizontal");

        // 재휘 모바일 수정
        //Debug.Log($"[PlayerController] 들어가는중?");
        //Debug.Log($"[PlayerController] 값 출력?{GameManager.instance.inputMgr.moveX}");
        //

        //var v = GameManager.instance.inputMgr.moveX;
        //var h = GameManager.instance.inputMgr.moveZ;

        input = v != 0f || h != 0f;

        var viewPos1 = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
        var viewPos2 = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 10));
        var viewPosR = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 10));

        var wDir = viewPos2 - viewPos1;
        wDir.y = 0;
        wDir.Normalize();

        var rDir = viewPosR - viewPos1;
        rDir.y = 0;
        rDir.Normalize();

        dir = v * wDir + h * rDir;
        if(isGround)
        {
            var dirY = dir;
            dirY.y += 1f;
            dir = dirY;
        }
        dir.Normalize();

        if (rb.velocity.magnitude > 0.1f)
        {
            targetRot = Quaternion.LookRotation(rb.velocity);
        }

        if (input)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }
        animator.SetFloat("Velocity", rb.velocity.magnitude * 2f);
    }

    [System.Obsolete]
    public void InputSet()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSneck = !isSneck;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            isWing = !isWing;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = !isRun;

            maxSpeed = isRun ? 2.5f : 1.5f;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            float npcDistance = 3f;
            //꽥꽥
            var ran = Random.Range(1, 7);
            string fileName = "sfx_goose_honk_b_0";
            GameManager.instance.audioMgr.SFXPlay(fileName + ran.ToString());
            // gamemanager.audioMgr.SFXPlay("sfx_goose_honk_b_01");
            //npc와의 거리가 약 10cm 정도 거리라면 npc 어그로 끄는 함수 발동
            var distance = npc.transform.position - transform.position;
            if (distance.magnitude < npcDistance)
            {
                isHonk = true;
            }
        }
    }

    // 재휘 추가
    public void ChangeSneck()
    {
        isSneck = !isSneck;
    }
    public void ChangeWing()
    {
        isWing = !isWing;
    }
    public void ChangeRun()
    {
        isRun = !isRun;
        maxSpeed = isRun ? 2.5f : 1.5f;
    }
    //

    public void AniParameters()
    {
    }

    public void Shoo(Vector3 forward)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(forward, ForceMode.Impulse);
        goosegrab.Drop();
    }

    public float radius = 0.1f;

    public void CheckForward()
    {
        float checkDistance = 1f;
        bool cast = Physics.SphereCast(groundCastPoint.position, radius, Vector3.down, out var hit, checkDistance, layer, QueryTriggerInteraction.Ignore);

        if (cast)
        {
            isGround = true;
            float groundSlope = Vector3.Angle(hit.normal, Vector3.up);
            rb.velocity = Quaternion.AngleAxis(groundSlope, dir) * rb.velocity;
        }
        else if(!cast)
        {
            isGround = false;
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(dir, dir * 15f);
        
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(groundCastPoint.position, radius);
    //}
}