using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterForce : MonoBehaviour {

    [SerializeField]
    GameObject player;//キャラのインスタンス
    [SerializeField]
    GameObject plane;//正面ベクトル算出用のオブジェクト
    [SerializeField]
    GameObject planet;//惑星
    [SerializeField]
    GameObject SceneManager;
    [SerializeField]
    GameObject Canvas;
    [SerializeField]
    GameObject Canvas1;

    Rigidbody playerRigidbody;//キャラのRigidbodyのインスタンス
    GravityController gravityController = new GravityController();//GraivityControllerクラスのインスタンス生成
    public Vector3 normalVector;//法線ベクトルのインスタンス
    public Vector3 planeVector;//正面方向のベクトルのインスタンス

    [SerializeField]
    float runSpeed;//キャラに加える力
    [SerializeField]
    float lowSpeed;//通常移動
    [SerializeField]
    float highSpeed;//ダッシュ移動
    [SerializeField]
    float jumpPower;//ジャンプの高さ
    [SerializeField]
    float moveSpeed;
    float inputVertical;//コントローラーの前後方向の入力
    float inputHorizontal;//コントローラーの左右方向の入力

    [SerializeField]
    bool ground;//接地しているかの判定
    [SerializeField]
    float jumptime;//三段跳びの判定用の変数
    [SerializeField]
    float UItime;//UIを表示する時間
    float time;
    bool touch;

    private Animator animator;
    bool goSign;//キャラを動かすかどうかの判定用
    bool jumpingFlag = true;//キャラのジャンプ判定

    private AudioSource audioSource;
    private AudioClip audioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start() {
        playerRigidbody = GetComponent<Rigidbody>();//インスタンス格納
        gravityController = planet.GetComponent<GravityController>();
        planeVector = plane.transform.position - transform.position;//正面ベクトルを仮代入
        animator = GetComponent<Animator>();
        Canvas.SetActive(false);
        Canvas1.SetActive(false);
        touch = true;
    }

    // Update is called once per frame
    void Update() {
        normalVector = gravityController.normalVector;//移動後の法線ベクトルを取得
        planeVector = CharacterStandingManager(planeVector, normalVector);//姿勢制御後の正面ベクトルを代入
        InputManager();//コントローラーの入力を取る
        planeVector = CharacterDirection();//キャラの向きを更新した後の正面ベクトルを代入
    }

    private void FixedUpdate()
    {
        CharacterMove(planeVector);//正面ベクトルの方向に力をかけてキャラを移動
        
        CharacterAction();//キャラのアクションを制御
        CharacterMoveAnimation();//キャラのアニメーションを制御

        if (goSign == true && ground == true)//SEを鳴らすタイミングを調整
        {
            audioSource.Play();
        }
    }

    private void InputManager()//コントローラーの入力を取る
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
    }

    private Vector3 CharacterDirection()//キャラの方向を決める
    {
        //カメラからプレイヤーに向かって作成した単位ベクトルをプレイヤーが立つ平面に投影させ，カメラ基準の「前方」成分を作成
        Vector3 controllerBase = Vector3.ProjectOnPlane((transform.position - Camera.main.transform.position).normalized, normalVector);
        //↑で求めた前方方向のベクトルと法線ベクトルとの外積から横移動のベクトルを作成
        Vector3 rightBase = Vector3.Cross(normalVector, controllerBase);

        //コントローラーの傾きに応じてそれぞれの成分にキャラの向きを変える
        //if (inputVertical != 0)
        //{
        //    controllerBase = controllerBase * inputVertical;
        //    transform.rotation = Quaternion.LookRotation((controllerBase + rightBase).normalized, normalVector);
        //    //transform.rotation = Quaternion.LookRotation(controllerBase * inputHorizontal, normalVector);
        //    goSign = true;
        //}
        //if (inputHorizontal != 0)
        //{
        //    rightBase = rightBase * inputHorizontal;
        //    transform.rotation = Quaternion.LookRotation((controllerBase + rightBase).normalized, normalVector);
        //    //transform.rotation = Quaternion.LookRotation(rightBase * inputHorizontal, normalVector);
        //    goSign = true;
        //}

        if (inputVertical != 0 || inputHorizontal != 0)
        {
            controllerBase = controllerBase * inputVertical;
            rightBase = rightBase * inputHorizontal;
            transform.rotation = Quaternion.LookRotation(controllerBase + rightBase, normalVector);
            //transform.rotation = Quaternion.LookRotation(controllerBase * inputHorizontal, normalVector);
            goSign = true;
        }

        //キャラが動いていないときはキャラの向きを変えない
        if (inputHorizontal == 0 && inputVertical == 0)
        {
            goSign = false;
        }

        planeVector = plane.transform.position - transform.position;//正面ベクトルを向きを変えた後に更新
        return planeVector;//更新した正面ベクトルを戻り値として返す
    }

    private Vector3 CharacterStandingManager(Vector3 planeVector,Vector3 normalVector)//姿勢制御
    {
        //移動前の正面ベクトルと移動後の法線ベクトルから移動後の正面ベクトルを生成
        planeVector = Vector3.ProjectOnPlane(planeVector,normalVector);
        //Quaternionで↑の正面ベクトルに合う姿勢にキャラを回転
        transform.rotation = Quaternion.LookRotation(planeVector,normalVector);
        return planeVector;//移動後の正面ベクトルを戻り値として返す
    }

    private void CharacterMove(Vector3 planeVector)//キャラ移動のメソッド
    {
        if (goSign == true)//コントローラが倒されたらture
        {

            if (Input.GetButton("B"))//シフトキーを押すとダッシュ
            {
                moveSpeed = highSpeed;//ダッシュ
            }
            else
            {
                moveSpeed = lowSpeed;
            }

            if (playerRigidbody.velocity.magnitude < moveSpeed)
            {
                playerRigidbody.AddForce(planeVector * highSpeed);//キャラの正面方向に力を加える
            }
            else if (playerRigidbody.velocity.magnitude > moveSpeed)
            {

            }
        }
        //else if (goSign == false && ground == true && playerRigidbody.velocity.magnitude != 0)
        //{
        //    playerRigidbody.AddForce(-planeVector * lowSpeed);
        //}

        
    }

    private void OnCollisionEnter(Collision other)//接地判定01 & ギミック接触判定 &UI表示＋シーン遷移
    {
        if (other.gameObject.tag == "Stage")
        {
            ground = true;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("死にました");
            StartCoroutine(GameOverAppearance());
        }
        else if (other.gameObject.tag == "Goal" && touch == true)
        {
            touch = false;
            Debug.Log("獲ったどー!");
            GameObject.Find("EegiruCount").GetComponent<EegiruCount>().eegiruNum++;
            //Destroy(other.gameObject);
            StartCoroutine(TextAppearance());
        }
    }

    private void OnCollisionExit(Collision other)//接地判定02
    {
        if (other.gameObject.tag == "Stage")
        {
            ground = false;
        }
    }

    private void CharacterAction()//キャラのアクションを制御
    {
        //int jump = 0;
        if (Input.GetButtonDown("A") && ground == true)
        {
            playerRigidbody.AddForce(normalVector * jumpPower);
            animator.SetBool("Jump", true);

            //if (Input.GetButtonDown("A") && ground == false && jump == 0)
            //{
            //    playerRigidbody.AddForce(normalVector * jumpPower);
            //    jump = 1;
            //}
            //else if (time >= jumptime && goSign == true)
            //{
            //    playerRigidbody.AddForce(normalVector * jumpPower * 2);
            //    jump++;
            //}
            //else if (time >= jumptime && goSign == true &&jump == 1)
            //{
            //    playerRigidbody.AddForce(normalVector * jumpPower * 3);
            //    jump = 0;
            //}
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    private void CharacterMoveAnimation()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private IEnumerator TextAppearance()
    {
        Canvas.SetActive(true);
        Debug.Log("テキスト表示");
        yield return null;
        yield return new WaitForSeconds(UItime);
        SceneManager.GetComponent<SceneJump>().SceneJamp();
        yield break;
    }

    private IEnumerator GameOverAppearance()
    {
        Canvas1.SetActive(true);
        Debug.Log("テキスト表示");
        yield return null;
        yield return new WaitForSeconds(UItime);
        SceneManager.GetComponent<SceneJump>().SceneJamp();
        yield break;
    }
}