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
            Debug.Log("���}�T���_�[�o�[�h");
            CommandText.text = "���}�T���_�[�o�[�h�����ꂽ�I";
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        CommandText.text = "1.�키\n2.���\n3.������";
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
