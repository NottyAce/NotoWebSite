using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour {

    [SerializeField] int nowScene;
    [SerializeField] string nextScene;

    public enum sceneNum { StartScene = 0, SelectScene = 1, MainScene = 2, EndScene = 3, PoseScene = 4 };
    string sceneName;

    private void Awake()
    {

    }


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("A") && nowScene == 0)
        {
            SceneJamp();
        }

        if (Input.GetButtonDown("Start") && nowScene == 2)
        {
            PoseJump();
        }

        if (Input.GetButtonDown("A") && nowScene == 4)
        {
            nowScene = 2;
            SceneManager.UnloadScene("PoseScene");
        }
        else if (Input.GetButtonDown("B") && nowScene == 4)
        {
            nowScene = 1;
            SceneManager.LoadScene("stageSelect");
        }

    }

    public void SceneJamp()
    {
        //var scene = (sceneNum)Enum.ToObject(typeof(sceneNum), nowScene);
        //sceneName = scene.ToString();

        SceneManager.LoadSceneAsync(nextScene);
    }

    public void PoseJump()
    {
        nowScene = 4;

        var scene = (sceneNum)Enum.ToObject(typeof(sceneNum), 4);
        sceneName = scene.ToString();

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
