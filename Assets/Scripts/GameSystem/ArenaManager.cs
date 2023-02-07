using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    public List<Transform> spawns = new List<Transform>();
    private bool roundOver = false;

    [Header("Canvas")] 
    public TMP_Text playerWinsText;
    public GameObject roundOverText, textBackGround;

    private void Awake()
    {
        //Removing Texts before the Round Begins
        roundOverText.SetActive(false);
        textBackGround.SetActive(false);
        playerWinsText.text = "";
    }

    void Start()
    {
        foreach (PlayerController player in GameManager.instance.activePlayers)
        {
            int randomSpawn = Random.Range(0, spawns.Count);
            player.transform.position = spawns[randomSpawn].position;

            if (GameManager.instance.activePlayers.Count > 1)
            {
                spawns.RemoveAt(randomSpawn);
            }
        }

        GameManager.instance.canFight = true;
        GameManager.instance.ActivatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.CheckActivePlayers() == 1 && !roundOver)
        {
            roundOver = true;
            StartCoroutine(RoundOver());
        }
    }
    
    //Coroutines
    public IEnumerator RoundOver()
    {
        roundOverText.SetActive(true);
        textBackGround.SetActive(true);
        playerWinsText.text = "Player " + (GameManager.instance.playerNumber + 1) + " is Victorious!";
        
        GameManager.instance.AddWinningRound();
        
        yield return new WaitForSeconds(5f);
        
        GameManager.instance.GoToNextArena();
    }
}
