using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallControl : MonoBehaviour
{
    int dir;
    public AudioSource attackAudio;
    IEnumerator FireBOOM()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            attackAudio.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireBOOM());
        if(transform.rotation.y==0)
        {
            dir = -1;//左
        }
        else 
        {
            dir = 1;//右
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(30*Time.deltaTime*dir,0,0);
    }
}
