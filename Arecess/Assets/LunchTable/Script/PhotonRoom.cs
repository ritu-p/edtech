using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
public class PhotonRoom : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    public static PhotonRoom photonRoom;
    private PhotonView photonView;
    public bool IsGameLoaded;
    public int currentScene;
    private Player[] photonPlayers;
    public int playersInRoom;
    public int mynoInRoom;
    public int playerInGame;


    private void Awake()
    {
        if (PhotonRoom.photonRoom == null)
        {
            PhotonRoom.photonRoom = this;
        }
        else
        {
            if (PhotonRoom.photonRoom != this)
            {
                Destroy(PhotonRoom.photonRoom.gameObject);
                PhotonRoom.photonRoom = this;
      
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        mynoInRoom = playersInRoom;
        Startgame();


    }
    public override void OnPlayerEnteredRoom(Player newplayer)
    {
        base.OnPlayerEnteredRoom(newplayer);
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
    }
    void Startgame()
    {
        IsGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;

    }
    void OnSceneFinishedLoading(Scene scene,LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene==5)
        {
            IsGameLoaded = true;
            RPC_CreatePlayer();
        }

    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

}
