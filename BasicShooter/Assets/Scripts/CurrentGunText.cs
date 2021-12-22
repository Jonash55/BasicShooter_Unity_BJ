using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGunText : MonoBehaviour
{

    public GameObject player;
    public Text currentGunText;
    void Start()
    {
        currentGunText.text = "CurrentGun: Pistol";
    }
    // Update is called once per frame
    void Update()
    {
        Shooting shooting = player.GetComponent<Shooting>();
        if (shooting.m4)
        {
            currentGunText.text = "CurrentGun: M4";
        }
        if (shooting.shotgun)
        {
            currentGunText.text = "CurrentGun: Shotgun";
        }
    }
}
