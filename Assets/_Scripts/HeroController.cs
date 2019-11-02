using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HeroController : MonoBehaviour
{
    public Animator anim;
    public HeroAnimState heroAnimState;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rgd;

    public Transform target;

    public bool grounded;
    public AudioSource jumpSound;
    public float jumpForce;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        heroAnimState = HeroAnimState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        Debug.DrawRay(transform.position, Vector2.down * 2, Color.red);
        //grounded = Physics2D.Linecast(transform.position, target.position, 1 << LayerMask.NameToLayer("Ground"));
        
        grounded = Physics2D.BoxCast(transform.position,
            new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));
        

        if((Input.GetAxis("Horizontal") > 0) && (grounded))
        {
            spriteRenderer.flipX = false;
            heroAnimState = HeroAnimState.WALK;
            anim.SetInteger("AnimState", (int)HeroAnimState.WALK);
            rgd.AddForce(Vector2.right * speed);
        }
        if ((Input.GetAxis("Horizontal") < 0) && (grounded))
        {
            spriteRenderer.flipX = true;
            heroAnimState = HeroAnimState.WALK;
            anim.SetInteger("AnimState", (int)HeroAnimState.WALK);
            rgd.AddForce(Vector2.left * speed);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            heroAnimState = HeroAnimState.IDLE;
            anim.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            
        }
        if ((Input.GetAxis("Jump") > 0) && (grounded))
        {
            anim.SetInteger("AnimState", (int)HeroAnimState.JUMP);
            heroAnimState = HeroAnimState.JUMP;
            rgd.AddForce(Vector2.up * jumpForce);
            jumpSound.Play();
        }
    }
}
