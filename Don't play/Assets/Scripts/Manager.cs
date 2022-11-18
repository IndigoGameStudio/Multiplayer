using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Manager : MonoBehaviour
{
    public PhotonView photonView;
    [SerializeField] TextMeshProUGUI _timeText;
    [SerializeField] int _timeStart = 10;
    public void StartGame(int addTime)
    {

        if (!photonView.IsMine)
        {
            _timeStart += addTime;
        }
        _timeText.text = "STARTING IN: 10";
        if (Application.isEditor)
        { 
            _timeStart = 3;
            _timeText.text = "STARTING IN: 3";
        }
        StartCoroutine(Time());
    }

    // ========================================================================
    IEnumerator Time()
    {
        while(_timeStart > 0)
        {
            yield return new WaitForSeconds(1);
            _timeStart -= 1;
            _timeText.text = string.Format("STARTING IN: {0} ", _timeStart.ToString());
            if(_timeStart == 0 && PhotonNetwork.IsMasterClient) { PhotonNetwork.LoadLevel(1); }
        }
    }

    // ========================================================================
    public void QuitGame()
    {
        #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) 
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://antoniogorki.itch.io/test");
        #endif
    }
}
