using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour {

    PlayerStatus pS;//プレイヤーインスタンス生成
    BossStatus bS;//ボスインスタンス生成
    BossBattleText bBT;//テキストインスタンス生成
    public BattleMode battleMode;//バトルの進行状況を格納するインスタンスを生成

    GameObject pointer;
    PointerControler pC;

    private int command;//矢印制御で使用    
    
    // Use this for initialization
	void Start () {
        pS = GameObject.Find("Player").GetComponent<PlayerStatus>();//プレイヤー情報の取得
        bS = GameObject.Find("Boss").GetComponent<BossStatus>();//ボス情報の取得
        bBT = GameObject.Find("BossBattleText").GetComponent<BossBattleText>();//ボステキストの取得
        pointer = GameObject.Find("pointer");//矢印取得
        pC = pointer.GetComponent<PointerControler>();

        StartCoroutine(BossBattleProgram());
    }

    // Update is called once per frame
    void Update () {



    }
    string nothing;

    public IEnumerator BossBattleProgram()
    {
        pointer.SetActive(false);
        battleMode = BattleMode.Appearance;
        bBT.TextMessage(0, nothing, 0, 0, 0, 0);//ラスボス戦突入
        while (!Input.GetKeyDown(KeyCode.Return))
        { yield return null; }
        yield return new WaitForSeconds(0.1f);

        while (pS.playerHP > 0 && bS.bossHP > 0)
        {
            battleMode = BattleMode.BattleChoice;
            bBT.TextMessage(0, nothing, 0, 0, 0, 0);//攻撃，防御，回復選択
            pC.PointerMovings(command);//矢印関数呼び出し
            command = 0;
            while (!Input.GetKeyDown(KeyCode.Return))
            {
                command = pC.PointerMovings(command);
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);

            if (command == 0)
            {
                pointer.SetActive(false);//矢印消滅
                battleMode = BattleMode.PlayerAttack;//プレイヤーが攻撃
                bBT.TextMessage(pS.playerAttack, pS.playerName, 0, 0, 0, 0);
                bS.BossDamage();
                while (!Input.GetKeyDown(KeyCode.Return))
                { yield return null; }
                yield return new WaitForSeconds(0.1f);
            }

            if (command == 1)
            {
                pointer.transform.Translate(0, 60, 0);
                pointer.SetActive(false);//矢印消滅
                battleMode = BattleMode.PlayerDefence;//プレイヤーが防御
                bBT.TextMessage(0, pS.playerName, 0, 0, 0, 0);
                while (!Input.GetKeyDown(KeyCode.Return))
                { yield return null; }
                yield return new WaitForSeconds(0.1f);
                command = 3;
            }

            if (command == 2)
            {
                pointer.transform.Translate(0, 120, 0);
                pointer.SetActive(false);//矢印消滅
                battleMode = BattleMode.PlayerRecover;//プレイヤーがHP回復
                bBT.TextMessage(0, pS.playerName, pS.playerRecover, 0, 0, 0);
                pS.PlayerRecover();

                while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
                yield return new WaitForSeconds(0.1f);
            }
        
            int bossChoice;//ボスが行動選択に使う数字
            if (bS.bossHP > 0 && pS.playerHP > 0)
            {
                battleMode = BattleMode.BossAction;//ボスが行動選択
                bBT.TextMessage(0, nothing, 0, 0, 0, 0);
                bossChoice = Random.Range(1, 1001);//完成ver
                //bossChoice = Random.Range(1,1000);//クリティカル無いver
                //bossChoice = 1000;
                //bossChoice = 334;//ずっと様子見るver
                //bossChoice = 1;//ずっと攻撃するver

                while (!Input.GetKeyDown(KeyCode.Return))
                { yield return null; }
                yield return new WaitForSeconds(0.1f);

                if (bossChoice <= 333 && command != 3)
                {
                    battleMode = BattleMode.BossAttack;//ボス通常攻撃
                    bBT.TextMessage(0, nothing, 0, bS.bossAttack, 0, 0);
                    bS.BossAttack();
                    while (!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }

                if (bossChoice == 1000)
                {
                    battleMode = BattleMode.BossSpecialAttack;//ボスクリティカルヒット
                    bBT.TextMessage(0, nothing, 0, 0, bS.bossSpecialAttack, 0);
                    while (!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);

                    battleMode = BattleMode.PlayerSpecialDamage;
                    bBT.TextMessage(0,nothing,0,0,bS.bossSpecialAttack,0);
                    bS.BossSpecialAttack();
                    while (!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }

                if (bossChoice <= 666 && bossChoice > 333)
                {
                    battleMode = BattleMode.BossWaiting;//ボス様子見
                    bBT.TextMessage(0, nothing, 0, 0, 0, 0);
                    while (!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }

                if (bossChoice > 666 && bossChoice != 1000)
                {
                    battleMode = BattleMode.BossRecover;//ボス回復
                    bBT.TextMessage(0, nothing, 0, 0, 0,bS.bossRecover);
                    while (!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }

                if (bossChoice <= 333 && command == 3)//防御時のダメージ
                {
                    battleMode = BattleMode.BossHalfAttack;
                    bBT.TextMessage(0, pS.playerName, 0, bS.bossAttack, 0, 0);
                    bS.BossHalfAttack();
                    while (!Input.GetKeyDown(KeyCode.Return))
                    { yield return null; }
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        if (pS.playerHP > 0 && bS.bossHP <= 0)
        {
            GameObject.Find("Boss").SetActive(false);
            battleMode = BattleMode.BattleFinish;//ラスボス戦終了
            bBT.TextMessage(0, nothing, 0, 0, 0, 0);
            while (!Input.GetKeyDown(KeyCode.Return))
            { yield return null; }
            GameObject.Find("SceneChangingFromBossToEpilogue").GetComponent<SceneChangingFromBossToEpilogue>().sceneMoving = 1;
            yield break;
        }

        if (pS.playerHP <= 0 && pS.playerHP >= -80000)
        {
            battleMode = BattleMode.GameOver;//ゲームオーバー
            bBT.TextMessage(0,pS.playerName,0,0,0,0);
            while (!Input.GetKeyDown(KeyCode.Return))
            { yield return null; }
            GameObject.Find("SceneChangingFromBossToEpilogue").GetComponent<SceneChangingFromBossToEpilogue>().sceneMoving = 2;
            yield break;
        }

        if (pS.playerHP < -80000)
        {
            battleMode = BattleMode.PlayerLost;//ゲームオーバー木っ端微塵ver
            bBT.TextMessage(0, pS.playerName, 0, 0, 0, 0);
            while (!Input.GetKeyDown(KeyCode.Return))
            { yield return null; }
            GameObject.Find("SceneChangingFromBossToEpilogue").GetComponent<SceneChangingFromBossToEpilogue>().sceneMoving = 2;
            yield break;
        }
    }
}






