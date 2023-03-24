using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPGage : MonoBehaviour {

    Slider pSlider;
    
    // Use this for initialization
	void Start () {
        pSlider = this.GetComponent<Slider>();


    }

    PlayerStatus pS;
    int playerHP;
    int playerMaxHP;
    
    //Update is called once per frame
	void Update () {
        pS = GameObject.Find("Player").GetComponent<PlayerStatus>();
        playerHP = pS.playerHP;
        playerMaxHP = pS.playerMaxHP;

        pSlider.minValue = 0;
        pSlider.maxValue = playerMaxHP;
        pSlider.value = playerHP;

    }
}
