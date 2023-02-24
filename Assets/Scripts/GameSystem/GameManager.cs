using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Variables
    public static GameManager instance;
    
    [Header("Player Managing")]
    public int maxPlayers;
    public List<PlayerController> activePlayers = new List<PlayerController>();
    [HideInInspector] public int playerNumber = 0; //Ex: Player 1, Player 2... Player 3 Wins!
    
    [Header("Components")]
    public GameObject spawnFx;
    
    [Header("Starting Game")]
    public bool canFight = false;
    
    [Header("Scene Manager")]
    public string[] arenaName;
    private List<string> arenaList = new List<string>();

    [Header("Winning Game")] 
    public int pointsToWin;
    private bool gameWin;
    public string gameWinScene;

    private List<int> roundWins = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            GoToNextArena();
        }
    }

    public void AddPlayer(PlayerController newPlayer)
    {
        if (activePlayers.Count < maxPlayers)
        {
            activePlayers.Add(newPlayer);
            Instantiate(spawnFx, newPlayer.transform.position, spawnFx.transform.rotation);
        }
        else
            Destroy(newPlayer.gameObject);
    }

    public void ActivatePlayer()
    {
        foreach (PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }
    }

    public int CheckActivePlayers()
    {
        int playersInScene = 0;

        for (int i = 0; i < activePlayers.Count; i++)
        {
            if (activePlayers[i].gameObject.activeInHierarchy)
            {
                playersInScene++;
                playerNumber = i;
            }
        }

        return playersInScene;
    }

    public void GoToNextArena()
    {
        //SceneManager.LoadScene(arenaName[Random.Range(0, arenaName.Length)]);
        if (!gameWin)
        {
            if (arenaList.Count == 0)
            {
                List<string> levelList = new List<string>();
                levelList.AddRange(arenaName);

                for (int i = 0; i < arenaName.Length; i++)
                {
                    int select = Random.Range(0, levelList.Count);

                    arenaList.Add(levelList[select]);
                    levelList.RemoveAt(select);
                }
            }

            string levelToLoad = arenaList[0];
            arenaList.RemoveAt(0);

            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            foreach (PlayerController player in activePlayers)
            {
                player.gameObject.SetActive(false);
                player.GetComponent<PlayerHealthController>().FillHealth();
            }

            SceneManager.LoadScene(gameWinScene);
        }
    }

    public void StartingRound()
    {
        roundWins.Clear();
        foreach (PlayerController player in activePlayers)
        {
            roundWins.Add(0);
        }

        gameWin = false;
        
        GoToNextArena();
    }

    public void AddWinningRound()
    {
        if (CheckActivePlayers() == 1)
        {
            roundWins[playerNumber]++;

            if (roundWins[playerNumber] >= pointsToWin)
            {
                gameWin = true;
            }
        }
    }
}
