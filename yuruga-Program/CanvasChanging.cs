using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChanging : MonoBehaviour {

    public bool change;

    // Use this for initialization
	void Start () {
        change = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Dialogue/Image/Conversation").GetComponent<Talk>().fin == 1 && change == true)
        {
            change = false;
            GameObject.Find("FinScrean").GetComponent<Canvas>().enabled = true;
        }
    }
}
