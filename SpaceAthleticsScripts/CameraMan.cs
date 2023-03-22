using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject planet;

    public Vector3 normalVector;//法線ベクトルのインスタンス
    public Vector3 planeVector;//正面方向のベクトルのインスタンス

    GravityController gravityController = new GravityController();

    // Use this for initialization
    void Start () {
        gravityController = planet.GetComponent<GravityController>();
        planeVector = player.GetComponent<CharacterForce>().planeVector;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = player.transform.position;//プレイヤーの座標に追従
        normalVector = gravityController.normalVector;//移動後の法線ベクトルを取得
        planeVector = Vector3.back;//カメラ用の空オブジェクトの正面ベクトルを代入

        planeVector = CameraStandingManager(planeVector, normalVector);//姿勢制御後の正面ベクトルを代入
    }

    private Vector3 CameraStandingManager(Vector3 planeVector, Vector3 normalVector)//姿勢制御
    {
        //移動前の正面ベクトルと移動後の法線ベクトルから移動後の正面ベクトルを生成
        planeVector = Vector3.ProjectOnPlane(planeVector, normalVector);
        //Quaternionで↑の正面ベクトルに合う姿勢に空オブジェクトを回転
        transform.rotation = Quaternion.LookRotation(planeVector, normalVector);
        return planeVector;//移動後の正面ベクトルを戻り値として返す
    }
}
