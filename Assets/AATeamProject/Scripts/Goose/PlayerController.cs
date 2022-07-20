using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform camTr;
    private Rigidbody rb;
    private Animator animator;

    private bool isSneck;
    private bool isRun = false;
    private bool isWing = false;

    private float wing = 0f;
    private float sneak = 0f;
    private float run = 0f;
    public float curspeed = 5;
    public float maxSpeed = 1;


    private Vector3 dir;
    private bool input = false;
    private Quaternion targetRot;
    void Start()
    {
        camTr = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        InputSet();

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
            rb.AddForce(dir * curspeed);
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
        dir.Normalize();

        if (rb.velocity.magnitude > 0.1f)
        {
            targetRot = Quaternion.LookRotation(rb.velocity);
        }

        if (input)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }
        animator.SetFloat("Velocity", rb.velocity.magnitude * 4f);
    }



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
        }
    }

    public void AniParameters()
    {

    }

    public void Shoo(Vector3 forward)
    {

    }
}