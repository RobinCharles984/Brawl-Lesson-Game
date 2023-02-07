using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompScript : MonoBehaviour
{
    //Variables
    public int stompDamage;
    public float bounceForce;
    public PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && player.rb.velocity.y < -.1f)
        {
            if(GameManager.instance.canFight)
                other.GetComponent<PlayerHealthController>().DamagePlayer(stompDamage);
            
            player.rb.velocity = new Vector2(player.rb.velocity.x, bounceForce);
        }
    }
}
