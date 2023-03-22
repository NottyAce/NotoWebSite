using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleMode
{
    Appearance,
    BattleChoice,
    PlayerAttack,
    PlayerDefence,
    PlayerRecover,
    BossAction,
    BossAttack,
    BossHalfAttack,
    BossSpecialAttack,
    PlayerSpecialDamage,
    BossWaiting,
    BossRecover,
    BattleFinish,
    GameOver,
    PlayerLost
}






public class BossBattleText : MonoBehaviour {

    BossStatus boss;//ボスクラスのインスタンス生成
    BossStatus bossDamage;//ボスクラスのダメージ計算
    PlayerStatus pS;//プレイヤークラスのインスタンス生成
    public BattleMode battleMode;//バトルモードを格納するインスタンス生成

    string textMessage;//テキストの内容
    string playerName;

    int bossHP;//ボスのHP
    int bossAttack;//ボスの攻撃力
    int bossSpecialAttack;//ボスの攻撃力クリティカルヒットver
    int bossRecover;//ボスの回復力

    int playerHP;//プレイヤーHPの取得
    int playerAttack;//プレイヤーの攻撃力の取得
    int playerRecover;//プレイヤーの回復力の取得
    
    // Use this for initialization
	void Start () {
        textMessage = GameObject.Find("BossBattleText").GetComponent<Text>().text;//テキストの取得
        boss = GameObject.Find("Boss").GetComponent<BossStatus>();//ボス情報の取得
        pS = GameObject.Find("Player").GetComponent<PlayerStatus>();//プレイヤー情報の取得

        bossHP = boss.bossHP;//ボスHPの取得
        bossAttack = boss.bossAttack;//攻撃力の取得
        bossSpecialAttack = boss.bossSpecialAttack;//クリティカルヒットを取得
        bossRecover = boss.bossRecover;//回復力の取得

        playerName = pS.playerName ;//プレイヤーの名前の取得
        playerAttack = pS.playerAttack;//プレイヤー攻撃力の取得
        playerRecover = pS.playerRecover;//プレイヤー回復力の取得
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void TextMessage(int playerAttack, string playerName, int playerRecover, int bossAttack, int bossSpecialAttack, int bossRecover)
    {
        battleMode = GameObject.Find("BossBattle").GetComponent<BossBattle>().battleMode;
        switch (battleMode)
        {
            case BattleMode.Appearance:
                GetComponent<Text>().text = "ラスボス\n近所のじじいが現れた！";
                break;
            case BattleMode.BattleChoice:
                GetComponent<Text>().text = "攻撃\n防御\n回復";
                break;
            case BattleMode.PlayerAttack:
                GetComponent<Text>().text = playerName + "の攻撃\n" + playerAttack + "のダメージを与えた" ;
                break;
            case BattleMode.PlayerDefence:
                GetComponent<Text>().text = playerName + "は防御した";
                break;
            case BattleMode.PlayerRecover:
                GetComponent<Text>().text = playerName + "は回復の呪文を使った\n" + "HPが" + playerRecover + "回復した";
                break;
            case BattleMode.BossAction:
                GetComponent<Text>().text = "ボスの行動";
                break;
            case BattleMode.BossAttack:
                GetComponent<Text>().text = "ボスの攻撃\n" + bossAttack + "のダメージを受けた";
                break;
            case BattleMode.BossHalfAttack:
                GetComponent<Text>().text = "ボスの攻撃\n" + bossAttack/2 + "のダメージを受けた";
                break;
            case BattleMode.BossSpecialAttack:
                GetComponent<Text>().text = "ボスの攻撃\nまさかのクリティカルヒット";
                break;
            case BattleMode.PlayerSpecialDamage:
                GetComponent<Text>().text = bossSpecialAttack + "のダメージを受けた!";
                break;
            case BattleMode.BossWaiting:
                GetComponent<Text>().text = "ボスはこちらの様子を伺っている";
                break;
            case BattleMode.BossRecover:
                GetComponent<Text>().text = "ボスは回復の杖を使った\nHPが" + bossRecover + "回復した";
                break;
            case BattleMode.BattleFinish:
                GetComponent<Text>().text = "ついにボスを倒した！！！！！！";
                break;
            case BattleMode.GameOver:
                GetComponent<Text>().text = playerName + "は倒れた・・・";
                break;
            case BattleMode.PlayerLost:
                GetComponent<Text>().text = playerName + "は\n木端微塵になってしまった．．．";
                break;

            default:
                break;
        }
    }
}
