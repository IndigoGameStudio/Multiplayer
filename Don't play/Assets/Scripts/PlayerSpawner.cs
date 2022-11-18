using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public Transform[] spawnPoints;

    public void Start()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        int randomNumebr = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumebr];
        GameObject playerToSpawn = playerPrefab[0];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
}
