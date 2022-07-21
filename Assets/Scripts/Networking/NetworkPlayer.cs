using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using atmgames.player;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Renderer headset;
    [SerializeField] private Camera pixelCamera;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private Renderer playerModelMirror;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            headset.enabled = false;

            playerModelMirror.enabled = true;
        }

        if (!photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<AudioListener>())
            {
                item.enabled = false;
            }
            foreach (var item in GetComponentsInChildren<Player>())
            {
                item.enabled = false;
            }
            foreach (var item in GetComponentsInChildren<Camera>())
            {
                item.enabled = false;
            }
            playerCamera.enabled = false;
            pixelCamera.enabled = false;
        }
    }

    private void Update()
    {
        
    }

}
