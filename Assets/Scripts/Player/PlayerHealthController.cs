using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    //Variables
    [Header("Life Varibles")] 
    public int maxHealth;
    private int currentHealth;

    [Header("Sprites")] 
    public SpriteRenderer[] sprRenderer;
    public Sprite heartFull, heartEmpty;

    [Header("Other Components")] 
    public Transform healthTransform;

    [Header("Counter Variables")] 
    public float invincibleTime, flashTime;
    private float invincCounter, flashCounter;
    

        // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter < 0)
            {
                flashCounter = flashTime;
                
                healthTransform.gameObject.SetActive(!healthTransform.gameObject.activeInHierarchy);
            }
        }

        if (invincCounter <= 0)
        {
            healthTransform.gameObject.SetActive(true);
        }
    }

    public void LateUpdate()
    {
        healthTransform.localScale = transform.localScale;
    }

    public void UpdateHealth()
    {
        switch (currentHealth)
        {
            case 3:
                sprRenderer[0].sprite = heartFull;
                sprRenderer[1].sprite = heartFull;
                sprRenderer[2].sprite = heartFull;
                break;
            
            case 2:
                sprRenderer[0].sprite = heartFull;
                sprRenderer[1].sprite = heartFull;
                sprRenderer[2].sprite = heartEmpty;
                break;
            
            case 1:
                sprRenderer[0].sprite = heartFull;
                sprRenderer[1].sprite = heartEmpty;
                sprRenderer[2].sprite = heartEmpty;
                break;
            
            case 0:
                sprRenderer[0].sprite = heartEmpty;
                sprRenderer[1].sprite = heartEmpty;
                sprRenderer[2].sprite = heartEmpty;
                gameObject.SetActive(false);
                break;
            
        }
    }

    public void DamagePlayer(int damageToReceive)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damageToReceive;
            UpdateHealth();
            invincCounter = invincibleTime;
        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        UpdateHealth();

        flashCounter = 0;
        invincCounter = 0;
    }
}
