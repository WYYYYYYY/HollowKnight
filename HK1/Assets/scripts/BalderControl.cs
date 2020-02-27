using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalderControl : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask playerMask;
    public Animator balderAnim;
    public AudioSource openAudio;
    public AudioSource closeAudio;
    public AudioSource spitAudio;
    public int spitTimeCold;
    void Start()
    {
        playerMask = LayerMask.GetMask("player");
        balderAnim = GetComponent<Animator>();
        spitTimeCold = 0;
    }

    void death()
    {
        balderAnim.ResetTrigger("deathtrigger");
        GetComponent<CircleCollider2D>().enabled = false;
    }
    void SpitTime()
    {
        spitTimeCold++;
    }

    void SpitExit()
    {
        balderAnim.ResetTrigger("spittrigger");
        spitTimeCold = 0;
    }

    void SpitSound()
    {
        spitAudio.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            balderAnim.SetBool("isidle", false);
            balderAnim.SetBool("isdefense", true);
            closeAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            balderAnim.SetBool("isidle", true);
            balderAnim.SetBool("isdefense", false);
            openAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spitTimeCold>3)
        {
            balderAnim.SetTrigger("spittrigger");
        }
    }
}
