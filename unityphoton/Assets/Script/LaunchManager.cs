using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

namespace Script
{
    public class LaunchManager : MonoBehaviourPunCallbacks
    {
        public GameObject EnterGamePanel;
        public GameObject ConnectionStatusPanel;
        public GameObject LobbyPanel;

        #region Unity Methods

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            EnterGamePanel.SetActive(true);
            ConnectionStatusPanel.SetActive(false);
            LobbyPanel.SetActive(false);
        }

        #endregion
        
        #region Public Methods

        public void ConnectToPhotonServer()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();

                EnterGamePanel.SetActive(false);
                ConnectionStatusPanel.SetActive(true);
            }
        }

        public void JoinRandomRoom()
        {
            Debug.Log("랜덤 방에 입장합니다.");
            var temp = PhotonNetwork.JoinRandomRoom();
            Debug.Log($"성공여부 {temp}");
        }

        #endregion


        #region Private Methods

        private void CreateAndJoinRoom()
        {
            var randomRoomName = "Room " + Random.Range(0, 10000);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 20;

            PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
        }

        #endregion


        #region Photon Callback

        public override void OnConnectedToMaster()
        {
            Debug.Log($"{PhotonNetwork.NickName} 이름으로 포톤서버에 연결되었습니다.");

            LobbyPanel.SetActive(true);
            ConnectionStatusPanel.SetActive(false);
        }

        public override void OnConnected()
        {
            Debug.Log("인터넷에 연결되었습니다.");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.Log("랜덤 방 입장에 실패했습니다.");
            Debug.Log(message);

            CreateAndJoinRoom();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("방 입장에 실패했습니다.");
            base.OnJoinRoomFailed(returnCode, message);
            Debug.Log("방 입장에 실패했습니다.");
            Debug.Log(message);

            // CreateAndJoinRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.NickName + " 이름으로 " + PhotonNetwork.CurrentRoom.Name + " 방에 입장했습니다.");
            PhotonNetwork.LoadLevel("GameScene");
            
            
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log(newPlayer.NickName + " 사용자가" + PhotonNetwork.CurrentRoom.Name + " 방에 입장했습니다.");
        }

        #endregion
    }
}