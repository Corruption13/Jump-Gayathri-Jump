using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Keyboard Controls")]
    public KeyCode interactkey;
    private bool isUsingTouchInput = false;


    [Header("Character Movement Stats")]
    public int speed = 10;
    public float jumpforce = 1000;
    public float groundHeight = 0.70f;
    public float groundWidth = 0.35f;
    public float bottomOffset = 0.004f;


    private Animator animator;
    private Rigidbody2D rb;


    [Header("JumpLayer")]
    public LayerMask groundlayer;
    public LayerMask MovableLayer;
    public LayerMask OtherPlayerLayer;
    public int maxJump = 1;

    private float h = 0;
    private float v = 0;
    public int jumpCount = 1;

    public AudioController audioController;

    private Vector2 overlapBoxSize;
    private float correctDirection;

    private void Start()
    {
        overlapBoxSize = new Vector2(groundWidth, 2 * groundHeight);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        correctDirection = transform.localScale.x;
    }

    private void Update()
    {
        if (!isUsingTouchInput)
        {
            GetInput();
        }

        

    }

    void FixedUpdate()
    {
        if (jumpCount != maxJump && ValidLayerCheck()) jumpCount = maxJump;
        AnimatePlayer();
        RotatePlayer();
        RightSideUpPlayer();
        MovePlayerHorizontal(h);
        if (v != 0)
            JumpPlayer((int)v);
    }

    /* Controls Section 
     * 
     */
    void GetInput()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
    }

    public void MovePlayerLeftStart()
    {
        isUsingTouchInput = true;
        h = -1;
    }
    public void MovePlayerRightStart()
    {
        isUsingTouchInput = true;
        h = 1;
    }
    public void StopPlayerHorizontal()
    {
        isUsingTouchInput = false;
        h = 0;
    }


    /* Motion Functions Section 
    * 
    */
    void MovePlayerHorizontal(float horizontal)
    {
        if (horizontal != 0)
            rb.velocity = new Vector2(horizontal * Time.deltaTime * speed, rb.velocity.y);
        if (horizontal == 0)
        {
            if (!Physics2D.OverlapBox(transform.position, overlapBoxSize, 360f, MovableLayer))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

    }

    public void JumpPlayer(int v)
    {


        if (jumpCount > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpforce, 0);
            jumpCount--;
            Fart();
        }

    }

    private bool ValidLayerCheck()
    {
        return Physics2D.OverlapBox(transform.position, overlapBoxSize, 180f, groundlayer);
               // || Physics2D.OverlapCircle(transform.position - new Vector3(0f, bottomOffset), collisionDetectorRadius, MovableLayer)
               // || Physics2D.OverlapBox(transform.position - new Vector3(0f, bottomOffset), overlapBoxSize * 0.75f, 180f, OtherPlayerLayer);
    }

    void RotatePlayer()
    {
        if (h == 0) { }
        else if (h == -1)
        {
            transform.localScale = new Vector3(-correctDirection, transform.localScale.y, transform.localScale.z);
        }
        if (h == 1)
        {
            transform.localScale = new Vector3(correctDirection, transform.localScale.y, transform.localScale.z);
        }
        Debug.Log(h);
    }

    void RightSideUpPlayer()
    {

        if (rb.velocity == Vector2.zero)
        {
            transform.localRotation = Quaternion.identity;
        }
    }

    void AnimatePlayer()
    {
        if (h == 1 || h == -1)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        if (v == 1)
            animator.SetBool("jumping", true);
        else
            animator.SetBool("jumping", false);
    }

    void Fart()
    {
        audioController.PlayAudio(0, true);   
    }

}
