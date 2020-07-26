using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameSetup : MonoBehaviour
{

    public static GameSetup gamesetup;
    public Transform[] spawnPoints;
    public Camera ARcamera;
    private void OnEnable()
    {
        if (GameSetup.gamesetup == null)
        {
            GameSetup.gamesetup = this;
        }
      /*  else
        {
            if (MultiplayerSetting.multiplayerSetting != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);*/
    }
   
}
