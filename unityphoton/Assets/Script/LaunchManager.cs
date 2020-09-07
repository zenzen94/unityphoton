using UnityEngine;
using Photon.Pun;

namespace Script
{
    public class LaunchManager : MonoBehaviourPunCallbacks
    {
        public void ConnectToPhotonServer()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log($"{PhotonNetwork.NickName} 이름으로 포톤서버에 연결되었습니다.");
        }

        public override void OnConnected()
        {
            Debug.Log("인터넷에 연결되었습니다.");
        }
    }
}
