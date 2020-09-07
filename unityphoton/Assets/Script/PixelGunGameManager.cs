using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace Script
{
    public class PixelGunGameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerPrefab;

        public static PixelGunGameManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Debug.Log("겜매니저 스타트 불림");
            if (PhotonNetwork.IsConnected)
            {
                if (!ReferenceEquals(playerPrefab, null))
                {
                    var randomPoint = Random.Range(-20, 20);
                    PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPoint, 0, randomPoint),
                        Quaternion.identity);
                }
            }
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("방입장 OnJoinedRoom 겜매니저");
            
            Debug.Log(PhotonNetwork.NickName + " 이름으로 " + PhotonNetwork.CurrentRoom.Name + " 방에 입장했습니다.");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log(newPlayer.NickName + " 사용자가" + PhotonNetwork.CurrentRoom.Name + " 방에 입장했습니다. 현재 방인원은 " +
                      PhotonNetwork.CurrentRoom.PlayerCount);
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("GameLauncherScene"); 
            //PhotonNetwork.LoadLevel("GameLauncherScene");
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}