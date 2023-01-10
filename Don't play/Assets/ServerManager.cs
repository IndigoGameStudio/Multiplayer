using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{

    private IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            CheckPlayersCount();
        }   
    }

    private void CheckPlayersCount()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && !PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        }
    }

    public void QuitGame()
    {
#if (UNITY_EDITOR)
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsMasterClient) { PhotonNetwork.DestroyAll(); }
        PhotonNetwork.Disconnect();
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsMasterClient) { PhotonNetwork.DestroyAll(); }
        PhotonNetwork.Disconnect();
            Application.Quit();
#elif (UNITY_WEBGL)
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsMasterClient) { PhotonNetwork.DestroyAll(); }
        PhotonNetwork.Disconnect();
        Application.OpenURL("about:blank");
#endif
    }


    private void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsMasterClient) { PhotonNetwork.DestroyAll(); }
        PhotonNetwork.Disconnect();
    }
}
