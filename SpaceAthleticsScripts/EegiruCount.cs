using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EegiruCount : MonoBehaviour {

    public int eegiruNum;
    [SerializeField] float maxNum;
    bool count;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "ED")
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (eegiruNum == maxNum)
        {
            SceneManager.LoadScene("ED");
        }
	}
}
