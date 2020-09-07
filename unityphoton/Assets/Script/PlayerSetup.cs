using System;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Script
{
    public class PlayerSetup : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject fpsCamera;
        [SerializeField] private TextMeshProUGUI playerNameText;
        private void Start()
        {
            if (photonView.IsMine)
            {
                Debug.Log("내캐릭임");
                transform.GetComponent<MovementController>().enabled = true;
                fpsCamera.GetComponent<Camera>().enabled = true;
                fpsCamera.GetComponent<AudioListener>().enabled = true;

            }
            else
            {
                Debug.Log("내캐릭 아님");
                transform.GetComponent<MovementController>().enabled = false;
                fpsCamera.GetComponent<Camera>().enabled = false;
                fpsCamera.GetComponent<AudioListener>().enabled = false;
            }

            SetPlayerUI();
        }
        
        private void SetPlayerUI()
        {
            if (!ReferenceEquals(playerNameText, null))
            {
                playerNameText.text = photonView.Owner.NickName;
            }
        }
    }
}
