using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    public TextMeshPro playerNameText;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
       /* if (photonView.IsMine)
        {
            //The player is local player. 
            transform.GetComponent<MovementController>().enabled = true;
            transform.GetComponent<MovementController>().joystick.gameObject.SetActive(true);
        }
        else
        {

            //The player is remote player
            transform.GetComponent<MovementController>().enabled = false;
            transform.GetComponent<MovementController>().joystick.gameObject.SetActive(false);
        }*/
        SetPlayerName();
    }

    void SetPlayerName()
    {

        UnityEngine.Debug.Log("photon view" + photonView.IsMine);
        if (playerNameText != null)
        {
            if (photonView.IsMine)

            {
                playerNameText.text = "YOU";
                playerNameText.color = Color.red;
            }
    else
            {

                /*foreach (PhotonPlayer p in PhotonNetwork.playerList)
                {
                    p.NickName); 
                }*/
                Dictionary<int, Photon.Realtime.Player> pList = PhotonNetwork.CurrentRoom.Players;
                foreach (KeyValuePair<int, Photon.Realtime.Player> p in pList)
                {
                    UnityEngine.Debug.Log("test print:" + p.Value.NickName);
                }
                if (photonView.Owner != null && photonView.Owner.NickName != null)
                {
                    playerNameText.text = photonView.Owner.NickName;
                }

            }
            UnityEngine.Debug.Log("Player bane:" + playerNameText.text);
        }
       

    }

}
