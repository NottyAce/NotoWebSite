using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    GameObject RightEnemy;
    GameObject LeftEnemy;

    float startTime;

    public int finish;//戦闘終了のシグナル

    public GameObject player;

    public GameObject[] Train = new GameObject[4];
    private RightEnemyHPGage rightEnemy;
    private LeftEnemyHPGage leftEnemy;

    private tekisutotesuto enemyScript;

    bool isActive;
    bool timeRoad;
    bool musicActive;
    public bool change;

    public int battleStart;
    int battlenumber;
    private Mode GameMode;

    // Use this for initialization
    void Start()
    {
            startTime = Time.time;
            isActive = true;
            timeRoad = true;
        //  enemyAppearance = GameObject.Find("person").GetComponent<BaseCharacterController>().enemyAppearance;//戦闘画面移行時間の受け渡し

        RightEnemy = GameObject.Find("RightEnemy");
        LeftEnemy = GameObject.Find("LeftEnemy");
        change = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(change);
        if (timeRoad)
        {
            //battleStart = 1;
            battleStart = Random.Range(10, 66);//敵出現までの時間

            Debug.Log(battleStart);
            timeRoad = false;
        }

        if (change == true)
        {
            if ((Time.time - startTime) >= battleStart)
            {
                GameObject.Find("MusicControler").GetComponent<AudioSource>().enabled = true;//戦闘BGM
                GameObject.Find("MusicControler").GetComponent<MusicControl>().MusicPlay();//戦闘BGM
            }

            if ((Time.time - startTime) >= battleStart && isActive == true)
            {
                isActive = false;
                GameObject.Find("CanvasBackground").GetComponent<Canvas>().enabled = true;//戦闘画面開始
                GameObject.Find("CanvasBattle").GetComponent<Canvas>().enabled = true;//戦闘画面開始
                GameObject.Find("MusicControler").GetComponent<MusicControl>().SEPlay();//戦闘BGM
                EnemyAppearance();
                //Debug.Log("時間です");
                GameObject.Find("CanvasBattle/Text").GetComponent<tekisutotesuto>();
                GameObject.Find("BGMmanager").GetComponent<AudioSource>().Pause();//マップBGM中断
                GameObject.Find("person").GetComponent<Rigidbody2D>().simulated = false;//戦闘中主人公の動きを止める
                finish = GameObject.Find("CanvasBattle/Text").GetComponent<tekisutotesuto>().finish;//戦闘終了のシグナル
                StartCoroutine(GameObject.Find("CanvasBattle/Text").GetComponent<tekisutotesuto>().Battle());
            }

            if (finish == 1)//戦闘終了のプロセス
            {
                finish = 2;
                GameObject.Find("CanvasBattle").GetComponent<Canvas>().enabled = false;//スクリーン消滅
                GameObject.Find("CanvasBackground").GetComponent<Canvas>().enabled = false;//スクリーン消滅
                GameObject.Find("MusicControler").GetComponent<AudioSource>().enabled = false;//戦闘BGM終了

                GameObject.Find("BGMmanager").GetComponent<AudioSource>().UnPause();//マップBGM再開 
                startTime = Time.time;
                isActive = true;
                timeRoad = true;

                GameObject.Find("person").GetComponent<Rigidbody2D>().simulated = true;//戦闘終了後主人公を動けるようにする


                //GameObject.Find("Text").GetComponent<tekisutotesuto>().GameMode = Mode.Map;

            }
        }

    }

    public void EnemyAppearance()
    {
        int number = Random.Range(0, 4);//配列の要素0～3を宣言
        transform.position = new Vector3(player.transform.position.x+3, player.transform.position.y, 2);//敵一体目
        GameObject RightEnemy = Instantiate(Train[number], transform.position, transform.rotation); // 敵オブジェクトを生成。+enemyAに格納。
        //RightEnemy = Train[number];
        enemyScript = GameObject.Find("Text").GetComponent<tekisutotesuto>();
        enemyScript.enemy[0] = RightEnemy;


        number = Random.Range(0, 4);//敵二体目
        GameObject LeftEnemy = Instantiate(Train[number], new Vector3(transform.position.x - 9, transform.position.y, transform.position.z), transform.rotation); // 敵オブジェクトを生成
        //LeftEnemy = Train[number];
        enemyScript = GameObject.Find("Text").GetComponent<tekisutotesuto>();
        enemyScript.enemy[1] = LeftEnemy;

    }


}





