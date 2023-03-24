using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject player;
    [SerializeField]
    GameObject planet;

    [SerializeField]
    float cameraMinDistance;//カメラとプレイヤーの最大距離
    [SerializeField]
    float cameraMaxDistance;//カメラとプレイヤーの最小距離
    [SerializeField]
    float cameraDistance = 5;//カメラの距離
    [SerializeField]
    float distanceAngle;
    float cameraAngle = 0;//カメラのアングル
    [SerializeField]
    float zoomSpeed;//ズームする際のカメラのスピード
    [SerializeField]
    float cameraSpeed;//横移動する際のカメラのスピード

    float inputHorizontal2;
    float inputVertical2;

    Vector3 normalVector;//プレイヤーに働く法線ベクトル
    Vector3 planeVector;//プレイヤーの平面ベクトル
    Vector3 cameraPlaneVector;//カメラからキャラクターに伸ばしたベクトル

    // Use this for initialization
    void Start() {
   
    }

    // Update is called once per frame
    void Update() {
        ControllerManager();
        CameraMoving();
    }

    private void ControllerManager()//コントローラーの入力を取る
    {
        inputHorizontal2 = Input.GetAxis("Horizontal2");
        inputVertical2 = Input.GetAxis("Vertical2");
    }

    private void CameraMoving()
    {
        if (inputHorizontal2 != 0)
        {
            cameraAngle += cameraSpeed * inputHorizontal2;
            //Debug.Log(cameraAngle);
        }
        //else if (inputHorizontal2 < 0)
        //{
        //    cameraAngle -= cameraSpeed;
        //}

        if (inputVertical2 != 0)
        {
            cameraDistance -= zoomSpeed * inputVertical2;
        }
        //else if (inputVertical2 > 0)
        //{
        //    cameraDistance -= zoomSpeed;
        //}

        CameraTrans();
    }

    private void CameraTrans()
    {
        if (cameraAngle >= 360)
        {
            cameraAngle = 0;
        }
        else if (cameraAngle < 0)
        {
            cameraAngle += 360;
        }

        if (cameraDistance > cameraMaxDistance)
        {
            cameraDistance = cameraMaxDistance;
        }
        else if (cameraDistance < cameraMinDistance)
        {
            cameraDistance = cameraMinDistance;
        }

        transform.localPosition = new Vector3(Mathf.Sin( - cameraAngle / 180 * Mathf.PI) * cameraDistance, /*Mathf.Sin(-cameraAngle / 180 * Mathf.PI) * */cameraDistance, Mathf.Cos(cameraAngle / 180 * Mathf.PI) * cameraDistance);
        transform.rotation = Quaternion.LookRotation(transform.parent.position - transform.position, (transform.parent.position - planet.transform.position) * 10);
    }
}