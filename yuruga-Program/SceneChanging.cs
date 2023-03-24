using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour {

    public int bossBattleStart;
    GameObject Player;

    // Use this for initialization
	void Start () {
        bossBattleStart = 0;
        Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (bossBattleStart ==1)
        {
            SceneManager.LoadScene("BossScean");
            Debug.Log("ボス戦に移行");
        }

        if (bossBattleStart == 2)
        {
            SceneManager.LoadScene("scee1");
            Destroy(Player);
            Debug.Log("タイトルに戻る");
        }
    }
}
