using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PhotonConnection : MonoBehaviourPunCallbacks
{
    public static PhotonConnection instance;
    public TextMeshProUGUI txt;

    void Start()
    {
        instance = this;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        if (CheckDevice.instance.isMobile())
        {
            txt.text = "Connecting to server...";
        }
    }

    public override void OnConnectedToMaster()
    {
        txt.text = "Connected !";
        txt.color = Color.green;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        txt.text = string.Format("Disconnected from server. Reason: {0}", cause.ToString());
    }

    public int GetOnlinePlayers()
    {
        int playersNumber = PhotonNetwork.CountOfPlayers - 1;

        if (playersNumber < 0)
            return 0;
        else
            return playersNumber;
    }
}
