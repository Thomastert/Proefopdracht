using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    private float JumpHeight = 1000f;
    private bool JumpCooldown = false;
    private Rigidbody2D rb;
    private bool timer;
    private bool IsGrounded;
    [SerializeField] private Transform groundedEnd;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        JumpInput();
        StartCoroutine(Timer());
        JumpingRayCast();
        Debug.Log(IsGrounded);
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
        {
            //StartCoroutine(Jump());
            rb.AddForce(Vector3.up * JumpHeight);
        }
    }

    void JumpingRayCast()
    {
        IsGrounded = Physics2D.Linecast(this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer("ground"));
        // downwards linecast 
    }


    IEnumerator Jump()
    {
        if (JumpCooldown == false)
        {
            JumpCooldown = true;
            rb.AddForce(Vector3.up * JumpHeight);
            yield return new WaitForSeconds(0.8f);
            JumpCooldown = false;
        }
    }

    IEnumerator Timer()
    {
        if (timer == false)
        {
            timer = true;
            increaseSpeed();
            yield return new WaitForSeconds(1);
            timer = false;
        }
    }

    void increaseSpeed()
    {
        ChunkManager.screenMoveSpeed = ChunkManager.screenMoveSpeed + 1f;
    }
}
