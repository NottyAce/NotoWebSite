using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    Text CommandText;
    bool textGo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextColoutine());
    }

    IEnumerator TextColoutine() {
        while (!Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("特急サンダーバード");
            CommandText.text = "特急サンダーバードが現れた！";
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        CommandText.text = "1.戦う\n2.乗る\n3.逃げる";
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("RPGBattle");
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
