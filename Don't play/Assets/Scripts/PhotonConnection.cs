using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
using System.Collections;

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

        if (CheckDevice.instance.isMobile() && PhotonNetwork.CountOfPlayers == 0) {
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
        StartCoroutine(JoinRoom());
    }

    IEnumerator JoinRoom()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.JoinOrCreateRoom("Room", null, null);
    }

    public override void OnJoinedRoom()
    {
        txt.text = "JOINED TO ROOM ! (" + PhotonNetwork.CurrentRoom.Name +")";
        Debug.Log(string.Format("Uspjesno si se povezao/la u sobu pod imenom '{0}'", PhotonNetwork.CurrentRoom.Name));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
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
        return Mathf.Clamp(PhotonNetwork.CurrentRoom.PlayerCount, 0, PhotonNetwork.CurrentRoom.MaxPlayers);
    }

}
