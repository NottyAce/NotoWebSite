using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    int movePow = 1;
    [SerializeField]
    int jumpPow = 1;
    [SerializeField]
    GameObject fuwatto;
    Rigidbody2D playerRigid;
    Transform playerTrans;
    Animator fuwattoAnima;
    Transform fuwattoTrans;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerTrans = player.GetComponent<Transform>();
        fuwattoAnima = fuwatto.GetComponent<Animator>();
        fuwattoTrans = fuwatto.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * movePow,playerRigid.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && !(playerRigid.velocity.y < -0.5f)){
            Jump();
        }

        //if () { 
        
        //}
    }
    private void FixedUpdate()
    {
        
    }

    void Jump() {
        playerRigid.AddForce(Vector2.up * jumpPow, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "KiraNoYatsu")
            {
                SceneManager.LoadScene("KiraNoYatsu");
            }
            else if (sceneName == "FuwattoBall")
            {
                SceneManager.LoadScene("FuwattoBall");
            }
            else { 
            
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if ((collision.gameObject.name == "FuwattoGround") && (Input.GetAxis("Horizontal") != 0.0f || Input.GetKeyDown(KeyCode.UpArrow) != false)) {
        //    player.GetComponent<Transform>().position.y = fuwattoTrans.position.y;
        //} 
    }
}
