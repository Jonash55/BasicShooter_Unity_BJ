using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpScript : MonoBehaviour
{
    public GameObject player;
    public Text hpText;

    // Update is called once per frame
    void Update()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement.healthPoints <= 0)
        {
            hpText.text = "HP: 0";
        }
        else
        {
            hpText.text = "HP: " + playerMovement.healthPoints.ToString();
        }
           
    }
}
