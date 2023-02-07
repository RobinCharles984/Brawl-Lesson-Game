using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinSecondKeyboardPlayer : MonoBehaviour
{
    [Header("Spawners")]
    public GameObject playerToLoad;
    public bool hasJoined;
    
    // Update is called once per frame
    void Update()
    {
        if (!hasJoined && GameManager.instance.activePlayers.Count < GameManager.instance.maxPlayers)
        {
            if (Keyboard.current.lKey.isPressed || Keyboard.current.jKey.isPressed || Keyboard.current.kKey.isPressed ||
                Keyboard.current.rightShiftKey.isPressed)
            {
                Instantiate(playerToLoad, transform.position, transform.rotation);
                hasJoined = true;
            }
        }
    }
}
