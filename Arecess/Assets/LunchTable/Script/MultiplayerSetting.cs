﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour
{
    public static MultiplayerSetting multiplayerSetting;
   private void Awake()
    {
        if(MultiplayerSetting.multiplayerSetting==null)
        {
            MultiplayerSetting.multiplayerSetting = this;
        }
        else
        {
            if (MultiplayerSetting.multiplayerSetting != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
