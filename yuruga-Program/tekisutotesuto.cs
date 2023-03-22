using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public enum Mode//用意
{
    Encount, //遭遇
    Action,  //戦闘か逃走選択画面
    Battle,  //攻撃か防御選択画面
    Escape,  //逃走
    Map,//マップに戻る
    Attack, //攻撃
    AttackChoose,//攻撃する敵を選択
    FoolishAttack,//アホな攻撃
    Defence, //防御
    Recover,//回復
    EnemyAttack, //敵のターン
    EnemyHalfAttack, //防御時の敵のターン
    Result,       //敵を倒した
    Gameover//ゲームオーバー
}

public class tekisutotesuto : MonoBehaviour
{

    // public int[] enemyAttack;
    public GameObject[] enemy = new GameObject[2];
    string playerName;


    public EnemyScript[] enemyScripts = new EnemyScript[2];

    public Mode GameMode;       //バトルの進行状態を格納するインスタンス
    GameObject pointer;
    private PlayerStatus pS;

    public int finish;//戦闘終了の合図
    // Use this for initialization
    void Start()
    {
        pS = GameObject.Find("Player").GetComponent<PlayerStatus>();//プレイヤーステイタスの取得
        playerName = GameObject.Find("Player").GetComponent<PlayerStatus>().playerName;//プレイヤーの名前の取得

        pointer = GameObject.Find("pointer");//矢印取得
                                             // enemy01 = GameObject.Find("EnemyADot");//敵情報の取得
        pointer.SetActive(false);//矢印消去
        GameMode = Mode.Map;        //状況の初期化
        //StartCoroutine(Battle());       //バトル開始

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int KeyUpDown(int nowcom)//矢印二択関数
    {
        pointer.SetActive(true);
        int com = nowcom;    //選択肢ID　矢印移動（二択）

        if (Input.GetKeyDown(KeyCode.DownArrow) && com < 1)//矢印が上ver
        {
            GameObject.Find("pointer").transform.Translate(0, -64, 0);
            com++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && com > 0)//矢印が下ver
        {
            GameObject.Find("pointer").transform.Translate(0, 64, 0);
            com--;
        }
        return com;
    }

    public int KeyUpDowns(int nowcom)//矢印三択関数
    {
        // GameObject.Find("pointer").transform.Translate(0, 18, 0);
        int com = nowcom;//選択肢ID　矢印移動（三択）
        if (Input.GetKeyDown(KeyCode.DownArrow) && com < 2)//下に下がるとき
        {
            GameObject.Find("pointer").transform.Translate(0, -60, 0);
            com++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && com > 0)//上に上がるとき
        {
            GameObject.Find("pointer").transform.Translate(0,60, 0);
            com--;
        }
        return com;
    }

    public void TextEdit(int playerAttack,int playerRecover, int enemyAttack, int enemyHalfAttack)//条件分岐　テキスト表示画面
    {
        switch (GameMode)
        {
            case Mode.Map:
                this.GetComponent<Text>().text = "敵が現れた！";
                break;
            case Mode.Action:
                this.GetComponent<Text>().text = "戦う\n逃げる";
                break;
            case Mode.Battle:
                this.GetComponent<Text>().text = "攻撃\n防御\n回復";
                break;
            case Mode.Escape:
                this.GetComponent<Text>().text = playerName + "は逃げ出した・・・";
                break;
            case Mode.AttackChoose:
                this.GetComponent<Text>().text = "右の敵\n左の敵";
                break;
            case Mode.Attack:
                this.GetComponent<Text>().text = playerName + "の攻撃\n" + playerAttack + "のダメージを与えた。";
                break;
            case Mode.FoolishAttack:
                this.GetComponent<Text>().text = playerName + "は気が狂って\n何もない所に攻撃した．．．";
                break;
            case Mode.Defence:
                this.GetComponent<Text>().text = playerName + "は防御した";
                break;
            case Mode.Recover:
                this.GetComponent<Text>().text = playerName + "は回復の呪文を使った\n"+"HPが"+playerRecover + "回復した。";
                break;
            case Mode.EnemyAttack:
                this.GetComponent<Text>().text = "敵の攻撃\n" + enemyAttack + "のダメージを受けた！";
                break;
            case Mode.EnemyHalfAttack:
                this.GetComponent<Text>().text = "敵の攻撃\n" +enemyHalfAttack + "のダメージを受けた！";
                break;
            case Mode.Result:
                this.GetComponent<Text>().text = "敵を倒した。\nHPが30，攻撃が10 \n回復力が15上がった。";
                break;
            case Mode.Gameover:
                this.GetComponent<Text>().text = playerName + "は倒れた・・・";
                break;
            default:
                break;
        }
    }

    public IEnumerator Battle()
    {
        GameMode = Mode.Map;
        //敵出現画面
        TextEdit(0,0,0,0);
        while (!Input.GetKeyDown(KeyCode.Return))
        { yield return null; }
        yield return new WaitForSeconds(0.1f);

        //①戦闘・逃走選択画面に移行
        GameMode = Mode.Action;
        TextEdit(0,0,0,0);

        while (pS.playerHP > 0 && (enemy[0].GetComponent<EnemyScript>().enemyHP > 0 || enemy[1].GetComponent<EnemyScript>().enemyHP > 0))
        {
            //①戦闘・逃走選択画面表示
            GameMode = Mode.Action;
            TextEdit(0,0,0,0);
            pointer.SetActive(true);
            int command = 0;
            while (!Input.GetKeyDown(KeyCode.Return))
            {
                command = KeyUpDown(command);
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);



            if (command == 0)//攻撃を選択，②-A 攻撃・防御・回復選択画面に移行
            {
                GameMode = Mode.Battle;
                TextEdit(0,0,0,0);
                // while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                //yield return new WaitForSeconds(0.1f);

                //ここから攻撃or防御or回復の選択
                command = 0;
                while (!Input.GetKeyDown(KeyCode.Return))
                {
                    command = KeyUpDowns(command);//三択関数導入
                    yield return null;
                }
                yield return new WaitForSeconds(0.1f);

                if (command == 0)//敵選択画面に移動
                {
                    GameMode = Mode.AttackChoose;
                    TextEdit(0,0,0,0);
                    while (!Input.GetKeyDown(KeyCode.Return))
                    {
                        command = KeyUpDown(command);//二択関数導入
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.1f);


                    if ((command == 0 && enemy[0].GetComponent<EnemyScript>().enemyHP <= 0) || (command == 1 && enemy[1].GetComponent<EnemyScript>().enemyHP <= 0))//アホな攻撃
                    {
                       // Debug.Log("右の敵" + enemy[0].GetComponent<EnemyScript>().enemyHP);
                       // Debug.Log("左の敵" + enemy[1].GetComponent<EnemyScript>().enemyHP);


                        if (command == 0) 
                        {
                            pointer.SetActive(false);
                        }

                        if (command == 1)
                        {
                            pointer.transform.Translate(0, 64, 0);
                            pointer.SetActive(false);
                        }
                        command = 4;//他の選択肢を回避
                        GameMode = Mode.FoolishAttack;
                        TextEdit(0, 0, 0, 0);
                        while (!Input.GetKeyDown(KeyCode.Return))
                        { yield return null; }
                        yield return new WaitForSeconds(0.1f);
                    }

                    if (command == 0 && enemy[0].GetComponent<EnemyScript>().enemyHP > 0)//右の敵を選択
                    {
                        pointer.SetActive(false);//矢印消滅
                        GameMode = Mode.Attack;//③-A 攻撃画面に移行
                        TextEdit(pS.playerAttack,0,0,0);
                        GameObject.Find("Player").GetComponent<PlayerStatus>().PlayerAttack(enemy[0].GetComponent<EnemyScript>());
                        while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                        yield return new WaitForSeconds(0.1f);

                        if (enemy[0].GetComponent<EnemyScript>().enemyHP <= 0)//敵のHPが０ならば敵消滅
                        {
                            //Destroy(enemy[0]);
                            GameObject en = enemy[0];
                            //enemy[0] = null;
                            //Destroy(en);
                            enemy[0].SetActive(false);
                            //enemy[0] = null;
                        }

                    }

                    if (command == 1 && enemy[1].GetComponent<EnemyScript>().enemyHP > 0)//左の敵を選択
                    {
                        command--;
                        GameObject.Find("pointer").transform.Translate(0, 64, 0);
                        pointer.SetActive(false);//矢印消滅
                        GameMode = Mode.Attack;//③-A 攻撃画面に移行
                        TextEdit(pS.playerAttack,0,0,0);
                        GameObject.Find("Player").GetComponent<PlayerStatus>().PlayerAttack(enemy[1].GetComponent<EnemyScript>());
                        while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                        yield return new WaitForSeconds(0.1f);
                        if (enemy[1].GetComponent<EnemyScript>().enemyHP <= 0)//敵のHPが０ならば敵消滅
                        {
                            enemy[1].SetActive(false);
                            //enemy[1] = null;
                            //Destroy(enemy[1]);
                        }
                    }

                    

                }
                if (command == 1)
                {
                    GameObject.Find("pointer").transform.Translate(0, 60, 0);
                    pointer.SetActive(false);//矢印消滅
                                             //③-B 防御画面に移行
                    GameMode = Mode.Defence;
                    TextEdit(0,0,0,0);
                    command = 3;
                    while(!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);

                }
                if(command == 2)
                {
                    GameObject.Find("pointer").transform.Translate(0, 120, 0);
                    pointer.SetActive(false);//矢印消滅
                    GameMode = Mode.Recover;
                    TextEdit(0,pS.playerRecover,0,0);
                    pS.PlayerRecover();
                    while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }

                //while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                //yield return new WaitForSeconds(0.1f);



                //通常ダメージ
                if (enemy[0].GetComponent<EnemyScript>().enemyHP > 0 && command  != 3 && pS.playerHP > 0)
                { //⑤敵の攻撃画面に移行
                    GameMode = Mode.EnemyAttack;
                    TextEdit(0,0,enemy[0].GetComponent<EnemyScript>().enemyAttack,0);
                    enemy[0].GetComponent<EnemyScript>().EnemyAttack(pS);
                    while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }
                if (enemy[1].GetComponent<EnemyScript>().enemyHP > 0 && command != 3 && pS.playerHP > 0)
                {
                    GameMode = Mode.EnemyAttack;
                    TextEdit(0,0,enemy[1].GetComponent<EnemyScript>().enemyAttack,0);
                    enemy[1].GetComponent<EnemyScript>().EnemyAttack(pS);
                    while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }

                //防御時のダメージ
                if (enemy[0].GetComponent<EnemyScript>().enemyHP > 0 && command == 3 && pS.playerHP >0)
                {
                    GameMode = Mode.EnemyHalfAttack;
                    //Debug.Log(enemy[0].GetComponent<EnemyScript>().enemyAttack);
                    TextEdit(0,0,0, enemy[0].GetComponent<EnemyScript>().enemyAttack/2);
                    pS.PlayerDefence(enemy[0].GetComponent<EnemyScript>().enemyAttack);
                    while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                    yield return new WaitForSeconds(0.1f);

                }

                if (enemy[1].GetComponent<EnemyScript>().enemyHP > 0 && command == 3 && pS.playerHP > 0)
                {
                    GameMode = Mode.EnemyHalfAttack;
                    TextEdit(0,0,0, enemy[1].GetComponent<EnemyScript>().enemyAttack / 2);
                    pS.PlayerDefence(enemy[1].GetComponent<EnemyScript>().enemyAttack);
                    while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }
            }
            if(command == 1)
            {
                //②-B 逃走画面に移行
                for (int n = 0; n < 2; n++)
                {
                    Destroy(enemy[n]);
                }

                GameMode = Mode.Escape;
                GameObject.Find("pointer").transform.Translate(0, 64, 0);
                pointer.SetActive(false);//矢印消去
                TextEdit(0,0,0,0);
                while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                yield return new WaitForSeconds(0.1f);
                GameObject.Find("GameManagemant").GetComponent<GameManager>().finish = 1;//戦闘終了①
                yield break;
            }
        }
        //敵のHPが0になれば
        if (enemy[0].GetComponent<EnemyScript>().enemyHP <= 0 && enemy[1].GetComponent<EnemyScript>().enemyHP <= 0)
        {
            //⑥-A戦闘終了
            for (int n = 0; n < 2; n++)
                Destroy(enemy[n]);

            GameMode = Mode.Result;
            TextEdit(0,0,0,0);
            pS.playerMaxHP += 30;
            pS.playerHP = pS.playerMaxHP;
            pS.playerAttack += 10;
            pS.playerRecover += 15;
            while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
            GameObject.Find("GameManagemant").GetComponent<GameManager>().finish = 1;//戦闘終了②
        }

        //自分のHPが0になれば
        if (pS.playerHP <= 0)
        {
            //⑥-Bゲームオーバー
            GameMode = Mode.Gameover;
            TextEdit(0,0,0,0);
            while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
            GameObject.Find("SceneChanging").GetComponent<SceneChanging>().bossBattleStart = 2;
            yield break;
        }
    }
}



