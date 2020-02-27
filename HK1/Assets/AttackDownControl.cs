using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDownControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "littleenemy")
        {
            GetComponentInParent<CharacterControl>().AttackDamage();

        }
        else if (collision.gameObject.tag == "trap")
        {
            GetComponentInParent<CharacterControl>().move.y = 1;
            GetComponentInParent<CharacterControl>().gravity = 0;
            GetComponentInParent<CharacterControl>().AttackJump();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
