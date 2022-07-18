using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Renderer headset;
    [SerializeField] private Camera pixelCamera;
    [SerializeField] private Camera playerCamera;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            headset.enabled = false;
        }

        if (!photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<AudioListener>())
            {
                item.enabled = false;
            }
            playerCamera.enabled = false;
            pixelCamera.enabled = false;
        }
    }

}
