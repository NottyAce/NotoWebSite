using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string stageSceneName;//ゲームのシーンの名前

    [SerializeField]
    float disturbTime;//時間に邪魔風船がよって増える時間間隔
    [SerializeField]
    float balloonTime;//風船が1ブロック上がるのにかかる時間
    [SerializeField]
    float timePer;//1ブロック上がる時間が短くなる比率(0 < timePer < 1の少数)
    [SerializeField]
    float playingTime;//ゲームスタートから終了までの時間

    [SerializeField]
    GameObject BalloonManager;//バルーンマネージャーのインスタンス
    [SerializeField]
    GameObject Result;

    [SerializeField]
    bool gameScene;


    Balloon_Manager balloonManager;
    string sceneName;//シーンがメインか否かという判定のための文字列
    bool game;//ゲームスタートのbool
    bool putEnd;//Get_trigger()で風船を置き終わっているかを取る変数
    bool putBalloon;//PutBalloon(bool) で新しい風船を置くbool値,falseならゲームオーバー
    int disturbBallonCount;//Create_disturb(int),Add_disturb(int)に渡す邪魔風船を配置する数
    float gameTime1;//風船を上げる時に使う時間
    float gameTime2;//邪魔風船を生成する時に使う時間
   // static private GameManager singleToneInstance;//シングルトン


    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;//スタートのタイミングでシーン名を参照

        //if (singleToneInstance == null)
        //{
        //    singleToneInstance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else if (gameScene == true)
        //{
        //    if (sceneName == stageSceneName)
        //    {
        //        singleToneInstance = this;
        //        DontDestroyOnLoad(gameObject);
        //        GameObject DestroyObject = GameObject.Find("GameManager");
        //        if (DestroyObject.GetComponent<GameManager>().gameScene == false)
        //        {
        //            Destroy(DestroyObject);
        //        }
        //    }   
        //}
        //else
        //{
        //    GameObject DestroyObject = GameObject.Find("GameManager");
        //    if (DestroyObject.GetComponent<GameManager>().gameScene == true)
        //    {
        //        Destroy(DestroyObject);
        //    }
        //    singleToneInstance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sceneName == stageSceneName)
        {
            Result.SetActive(false);
            game = true;//ゲームシーンならばゲームの処理をUpDateでスタートさせるためのbool値
            balloonManager = BalloonManager.GetComponent<Balloon_Manager>();
        }
        else {
            game = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (game) {
            playingTime = Time.time;
            balloonTime = GameController(balloonTime,playingTime); 
        }
    }

    private void OnApplicationPause(bool pause)//アプリの中断
    {
        if (pause == true)
        {

        }
        else
        {

        }
    }

    protected void OnApplicationQuit()//アプリの終了
    {
        Debug.Log("OnApplicationQuit呼ばれたよ～");
    }

    public float GameController(float balloonTime,float playingTime)
    {//ゲームプレイ中の処理を記載 

        gameTime1 += Time.deltaTime;
        gameTime2 += Time.deltaTime;

        if (balloonManager.Get_trigger() == true)//風船がメインレーンを移動中でなければ風船を置くという処理
        {//Putballoonが風船を新しく置いたうえでtrueを返すという処理だったこと,
         //風船を置くための判定がGet_trigger()の戻り値だったことからこのように記述

            if (putBalloon == true) {//2個目以降の風船を置く時のみ,風船のスピードアップが必要。
                //この場所であれば,風船のスピードアップのタイミングを正しく取れるため,ここに条件文を記述

                balloonTime = balloonTime * timePer;//風船の速度を増加
                balloonManager.Create_disturb(disturbBallonCount);//邪魔風船をメインレーンに配置
            }
            putBalloon = balloonManager.PutBalloon(true);
        }

        if (putBalloon)//ゲーム続行中
        {
                if (gameTime1 > balloonTime)//時間経過で風船が上昇
                {
                    balloonManager.Up();
                    gameTime1 = 0;
                }

                //邪魔風船を追加
                if (gameTime2 > disturbTime)
                {
                    disturbBallonCount = (int)(playingTime / disturbTime);
                    balloonManager.Add_disturb(disturbBallonCount);//邪魔風船をストックに配置
                    gameTime2 = 0;
                }
        }
        else if (putBalloon == false || balloonManager.Get_gameover() == true)
        {
            //ゲームオーバーでスコアの処理へ
            Result.SetActive(true);
            game = false;
            Debug.Log("ゲーム稼働中");
         }
        else
        {
           Debug.Log("ありえない選択肢に入っちゃってるよ～");
        }

        return balloonTime;
    }
}
