using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{

    // インスペクターから設定
    public GameObject PhotonObject;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // 入室に失敗した場合に呼ばれるコールバック
    // １人目は部屋がないため必ず失敗するので部屋を作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8; // 最大8人まで入室可能
        PhotonNetwork.CreateRoom(null, roomOptions); //第一引数はルーム名
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(
            PhotonObject.name,
            new Vector3(0f, 1f, 0f),    //ポジション
            Quaternion.identity,    //回転
            0
        );
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<UnityChan.ThirdPersonCamera>().enabled = true;
    }
}
