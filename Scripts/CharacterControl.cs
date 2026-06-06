using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public int speed;
    public int jumpForce;
    public Rigidbody2D rb;
    public SpriteRenderer render;
    bool isMirrored = false;
    private Animator anim;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        MirrorRender();
        if(Input.GetMouseButtonDown(0)) Attack();
    }

    private void MirrorRender()
    {
        
        if(rb.velocity.x > 0) isMirrored = false;
        else if(rb.velocity.x < 0) isMirrored = true;
        if(rb.velocity.x == 0)  isMirrored = isMirrored;
        render.flipX = isMirrored;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }
    private void Movement()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
            anim.SetBool("isRun", true);
        }
        else anim.SetBool("isRun", false);
        if(Input.GetKeyDown("space"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Grounded", true);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Grounded", false);
        }
    }
}
