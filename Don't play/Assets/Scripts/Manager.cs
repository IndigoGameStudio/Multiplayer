using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Manager : MonoBehaviour
{
    public PhotonView photonView;
    [SerializeField] TextMeshProUGUI _timeText;
    [Space]
    [Header("Settings")]
    [SerializeField] int _timeStart = 10;
    [SerializeField] int _minimumPlayers;
    int currentTime;

    public void StartGame(int addTime)
    {
        currentTime = _timeStart;
        if (PhotonNetwork.CurrentRoom.PlayerCount < _minimumPlayers)
            return;

        if (!photonView.IsMine)
        {
            currentTime += addTime;
        }
        _timeText.text = "STARTING IN: 10";
        if (Application.isEditor)
        {
            currentTime = 3;
            _timeText.text = "STARTING IN: 3";
        }
        StartCoroutine(Time());
    }

    // ========================================================================
    IEnumerator Time()
    {
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            if (PhotonNetwork.CurrentRoom.PlayerCount < _minimumPlayers)
            {
                currentTime = _timeStart;
                _timeText.text = string.Empty;
                break;
            }
            else
            {
                currentTime -= 1;
                _timeText.text = string.Format("STARTING IN: {0} ", currentTime.ToString());
                if (currentTime == 0 && PhotonNetwork.IsMasterClient) { PhotonNetwork.LoadLevel(1); }
            }
        }
    }

    // ========================================================================
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
