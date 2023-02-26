using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    //Variables
    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    [Header("Arts")] 
    public Sprite padDown;
    public Sprite padUp;

    [Header("Bounce Pad Values")] 
    public float timeToGetUp;
    public float bounceForce;
    private float timeFlag;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFlag > 0)
        {
            timeFlag -= Time.deltaTime;

            if (timeFlag <= 0)
                spriteRenderer.sprite = padDown;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Rigidbody2D>().velocity.y < -.1f)
            {
                timeFlag = timeToGetUp;
                spriteRenderer.sprite = padUp;

                Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
                otherRb.velocity = new Vector2(otherRb.velocity.x, bounceForce);
            }
        }
    }
}
