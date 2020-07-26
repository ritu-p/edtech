using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
public class ARPlacementController : MonoBehaviour
{
    public Text startText; // used for showing countdown from 3, 2, 1 
    private float timeLeft = 120.0f;
    ARPlaneManager ar_plane;
    ArPlacementManger ar_placementmgr;
    public GameObject placeBtn;
    // Start is called before the first frame update
    public GameObject[] playerPrefabs;
    public Transform[] spawnPositions;
    public GameObject table;
    // Start is called before the first frame update
    private void Awake()
    {
        ar_plane= GetComponent<ARPlaneManager>();
        ar_placementmgr = GetComponent<ArPlacementManger>();
    }
    public enum RaiseEventCodes
    {
        PlayerSpawnEventCode = 0
    }
    void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        placeBtn.SetActive(true);
  //      SpawnPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
        string seconds = (timeLeft % 60).ToString("00");
        startText.text = string.Format("{0}:{1}", minutes, seconds);
        if (timeLeft < 0)
        {
            UnityEngine.Debug.Log("Timer ranout" + timeLeft);
            SceneManager.LoadScene("LunchTable/GameOver");
        }
    }
    public void DisablePlacmenet()
    {
        ar_plane.enabled = false;
        ar_placementmgr.enabled = false;
        SetAllPlanes(false);
        placeBtn.SetActive(false);
        
    }

    public void EnablePlacmenet()
    {
        ar_plane.enabled = true;
        ar_placementmgr.enabled = true;
        SetAllPlanes(true);
        placeBtn.SetActive(true);
    }
    private void SetAllPlanes(bool val)
    {
        foreach (var plane in ar_plane.trackables)
        {
            plane.gameObject.SetActive(val);
        }
    }
     private void OnDestroy()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

   public void OnCook()
    {
        SceneManager.LoadScene("LunchTable/Won");
    }
 
    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RaiseEventCodes.PlayerSpawnEventCode)
        {

            object[] data = (object[])photonEvent.CustomData;
            Vector3 receivedPosition = (Vector3)data[0];
            Quaternion receivedRotation = (Quaternion)data[1];
            int receivedPlayerSelectionData = (int)data[3];

            UnityEngine.Debug.Log("Player selection custom data" + data);
            GameObject player = Instantiate(playerPrefabs[receivedPlayerSelectionData], receivedPosition+ table.transform.position, receivedRotation);
            PhotonView _photonView = player.GetComponent<PhotonView>();
            _photonView.ViewID = (int)data[2];


        }
    }

    private void SpawnPlayer()
    {
        object playerSelectionNumber;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(PlayerSelectionManager.avatarKey, out playerSelectionNumber))
        {

            UnityEngine.Debug.Log("Player selection number is " + (int)playerSelectionNumber);
            int randomSpawnPoint = Random.Range(0, spawnPositions.Length - 1);
            Vector3 instantiatePosition = spawnPositions[randomSpawnPoint].position;


            GameObject playerGameobject = Instantiate(playerPrefabs[(int)playerSelectionNumber-1], instantiatePosition, Quaternion.identity);
            PhotonView _photonView = playerGameobject.GetComponent<PhotonView>();

            if (PhotonNetwork.AllocateViewID(_photonView))
            {
                object[] data = new object[]
                {

                    playerGameobject.transform.position- table.transform.position, playerGameobject.transform.rotation, _photonView.ViewID,playerSelectionNumber
                };


                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others,
                    CachingOption = EventCaching.AddToRoomCache

                };


                SendOptions sendOptions = new SendOptions
                {
                    Reliability = true
                };

                //Raise Events!
                PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.PlayerSpawnEventCode, data, raiseEventOptions, sendOptions);


            }
            else
            {

                UnityEngine.Debug.Log("Failed to allocate a viewID");
                Destroy(playerGameobject);
            }

            UnityEngine.Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined room" + PhotonNetwork.CurrentRoom.Name + "spawned");


        }
    }
}
