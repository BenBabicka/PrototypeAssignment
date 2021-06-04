using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;

    public Transform playerResetPosition;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ResetPlayerPosition()
    {
        player.transform.position = playerResetPosition.position;
    }

}
