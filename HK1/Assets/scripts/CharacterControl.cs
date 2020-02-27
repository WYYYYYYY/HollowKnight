using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public enum PlayerDir
    {
        Left = -1,
        Right = 1,
    }

    public PlayerDir currentDir;//当前方向
    [Header("速度")]
    public float speed;//速度
    public Vector3 move;//每帧移动
    [Header("重力")]
    public float gravity;//受到的重力
    public bool canGravity;//是否受重力
    public bool isground;//是否在地面
    [Header("距离障碍物的最小距离")]
    public float distance;//距离障碍物的最小距离
    public Vector2 boxsize = new Vector2(0.55f, 1.15f);//盒型碰撞检测射线尺寸
    public bool isJump;
    [Header("跳跃蓄力大小")]
    public float jumpForce;
    public float jumpTime;
    [Header("最长蓄力时间")]
    public float maxJumpTime;
    [Header("冲刺时间")]
    public float RushTime;
    [Header("冲刺冷却时间")]
    public float canRushTime;
    public bool isRush;
    public bool isClimb;
    public bool isclimbJump;
    public AudioSource jumpAduio;
    public AudioSource landAudio;
    public AudioSource climbAudio;
    public AudioSource wallJumpAudio;
    public AudioSource attackAudio;
    public AudioSource rushAudio;
    public AudioSource runAudio;
    public AudioSource startRunAudio;
    public AudioSource fallingAudio;
    public AudioSource wallSlideAudio;
    public AudioSource firaBallAudio;
    public AudioSource damageAudio;
    public AudioSource rejectAudio;
    public List<PolygonCollider2D> atkColliders;//攻击碰撞器
    BoxCollider2D currentatkColliders;
    Animator playeranim;
    public int playerLayerMask;
    public int climbCount;
    bool isAttackJump;
    GameObject theBall;
    IEnumerator RushCoroutine(float time)
    {
        InputManager.getInstance().stopInput();
        canGravity = false;
        isRush = true;
        move.y = 0;
        if(isClimb)
        {
            if (currentDir == PlayerDir.Left)
            {
                transform.Rotate(0, 180, 0);
                currentDir = PlayerDir.Right;
            }
            else if (currentDir == PlayerDir.Right)
            {
                transform.Rotate(0, -180, 0);
                currentDir = PlayerDir.Left;
            }
            playeranim.Play("rush");
            playeranim.SetBool("isclimb", false);
        }
        if (currentDir == PlayerDir.Left)
        {
            move.x = 15 * -1;
        }
        else
        {
            move.x = 15;
        }
        yield return new WaitForSeconds(time);//延迟time秒后执行
        InputManager.getInstance().KeyInit();
        canGravity = true;
        isRush = false;
        canRushTime = 0.3f;
    }

    IEnumerator fireballCoroutine()//复仇之魂
    {
        InputManager.getInstance().stopInput();
        canGravity = false;
        move.y = 0;
        move.x = 0;
        theBall = Instantiate(theBall,transform);
        yield return new WaitForSeconds(0.3f);//延迟time秒后执行
        if(currentDir==PlayerDir.Left)
        {
            transform.position += new Vector3(0.1f,0,0);
        }
        else
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }
        InputManager.getInstance().KeyInit();
        gravity = 50;
        jumpForce = 40;
        canGravity = true;
    }
    IEnumerator ClimpJump()//墙上跳跃
    {
        InputManager.getInstance().stopInput();   //此时不接受其余输入
        canGravity = false;
        isClimb = false;
        climbCount = 0;
        isJump = true;
        playeranim.Play("climbjump");
        playeranim.SetBool("isclimb", false);
        playeranim.SetBool("isjump", true);
        isclimbJump = true;
        if (currentDir == PlayerDir.Left)
        {
            move.x = 8;
        }
        else
        {
            move.x = -8;
        }
        move.y = 8;
        yield return new WaitForSeconds(0.15f);
        InputManager.getInstance().KeyInit();
        canGravity = true;
        isJump = false;
        isclimbJump = false;
    }

    IEnumerator AttackDownJump()
    {
        isAttackJump = true;
        move.y = 40 * Time.deltaTime;
        gravity = 0;
        yield return new WaitForSeconds(0.2f);
        gravity = 50;
        isAttackJump = false;
    }

    public void AttackDamage()
    {
        damageAudio.Play();
        StartCoroutine(AttackCor());
    }

    public void AttackReject()
    {
        rejectAudio.Play();
        StartCoroutine(AttackCor());
    }
    public void AttackJump()
    {
        move.y = 40 * Time.deltaTime;
        rejectAudio.Play();
        StartCoroutine(AttackDownJump());

    }
    IEnumerator AttackCor()//普通左右攻击位移修正
    {
        InputManager.getInstance().stopInput();
        if (currentDir == PlayerDir.Left)
        {
            transform.position += new Vector3(0.5f,0,0);
        }
        else
        {
            transform.position -= new Vector3(0.5f, 0, 0);
        }
        yield return new WaitForSeconds(0.1f);
        InputManager.getInstance().KeyInit();
    }

    void LRmove()//左右帧移动
    {
        float dir;
        if (Input.GetKey(InputManager.getInstance().moveLeftKey)&& !Input.GetKey(InputManager.getInstance().moveRightKey))
        {
            dir = -1;
            if (isClimb&&currentDir==PlayerDir.Right)
            {
                transform.Rotate(0, -180, 0);
                currentDir = PlayerDir.Left;
                playeranim.SetBool("isclimb", false);
                playeranim.SetBool("isfalling",true);
            }
        }
        else if (Input.GetKey(InputManager.getInstance().moveRightKey)&&!Input.GetKey(InputManager.getInstance().moveLeftKey))
        {
            dir = 1;
            if (isClimb && currentDir == PlayerDir.Left)
            {
                transform.Rotate(0, 180, 0);
                currentDir = PlayerDir.Right;
                playeranim.SetBool("isclimb", false);
                playeranim.SetBool("isfalling", true);
            }
        }
        else
        {
            dir = 0;
        }
        if(canRushTime>0)
        {
            canRushTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(InputManager.getInstance().rushKey)&&canRushTime<=0)
        {
            StartCoroutine(RushCoroutine(RushTime));
            isJump = false;
        }
        if (!isRush&&!isclimbJump) 
        {
            move.x = dir * speed;
        }
        if(!isClimb && !isclimbJump)
        {
            Rotatedir();
        }
    }

    void UDmove()//上下帧移动
    {
        if (isAttackJump)
        {
            move.y += 40 * Time.deltaTime;
        }else if (isground)
        {
            if (Input.GetKeyDown(InputManager.getInstance().jumpKey))
            {
                jumpAduio.Play();
                isJump = true;  //进入跳跃状态
                move.y += jumpForce * Time.deltaTime;//初始添加向上的力
                jumpTime = 0;//蓄力时间清零
            }
            else
            {
                move.y = 0;
            }
        }
        else
        {
            if (isClimb)
            {
                if(Input.GetKeyDown(InputManager.getInstance().jumpKey))//爬墙跳跃
                {
                    StartCoroutine(ClimpJump());
                }
                else
                {
                    move.y = -3.0f;//沿墙下滑
                }
            }
            else
            {
                if (Input.GetKey(InputManager.getInstance().jumpKey) && isJump)
                {
                    jumpTime += Time.deltaTime;//蓄力时间增加
                    if (jumpTime < maxJumpTime)
                    {
                        move.y += jumpForce * Time.deltaTime;//蓄力
                    }
                    else
                    {
                        isJump = false;
                    }
                }
                else if (Input.GetKeyUp(InputManager.getInstance().jumpKey))
                {
                    isJump = false;//退出跳跃状态
                    jumpTime = 0;//蓄力时间清零
                }
                else
                {
                    if (canGravity)
                        move.y += -1 * gravity * Time.deltaTime;
                }
            }
        }
    }

    void MoveCheck()//每帧检测，先左右 后上下
    {
        Vector3 moveDistance = move * Time.deltaTime;//当前帧移动距离
        int dir = 0;//确定下一帧移动的左右方向
        if (move.x > 0)
        {
            dir = 1;
        }
        else if (move.x < 0)
        {
            dir = -1;
        }
        else
        {
            dir = 0;//左右方向没有移动
        }
        if (dir != 0)//当左右速度有值时
        {
            RaycastHit2D lRHit2D = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.right * dir, 5.0f, playerLayerMask);//盒型射线碰撞检测
            if (lRHit2D.collider!=null)//如果检测到障碍物
            {
                float tempXVaule = (float)Math.Round(lRHit2D.point.x, 1);                   //取X轴方向的数值，并保留1位小数精度。
                Vector3 colliderPoint = new Vector3(tempXVaule, transform.position.y);      //重新构建射线的碰撞点
                float tempDistance = Vector3.Distance(colliderPoint, transform.position);   //计算玩家与碰撞点的距离
                if(tempDistance > (boxsize.x * 0.5f+distance))//可以移动
                {
                    float tempX;
                    float nextX = transform.position.x + moveDistance.x;
                    if (dir > 0)
                    {
                        tempX = tempXVaule - boxsize.x * 0.5f - distance;
                        if (nextX > tempX)
                        {
                            transform.position = new Vector3(tempX, transform.position.y, 0);
                        }
                        else
                        {
                            transform.position += new Vector3(moveDistance.x, 0, 0);
                        }
                    }
                    else
                    {
                        tempX = tempXVaule + boxsize.x * 0.5f + distance;
                        if (nextX < tempX)
                        {
                            transform.position = new Vector3(tempX, transform.position.y, 0); //上下方向多减少0.1f的修正距离，防止鬼畜
                        }
                        else
                        {
                            transform.position += new Vector3(moveDistance.x, 0, 0);
                        }
                    }
                }
                else//修正位移
                {
                    float tempX = 0;//新的X轴的位置
                    if (dir > 0)
                    {
                        tempX = tempXVaule - boxsize.x * 0.5f  - distance + 0.05f; //多加上0.05f的修正距离，防止出现由于精度问题产生的鬼畜行为
                    }
                    else
                    {
                        tempX = tempXVaule + boxsize.x * 0.5f + distance - 0.05f;
                    }
                    transform.position = new Vector3(tempX, transform.position.y, 0);//修正位置
                    if (lRHit2D.collider.CompareTag("Untagged"))    //如果左右是墙
                    {
                        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.down, boxsize.y, playerLayerMask);
                        if (hit2D.collider == null)//离地面一定距离
                        {
                            //如果上方是异形碰撞体，那么就无法进入爬墙状态
                            hit2D = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.down, boxsize.y, playerLayerMask);
                            if (hit2D.collider == null )//头顶也一定距离
                            {
                                isClimb = true;//进入爬墙
                                if(climbCount==0)
                                    climbAudio.Play();
                                climbCount+=1;
                            }
                        }
                    }
                }
            }
            else//无碰撞体时
            {
                transform.position += new Vector3(moveDistance.x, 0, 0);
                if (isClimb)//退出爬墙
                {
                    isClimb = false;
                    playeranim.SetBool("isclimb", false);
                    playeranim.Play("startfalling");
                    climbCount = 0;
                }
            }
        }
        else//左右方向无速度时
        {
            if (isClimb)    //处于爬墙状态时
            {
                RaycastHit2D hit2D = new RaycastHit2D();
                switch (currentDir)
                {
                    case PlayerDir.Left:
                        hit2D = Physics2D.Raycast(transform.position, Vector3.left, boxsize.x,playerLayerMask);
                        break;
                    case PlayerDir.Right:
                        hit2D = Physics2D.Raycast(transform.position, Vector3.right, boxsize.x,playerLayerMask);
                        break;
                }

                if (hit2D.collider == null)
                {
                    Debug.Log("tuichupaqiang");
                    isClimb = false;
                    playeranim.SetBool("isclimb", false);
                    playeranim.Play("startfalling");
                    climbCount = 0;
                }
            }
        }

        //开始上下检查，dir变量现在代表上下方向
        if (move.y > 0)
        {
            dir = 1;
        }
        else if (move.y < 0)
        {
            dir = -1;
        }
        else
        {
            dir = 0;
        }
        if(dir!=0)
        {
            RaycastHit2D uDHit2D = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.up * dir, 5.0f, playerLayerMask);
            if(uDHit2D.collider!=null)
            {
                float tempYVaule = (float)Math.Round(uDHit2D.point.y, 1);
                Vector3 colliderPoint = new Vector3(transform.position.x, tempYVaule);
                float tempDistance = Vector3.Distance(transform.position, colliderPoint);
                if (tempDistance > (boxsize.y * 0.5f + distance))//可以移动
                {
                    float tempY;
                    float nextY = transform.position.y + moveDistance.y;
                    if (dir > 0)
                    {
                        tempY = tempYVaule - boxsize.y * 0.5f - distance;
                        if (nextY > tempY)
                        {
                            transform.position = new Vector3(transform.position.x, tempY + 0.1f, 0);
                        }
                        else
                        {
                            transform.position += new Vector3(0, moveDistance.y, 0);
                        }
                    }
                    else
                    {
                        tempY = tempYVaule + boxsize.y * 0.5f + distance;
                        if (nextY < tempY)
                        {
                            transform.position = new Vector3(transform.position.x, tempY - 0.1f, 0); 
                        }
                        else
                        {
                            transform.position += new Vector3(0, moveDistance.y, 0);
                        }
                    }
                    isground = false;   //更新在地面的bool值
                }
                else//修正位移
                {
                    float tempY;
                    if (dir > 0)
                    {
                        tempY = uDHit2D.point.y - boxsize.y * 0.5f - distance + 0.05f;
                        isground = false;
                    }
                    else
                    {
                        tempY = uDHit2D.point.y + boxsize.y * 0.5f + distance - 0.05f;
                        isground = true;
                    }
                    move.y = 0;
                    transform.position = new Vector3(transform.position.x, tempY, 0);
                }
            }
            else
            {
                isground = false;
                transform.position += new Vector3(0, moveDistance.y, 0);
            }
        }
        else
        {
            isground = CheckIsGround();
        }
    }

    public bool CheckIsGround()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, boxsize, 0, Vector2.down, 5f, playerLayerMask);
        if (hit2D.collider != null)
        {
            float tempDistance = Vector3.Distance(transform.position, hit2D.point);
            if (tempDistance > (boxsize.y * 0.5f + distance+0.05f))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    void Rotatedir()
    {
        if (currentDir == PlayerDir.Left && move.x > 0)
        {
            transform.Rotate(0, 180, 0);
            currentDir = PlayerDir.Right;
        }
        else if (currentDir == PlayerDir.Right && move.x < 0)
        {
            transform.Rotate(0, -180, 0);
            currentDir = PlayerDir.Left;
        }
    }

    void InitPrefab()
    {
        if (theBall == null)
        {
            theBall = (GameObject)Resources.Load("prefabs/fireball");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDir = PlayerDir.Left;
        playerLayerMask = LayerMask.GetMask("Player");
        playerLayerMask = ~playerLayerMask;
        canGravity = true;
        playeranim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InitPrefab();
        LRmove();
        UDmove();
        //Attack();
        MoveCheck();
    }

   
}
