using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {//AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
        if(other.tag == "Player")
        {
            //oh im fine
            GameObject.Find("GameController").GetComponent<GameManager>().ResetPlayerPosition();
        }
    }
}
