using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Script
{
    public class PixelGunGameManager : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.NickName + " 이름으로 " + PhotonNetwork.CurrentRoom.Name + " 방에 입장했습니다.");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log(newPlayer.NickName + " 사용자가" + PhotonNetwork.CurrentRoom.Name + " 방에 입장했습니다. 현재 방인원은 " +
                      PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
}
