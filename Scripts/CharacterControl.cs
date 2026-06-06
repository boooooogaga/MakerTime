using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private GameManager game;
    public int speed;

    public int damage;

    public int health;
    public int jumpForce;
    public Rigidbody2D rb;
    public SpriteRenderer render;
    bool isMirrored = false;
    private Animator anim;


    public bool canMove = true;

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(canMove)
        {
            Movement();
        }
        MirrorRender();
        if(Input.GetMouseButtonDown(0)) Attack();

        if(health <= 0)
        {
            StartCoroutine(Death());
        }
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
        if (other.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(spikeDamage(other.gameObject));
        }
        
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Grounded", false);
        }
    }

    private IEnumerator spikeDamage(GameObject spike)
    {
        canMove = false;
        Vector2 knockbackDirection = new Vector2((transform.position.x - spike.transform.position.x), -(transform.position.y - spike.transform.position.y)).normalized;
        health -= spike.GetComponent<Trap>().damage;
        rb.AddForce(knockbackDirection * spike.GetComponent<Trap>().dashForce, ForceMode2D.Impulse);
        for (int i = 0; i < 3; i++)
        {
            render.enabled = false;
            yield return new WaitForSeconds(0.1f);
            render.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.7f);
        canMove = true;
    }

    private IEnumerator Death()
    {
        anim.SetTrigger("Dead");
        canMove = false;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length * 2);
        anim.enabled = false;
        yield return new WaitForSeconds(1f);
        game.ShowDeathScreen();
    }
}
