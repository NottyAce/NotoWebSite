using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public int playerMaxHP = 100;//プレイヤーHPの最大値
    public int playerHP;//ダメージ受けた時とかのHP
    public int playerAttack = 30;//プレイヤーの攻撃力
    // public int playerSkill = 50;//プレイヤーの必殺攻撃
    public int playerRecover = 50;//プレイヤーHPの回復値
    public string playerName = "旅人";

    bool gameOver;

    SceneChanging sC;

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(this);
        gameOver = true;
    }




    void Start()
    {

        playerHP = playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //if (sC.bossBattleStart == 2 && gameOver == true)
        //{
        //    gameOver = false;
        //    Destroy(this);
        //}


    }
    public void PlayerAttack(EnemyScript enemyScript) //プレイヤーが与えるダメージ
    {
        enemyScript.EnemyDamage(playerAttack);

    }
    public void PlayerDamage(int enemyAttack)//プレイヤーが受けるダメージ
    {
        playerHP -= enemyAttack;

    }
    public void PlayerRecover()//プレイヤーが回復する
    {
        if (playerHP + playerRecover <= playerMaxHP)//最大HPを超えないための対策
        {
            playerHP += playerRecover;
        }
        else
        {
            playerHP = playerMaxHP;
        }
    }
    public void PlayerDefence(int enemyAttack)//プレイヤーが防御する
    {
        playerHP -= (enemyAttack / 2);
    }





    //  public void PlayerSkill(EnemyScript enemyHP);//特殊攻撃
    //  {
    //      enemyHP -= playerSkill;
    //  }


}
