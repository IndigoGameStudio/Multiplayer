using UnityEngine;
using TMPro;
using Photon.Pun;

public class GetPing : MonoBehaviour
{
    TextMeshProUGUI pingText;
    void Start() => pingText = GetComponent<TextMeshProUGUI>();

    void Update()
    {
      if(PhotonNetwork.IsConnected)
            pingText.text = string.Format("PING: {0}", PhotonNetwork.GetPing());
    }
}
