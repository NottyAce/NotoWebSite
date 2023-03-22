using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField]
    GameObject Victory;
    bool finish = false;
    bool playerDeath = false;

    // Start is called before the first frame update
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NextStage();
        if (this.playerDeath)
        {
            GameRestart();
        }
    }

    public void setVictory(bool finish) {
        this.finish = finish;
    }

    public void setPlayerDeath(bool playerDeath) {
        this.playerDeath = playerDeath;
    }

    void GameRestart()
    {
        playerDeath = false;
        SceneManager.LoadScene("SampleScene");
    }

    void NextStage() {
        if (finish == true)
        {
            finish = false;
            //SceneManager.LoadScene("���̃X�e�[�W");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
