using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager Instance;

    public bool isGetKey;
    public KeyCode nowDownKey;  //现在按下的键

    public static InputManager getInstance()
    {
        return Instance;
    }

    [Header("上")]
    public KeyCode moveUpKey;
    [Header("下")]
    public KeyCode moveDownKey;
    [Header("左")]
    public KeyCode moveLeftKey;
    [Header("右")]
    public KeyCode moveRightKey;
    [Header("攻击按键")]
    public KeyCode attackKey;
    [Header("跳跃按键")]
    public KeyCode jumpKey;
    [Header("冲刺按键")]
    public KeyCode rushKey;
    [Header("复仇之魂按键")]
    public KeyCode fireballKey;
    [Header("菜单按键")]
    public KeyCode menuKey;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        KeyInit();
        isGetKey = false;
    }


    public void KeyInit()
    {
        moveUpKey = KeyCode.UpArrow;
        moveDownKey = KeyCode.DownArrow;
        moveLeftKey = KeyCode.LeftArrow;
        moveRightKey = KeyCode.RightArrow;
        attackKey = KeyCode.X;
        jumpKey = KeyCode.Z;
        rushKey = KeyCode.C;
        menuKey = KeyCode.Escape;
        fireballKey = KeyCode.A;
    }

    public void stopInput()
    {
        moveUpKey = KeyCode.None;
        moveDownKey = KeyCode.None;
        moveLeftKey = KeyCode.None;
        moveRightKey = KeyCode.None;
        attackKey = KeyCode.None;
        jumpKey = KeyCode.None;
        rushKey = KeyCode.None;
    }

    private void OnGUI()
    {
        if (isGetKey)
        {
            if (Input.anyKeyDown)
            {
                Event e = Event.current;
                if (e.isKey)
                {
                    if (e.keyCode != KeyCode.KeypadEnter && e.keyCode != KeyCode.None)
                    {
                        nowDownKey = e.keyCode;
                    }

                }
            }
        }
    }
}
