using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanToTheStartChanging : MonoBehaviour {

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool change = GameObject.Find("CanvasChanging").GetComponent<CanvasChanging>().change;
        Debug.Log(change);

        if (change == false && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("ComeOn");
            SceneManager.LoadScene("scee1");
        }


	}
}
