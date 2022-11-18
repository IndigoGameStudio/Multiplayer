using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    public static PhotonConnection instance;
    public Manager manager;
    [SerializeField] PhotonView _photonView;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] GameObject _goPCGreska;
    [SerializeField] GameObject _goMobileGreska;

    void Start()
    {
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();
        txt.text = "CONNECTING TO SERVER...";
        Debug.Log("CONNECTING TO SERVER...");
    }

    // ========================================================================

    public override void OnConnectedToMaster()
    {
        if (!CheckDevice.instance.isMobile() && !Application.isEditor && PhotonNetwork.CountOfRooms != 0)
        {
            _goPCGreska.SetActive(true);
            PhotonNetwork.Disconnect();
            return;
        }

        if (CheckDevice.instance.isMobile() && PhotonNetwork.CountOfRooms == 0) {
            _goMobileGreska.SetActive(true);
            PhotonNetwork.Disconnect();
            return;
        }

        Debug.Log("CONNECTED TO MASTER !");
        txt.text = "YOU ARE CONNECTED !";
        PhotonNetwork.JoinLobby();
        DeviceManager.instance.LoadScene();

    }

    // ========================================================================

    public override void OnJoinedLobby()
    {
        if (!CheckDevice.instance.isMobile() && PhotonNetwork.CountOfRooms == 0)
        {
            PhotonNetwork.CreateRoom("Room");
        }

        if(CheckDevice.instance.isMobile())
        {
            PhotonNetwork.JoinRoom("Room");
        }

        if(Application.isEditor)
        {
            PhotonNetwork.JoinRoom("Room");
        }

        Debug.Log("JOINED TO LOBBY !");
    }

    // ========================================================================

    public override void OnJoinedRoom()
    {
        Debug.Log(string.Format("Uspjesno si se povezao/la u sobu pod imenom '{0}'", PhotonNetwork.CurrentRoom.Name));
        manager.StartGame(5);
    }

    // ========================================================================

    public override void OnDisconnected(DisconnectCause cause)
    {
        txt.text = string.Format("DISCONNECTED, REASON: {0}", cause.ToString().ToUpper());
    }

    // ========================================================================

    public int GetOnlinePlayers()
    {
        int playersNumber = PhotonNetwork.CountOfPlayers - 1;
        return Mathf.Clamp(playersNumber, 0, 19);
    }

}
