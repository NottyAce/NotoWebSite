using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangingFromBossToEpilogue : MonoBehaviour {

   public int sceneMoving;//戦闘終了のシグナル
    GameObject Player;

    
    // Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (sceneMoving == 1)
        {
            SceneManager.LoadScene("epilogue");
        }

        if (sceneMoving == 2)
        {
            Destroy(Player);
            SceneManager.LoadScene("scee1");
        }
    }
}
