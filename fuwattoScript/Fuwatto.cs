using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuwatto : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    private Animation fuwattoAnima;
    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        //Debug.Log("Anima");
        //fuwattoAnima.Play();
        anim.SetBool("fuwattoStart",true);
        //for (; ; ) {
        //    Player.transform.position += transform.position;
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            anim.SetBool("fuwattoStart", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
