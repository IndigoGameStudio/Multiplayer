using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayersOnline : MonoBehaviour
{

    [SerializeField] float UpdateCheck;
    TextMeshProUGUI _textCount;

    void Start()
    {
        _textCount = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(CheckPlayers());
    }


    IEnumerator CheckPlayers()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            if(PhotonNetwork.InRoom)
            {
                _textCount.text = string.Format("PLAYERS ONLINE: {0} / 20", PhotonNetwork.CurrentRoom.PlayerCount);
            }

        }
    }
    
}
