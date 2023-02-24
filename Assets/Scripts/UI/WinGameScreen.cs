using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinGameScreen : MonoBehaviour
{
    //Variables
    [Header("Components")] 
    public Image playerImage;
    public string mainMenuScene, selectCharScene;
    public TMP_Text winText;
    
    // Start is called before the first frame update
    void Start()
    {
        winText.text = "!! Player " + GameManager.instance.activePlayers[GameManager.instance.playerNumber + 1] + " venceu a Partida !!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgainButton()
    {
        GameManager.instance.StartingRound();
    }

    public void MainMenuButton()
    {
        ClearGame();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void SelectCharButton()
    {
        ClearGame();
        SceneManager.LoadScene(selectCharScene);
    }

    public void ClearGame()
    {
        foreach (PlayerController player in GameManager.instance.activePlayers)
        {
            player.gameObject.SetActive(false);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }
        
        Destroy(GameManager.instance.gameObject);
    }
}
