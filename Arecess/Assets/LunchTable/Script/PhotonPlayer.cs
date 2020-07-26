using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
   
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.gamesetup.spawnPoints.Length);
        if (PV.IsMine)
        {
            myAvatar= PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar2"),
                GameSetup.gamesetup.spawnPoints[spawnPicker].position, GameSetup.gamesetup.spawnPoints[spawnPicker].rotation, 0);
           // GameSetup.gamesetup.ARcamera.transform.position = GameSetup.gamesetup.spawnPoints[spawnPicker].position;
         //   GameSetup.gamesetup.ARcamera.transform.rotation = GameSetup.gamesetup.spawnPoints[spawnPicker].rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
