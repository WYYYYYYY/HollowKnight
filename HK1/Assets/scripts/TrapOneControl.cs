using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOneControl : MonoBehaviour
{
    public float blankTime;
    float caculateTime;
    public Animator trapOneAnim;
    // Start is called before the first frame update
    void Start()
    {
        caculateTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(caculateTime<blankTime)
        {
            caculateTime += Time.deltaTime;
        }
        else
        {
            trapOneAnim.Play("");
            caculateTime = 0;
        }
    }
}
