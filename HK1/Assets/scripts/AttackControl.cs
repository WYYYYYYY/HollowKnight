using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="littleenemy")
        {
            GetComponentInParent<CharacterControl>().AttackDamage();
            
        }else if(collision.gameObject.tag=="enemy")
        {
            GetComponentInParent<CharacterControl>().AttackReject();
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
