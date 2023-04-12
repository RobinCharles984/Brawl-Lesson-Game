using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviourPunCallbacks
{
    //Variables
    [Header("Game Objects")]
    public GameObject lobbyScreen;
    public GameObject connectionScreen;
    public GameObject disconnectionScreen;
    
    // Function if player chooses online game
    public void OnlineBt()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Function if player chooses local game
    public void LocalBt()
    {
        SceneManager.LoadScene("Character Selector");
    }

    // Button to return to lobby if disconnected
    public void TryAgainBt()
    {
        disconnectionScreen.SetActive(false);
        connectionScreen.SetActive(false);
    }

    // Function if connected to App Server
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    
    // Connection Failed
    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectionScreen.SetActive(true);
        Debug.Log(cause);
    }

    // Connection Successful
    public override void OnConnected()
    {
        lobbyScreen.SetActive(false);
        disconnectionScreen.SetActive(false);
        connectionScreen.SetActive(true);
    }

}
