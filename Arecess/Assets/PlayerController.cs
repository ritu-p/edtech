using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using NotificationSamples;
public class PlayerController : MonoBehaviour
{
 public Text codeTxt;
    public Text info;
    public static string sessionCode;
    public InputField code;
   /* [SerializeField]
    private GameNotificationManager gamenotifier;
    public void StartNotify()
    {
        GameNotificationChannel channel = new GameNotificationChannel("code1","this iscode","test");
        gamenotifier.Initialize(channel);


    }*/
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

                activity.Call<bool>("moveTaskToBack", true);
                Application.Quit();
            }
            else
            {
                Application.Quit();
            }
        }
    }
    public void GotoSessionScene()
    {
        SceneManager.LoadScene("start");
    }
    public void GotoTeacherScene()
    {
        SceneManager.LoadScene("first");
    }
    public void GotoStudentScene()
    {
        SceneManager.LoadScene("studentlog");
    }
   
           public void GotoGameScene()
    {
        sessionCode = code.text;
        UnityEngine.Debug.Log("sessionCode" + sessionCode);
        SceneManager.LoadScene("GameLobby");
    }
    public void GenerateRecess()
    {
        System.Random code = new System.Random();
        int recesscode = code.Next(1000, 9999);
        codeTxt.text = recesscode.ToString();
        info.text = "Share this code with the students";


    }
}
