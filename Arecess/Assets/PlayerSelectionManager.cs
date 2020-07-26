using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerSelectionManager : MonoBehaviourPunCallbacks
{
    public GameObject[] avatrModels;
    public GameObject btnCook;
    public int playerSelectionNumber;
    public Text info;
    public Text avatarinfo;
    public Text play;
    public GameObject recipe;
    public Toggle inforToggle;
    public static string avatarKey = "avatar_no";
    // Start is called before the first frame update
    void Start()
    {
        playerSelectionNumber = 0;
        inforToggle.onValueChanged.AddListener((value) =>
        {
            OnShowInfo(value);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnShowInfo(bool val)
    {
        UnityEngine.Debug.Log("Insie Toggle" + val);
        if (val)
        {
            play.gameObject.SetActive(true);
            recipe.SetActive(false);
        }
        else
        {
            play.gameObject.SetActive(false);
            recipe.SetActive(true);
        }

    }
    public void OnSelectButtonClicked()
    {
       // UnityEngine.Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        string avatar = EventSystem.current.currentSelectedGameObject.name;
        UnityEngine.Debug.Log(avatar);
        switch (avatar)
        {
            case "player1":
                playerSelectionNumber = 1;
                UnityEngine.Debug.Log("inside 1");
                break;
            case "player2":
                playerSelectionNumber = 2;
                UnityEngine.Debug.Log("inside 2");
                break;
            case "player3":
                playerSelectionNumber = 3;
                UnityEngine.Debug.Log("inside 3");
                break;
            case "player4":
                playerSelectionNumber = 4;
                UnityEngine.Debug.Log("inside 4");
                break;
        }
        UnityEngine.Debug.Log(playerSelectionNumber);
       
        ExitGames.Client.Photon.Hashtable playerSelectionProp = new ExitGames.Client.Photon.Hashtable { { avatarKey, playerSelectionNumber } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerSelectionProp);
        JoinRoom();

    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("LunchTable:" + PlayerController.sessionCode);

    }
    public void onCookClick()
    {
        SceneManager.LoadScene("LunchTable/LunchTableChaos");
    }
    public void CreateandjoinRoom()
    {
        // RoomOptions options = new RoomOptions();
        // options.MaxPlayers = 4;
        UnityEngine.Debug.Log("Creating room");
        PhotonNetwork.CreateRoom("LunchTable:" + PlayerController.sessionCode);
    }

    public override void OnJoinRoomFailed(short code, string message)
    {
        UnityEngine.Debug.Log("Failed to join room" + message);
        CreateandjoinRoom();
    }
    public override void OnJoinedRoom()
    {
        foreach(GameObject m in avatrModels)
        {
            m.SetActive(false);
        }
        info.text = PhotonNetwork.LocalPlayer.NickName + " joined room: " + PhotonNetwork.CurrentRoom.Name;
        info.gameObject.SetActive(true);
        btnCook.SetActive(true);
        recipe.SetActive(true);
        avatarinfo.gameObject.SetActive(false);
        inforToggle.gameObject.SetActive(true);
    
        UnityEngine.Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined room" + PhotonNetwork.CurrentRoom.Name);
        
    }
}
