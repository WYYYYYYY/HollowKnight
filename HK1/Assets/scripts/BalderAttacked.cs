using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalderAttacked : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fireball")
        {
            health--;
            if(health==0)
            {
                gameObject.GetComponentInParent<Animator>().SetTrigger("deathtrigger");
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponentInParent<Animator>().SetTrigger("hurttrigger");
            }
        }
    }
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
