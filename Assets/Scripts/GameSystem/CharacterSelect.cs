using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    //Variables
    [Header("Components")] 
    public SpriteRenderer spRender;
    public Sprite btnUp, btnDown;
    public AnimatorOverrideController animOver;
    public bool isPressed;

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
        if (other.tag == "Player" && !isPressed)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.rb.velocity.y < -.1f)
            {
                isPressed = true;
                spRender.sprite = btnDown;
                if (spRender.sprite == btnDown)
                    other.GetComponent<PlayerController>().anim.runtimeAnimatorController = animOver;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && isPressed)
        {
            isPressed = false;
            spRender.sprite = btnUp;
            other.GetComponent<PlayerController>().anim.runtimeAnimatorController = animOver;
        }
    }
}
