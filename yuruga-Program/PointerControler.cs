using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerControler : MonoBehaviour
{

    GameObject pointer;



    // Use this for initialization
    void Start() {
        pointer = GameObject.Find("pointer");
    }

    // Update is called once per frame
    void Update() {

    }

    //public int PointerMoving(int command)//矢印選択　二択ver
    //{
    //    pointer.SetActive(true);
    //    if (Input.GetKeyDown(KeyCode.DownArrow) && command == 0)
    //    {
    //        transform.Translate(0, -32, 0);
    //        command++;
    //    }
    //    if (Input.GetKeyDown(KeyCode.UpArrow) && command == 1)
    //    {
    //        transform.Translate(0, 32, 0);
    //        command--;
    //    }
    //    return command;
    //}

    public int PointerMovings(int command)//矢印選択　三択ver
    {
        pointer.SetActive(true);
        if (Input.GetKeyDown(KeyCode.DownArrow) && command < 2)
        {
            pointer.transform.Translate(0, -60, 0);
            command++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && command > 0)
        {
            pointer.transform.Translate(0, 60, 0);
            command--;
        }

        return command;
    }

}



