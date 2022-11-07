using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersOnline : MonoBehaviour
{

    [SerializeField] float UpdateCheck;
    Text _textCount;
    void Start()
    {
        _textCount = gameObject.GetComponent<Text>();
        StartCoroutine(CheckPlayers());
    }


    IEnumerator CheckPlayers()
    {
        while(true)
        {
            yield return new WaitForSeconds(UpdateCheck);
            _textCount.text = string.Format("PLAYERS ONLINE: {0} / 19", PhotonConnection.instance.GetOnlinePlayers().ToString());
        }
    }
    
}
