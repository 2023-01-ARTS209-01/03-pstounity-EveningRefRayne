using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;

    public Rigidbody2D rb;

    public float moveSpeed = 10;
    public float jumpPower = 10;
    public bool isGrounded;
    

    //initialization on run
    void Awake()
    {
        //Get the game controller when the game runs.
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FixedUpdate called once per physics cycle
    void FixedUpdate()
    {
        if (rb.velocity.x>0) GetComponent<SpriteRenderer>().flipX = true;
        if (rb.velocity.x<0) GetComponent<SpriteRenderer>().flipX = false;

        if (rb.velocity.y>0&&!isGrounded)
        {
            if (Input.GetAxis("Vertical")==0||Input.GetAxis("Jump")==0)
            {
                rb.AddForce(Vector2.down*Physics.gravity.y*Physics.gravity.y,ForceMode2D.Force);
            }
        }

        
        
        if (Input.GetAxis("Horizontal")!=0)
        {
            if (rb.velocity.x==0) rb.AddForce(Vector2.right*Input.GetAxis("Horizontal")*moveSpeed*rb.mass*50,ForceMode2D.Force);
            else
            {
                bool oppositeDirection = false;
                if ((Input.GetAxis("Horizontal")>0&&rb.velocity.x<0) || (Input.GetAxis("Horizontal")<0&&rb.velocity.x>0)) oppositeDirection = true;
                if (oppositeDirection) { rb.AddForce(Vector2.right*Input.GetAxis("Horizontal")*moveSpeed*rb.mass*10,ForceMode2D.Force); }
                else { rb.AddForce(Vector2.right*Input.GetAxis("Horizontal")*moveSpeed*rb.mass,ForceMode2D.Force); }
            }
        }
        else if (rb.velocity.x!=0)
        {
            rb.AddForce(Vector2.right*rb.velocity.x*rb.mass*-20f,ForceMode2D.Force);
        }
        if ((Input.GetAxis("Vertical")!=0||Input.GetAxis("Jump")!=0)&&isGrounded)
        {
            isGrounded=false;
            rb.AddForce(Vector2.up*jumpPower*rb.mass,ForceMode2D.Impulse);
        }

        if (rb.velocity.x > moveSpeed) rb.velocity=new Vector2(moveSpeed,rb.velocity.y);
        else if (rb.velocity.x<-moveSpeed) rb.velocity = new Vector2(moveSpeed*-1f,rb.velocity.y);
    }

        //Check collisions. Mostly to tell if the player has landed on the ground.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            ContactPoint2D[] points = collision.contacts;
            foreach(ContactPoint2D point in points)
            {
                if(point.point.y<(transform.position.y-GetComponent<SpriteRenderer>().bounds.extents.y*0.5f))
                {
                    isGrounded=true;
                }
            }
        }

        //OnCollisionExit is to check if the player has run off of a platform and is in the air again.
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!GetComponent<CapsuleCollider2D>().IsTouchingLayers(128))//128 decimal = 1000000 binary, for the 7th layer.
            {
                isGrounded=false;
            }
        }
    }
