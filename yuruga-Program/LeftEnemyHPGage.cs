using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftEnemyHPGage : MonoBehaviour {

    Slider lESlider;
    int leftEnemyHP;
    int leftEnemyMaxHP;
    
    // Use this for initialization
	void Start () {
        lESlider = this.GetComponent<Slider>();
        leftEnemyHP = GameObject.Find("LeftEnemy").GetComponent<EnemyScript>().enemyHP;
        leftEnemyMaxHP = GameObject.Find("LeftEnemy").GetComponent<EnemyScript>().enemyMaxHP;

    }
	
	// Update is called once per frame
	void Update ()
    {
        lESlider.minValue = 0;
        lESlider.maxValue = leftEnemyMaxHP;
        lESlider.value = leftEnemyHP;
	}
}
