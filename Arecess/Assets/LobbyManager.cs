using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using System.Threading;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Login UI")]
    public InputField playerNameIF;
    public GameObject loginscreen;

    [Header("Lobby UI")]
    public GameObject lobby_game;
    public GameObject gamearena_game;

   // [Header("Connection Status UI")]
   // public GameObject connectionstatus;
   // public Text statusText;
  //  public bool showConnectionStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {

            lobby_game.SetActive(true);
            loginscreen.SetActive(false);
           // uI_3DGameobject.SetActive(true);


          //  connectionstatus.SetActive(false);


        }
        else
        {
            //Activating only Login UI since we did noy connect to Photon yet.

            lobby_game.SetActive(false);
            // uI_3DGameobject.SetActive(false);
           // connectionstatus.SetActive(false);

            loginscreen.SetActive(true);

        }


    }

    // Update is called once per frame
    void Update()
    {
       /* if (showConnectionStatus)
        {
            statusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;

        }*/
    }
    public void onEnterLunchTableClick()
    {
        SceneManager.LoadScene("AvatarScene");
    }
    public void onStartGameButtonClick()
    {
        string playername = playerNameIF.text;
        if (!string.IsNullOrEmpty(playername))
        {

            lobby_game.SetActive(false);
            // uI_3DGameobject.SetActive(false);
            loginscreen.SetActive(false);

           // showConnectionStatus = true;
           // connectionstatus.SetActive(true);

            if (!PhotonNetwork.IsConnected)
            {

                PhotonNetwork.LocalPlayer.NickName = playername;
                PhotonNetwork.ConnectUsingSettings();


            }
        }
        else
        {

        }
        



    }
 
    public override void OnConnected()
    {
        UnityEngine.Debug.Log("not connected");
    }
    public override void OnConnectedToMaster()
    {
        UnityEngine.Debug.Log(PhotonNetwork.LocalPlayer.NickName+" ist connected");
        lobby_game.SetActive(true);
        // uI_3DGameobject.SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
        loginscreen.SetActive(false);
        //connectionstatus.SetActive(false);
        //JoinRoom();
       // SceneManager.LoadScene("studentlog");
    }
}
