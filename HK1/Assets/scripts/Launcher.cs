using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // 连接上服务器后，打印验证
        Debug.Log("welcome, HK");
        // 创建房间
        PhotonNetwork.JoinOrCreateRoom("Room",
            new Photon.Realtime.RoomOptions() { MaxPlayers = 4}, default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // 在指定位置生成玩家
        PhotonNetwork.Instantiate("player", new Vector3(1, 1, 0), Quaternion.identity, 0);
    }

}
