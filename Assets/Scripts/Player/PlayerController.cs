using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Variables
    [Header("Components")] 
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    
    [Header("Movement")] 
    public float moveSpeed;
    public float jumpForce;

    [Header("Movements")] 
    private float horizontal;

    [Header("Ground Checking")] 
    public Transform groundCheck;
    public bool isGrounded;
    public LayerMask checkLayer;

    [Header("Controllers")] public bool isKeyboard2;

    [Header("Attack")] 
    public float timeBetweenAttacks;
    private float attackCounter;
    
        // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManager.instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeyboard2)
        {
            horizontal = 0;

            if (Keyboard.current.jKey.isPressed)
                horizontal = -1f;
            
            if (Keyboard.current.lKey.isPressed)
                horizontal += 1f;
            
            if (Keyboard.current.rightShiftKey.wasPressedThisFrame && isGrounded)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            if (Keyboard.current.rightShiftKey.wasReleasedThisFrame && !isGrounded && rb.velocity.y > 0f)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);

            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                anim.SetTrigger("attack");
                attackCounter = timeBetweenAttacks;
            }
        }
        
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y); // Horizontal Movement
        
        //Configuring Animations
        anim.SetBool("isGrounded", isGrounded); // Idle-Jump Animation
        anim.SetFloat("yspeed", rb.velocity.y); // Falling Animation
        anim.SetFloat("speed", Math.Abs(rb.velocity.x)); // Walk Animation
        
        //Fliping
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        if (rb.velocity.x > 0)
            transform.localScale = Vector3.one;
        
        //Stop moving while attack
        if (attackCounter > 0)
        {
            attackCounter = attackCounter - Time.deltaTime;

            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, checkLayer);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (context.canceled && !isGrounded && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger("attack");
            attackCounter = timeBetweenAttacks;
        }
    }
}
