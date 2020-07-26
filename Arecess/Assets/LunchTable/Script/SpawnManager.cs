using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject[] playerPrefabs;
    public Transform[] spawnPositions;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnJoinedRoom()
    {
        object playerSelectionNumber;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(PlayerSelectionManager.avatarKey, out playerSelectionNumber))
        {

            UnityEngine.Debug.Log("Player selection number is "+ (int)playerSelectionNumber);
    int randomSpawnPoint = Random.Range(0, spawnPositions.Length-1);
              Vector3 instantiatePosition = spawnPositions[randomSpawnPoint].position;


              PhotonNetwork.Instantiate(playerPrefabs[(int)playerSelectionNumber].name, instantiatePosition, Quaternion.identity);
            UnityEngine.Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined room" + PhotonNetwork.CurrentRoom.Name+"spawned");


        }
    }
}
