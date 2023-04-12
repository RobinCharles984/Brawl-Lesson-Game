using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
    //Variables
    [Header("UI Components")] 
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateBt()
    {
        PhotonNetwork.CreateRoom(createInput.text, new RoomOptions{MaxPlayers = 6}, null);
    }

    public void JoinBt()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode + message);
    }
}
