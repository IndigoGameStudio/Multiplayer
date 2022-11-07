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
    public GameObject _goGreska;

    void Start()
    {


        instance = this;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        if (CheckDevice.instance.isMobile()) { txt.text = "CONNECTING TO SERVER..."; }
    }

    public override void OnConnectedToMaster()
    {
        if (CheckDevice.instance.isMobile()) { txt.text = "YOU ARE CONNECTED !"; }
        if (!CheckDevice.instance.isMobile() && GetOnlinePlayers() != 0)
        {
            _goGreska.SetActive(true);
            PhotonNetwork.Disconnect();
            return;
        }
        DeviceManager.instance.LoadScene();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        txt.text = string.Format("DISCONNECTED, REASON: {0}", cause.ToString().ToUpper());
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
