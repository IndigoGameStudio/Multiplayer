using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

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
        // Prilikom ulaska u igru pokrece se spajanje na server. (Bilo koji uredaj)
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();
        txt.text = "CONNECTING TO SERVER...";
        Debug.Log("CONNECTING TO SERVER...");
    }

    public override void OnConnectedToMaster()
    {
        //// Provjera ukoliko se netko spaja s racunalom a vec je jedno racunalo povezano (platno) izbacuje ga sa servera.
        //if(!CheckDevice.instance.isMobile() && PhotonNetwork.CountOfRooms != 0) {
        //    _goPCGreska.SetActive(true);
        //    PhotonNetwork.Disconnect();
        //    return;
        //}
        // Ukoliko se netko pokusa spojiti s mobitelom u igru a platno nije aktivno izbaciti ce ga iz igre.
        if (CheckDevice.instance.isMobile() && PhotonNetwork.CountOfRooms == 0) {
            _goMobileGreska.SetActive(true);
            PhotonNetwork.Disconnect();
            return;
        }

        // Ako je platno spojeno i korisnik se povezuje s mobitelom automatski ce ga poslati u lobby
        Debug.Log("CONNECTED TO MASTER !");
        txt.text = "YOU ARE CONNECTED !";
        PhotonNetwork.JoinLobby();
        DeviceManager.instance.LoadScene();

    }

    public override void OnJoinedLobby()
    {
        // Kad se uredaj spoio u lobby ide projvera je li je platno ako je platno onda ce kreirati sobu
        if (!CheckDevice.instance.isMobile() && PhotonNetwork.CountOfRooms == 0)
        {
            PhotonNetwork.CreateRoom("Room");
        }

        // Ukoliko se netko spoio s mobitelom onda ce se povezati u tu sobu koja je kreirana putem platna.
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

    public override void OnJoinedRoom()
    {
        // Funkcija se pokrece kada trenutni igrac poveze u sobu.
        Debug.Log(string.Format("Uspjesno si se povezao/la u sobu pod imenom '{0}'", PhotonNetwork.CurrentRoom.Name));
        manager.StartGame(5);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        txt.text = string.Format("DISCONNECTED, REASON: {0}", cause.ToString().ToUpper());
    }

    public int GetOnlinePlayers()
    {
        int playersNumber = PhotonNetwork.CountOfPlayers - 1;
        return Mathf.Clamp(playersNumber, 0, 19);
    }

}
