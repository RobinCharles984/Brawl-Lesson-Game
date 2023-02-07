using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    //Variables
    [Header("Time Counter")] 
    private float startCounter;
    public float timeToStart;

    [Header("UI")] 
    public TMP_Text text;
    
    private int playersInZone;
    
    // Start is called before the first frame update
    void Start()
    {
        playersInZone = 0;
        startCounter = timeToStart;
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playersInZone > 1 && playersInZone == GameManager.instance.activePlayers.Count)
        {
            startCounter -= Time.deltaTime;

            text.text = Mathf.CeilToInt(startCounter).ToString();
            if (startCounter <= 0)
            {
                GameManager.instance.StartingRound();
            }
        }
        else
            text.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            playersInZone++;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playersInZone--;
            startCounter = timeToStart;
        }
    }
}
