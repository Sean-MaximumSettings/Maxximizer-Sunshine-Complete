using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationExample : MonoBehaviour
{
    public NotificationManager notification_manager;

    [Header("INPUT")]
    public TMP_InputField title_field;
    public TMP_InputField desc_field;
    public TMP_Dropdown dropdown;

    [Header("PREFABS")]
    public GameObject Fade_N;
    public GameObject Pop_N;
    public GameObject Slide_N;
    public GameObject Shake_N;

    [Header("ICONS")]
    public Sprite Bell;
    public Sprite Photo;
    public Sprite Cogs;


    public void Spawn()
    {
        notification_manager.Title = title_field.text;
        notification_manager.Description = desc_field.text;
        if(notification_manager.Icon == null)
        {
            notification_manager.Icon = Bell;
        }
        if(notification_manager.NotificationPrefab == null)
        {
            notification_manager.NotificationPrefab = Fade_N;
        }
        notification_manager.SpawnNotification();
    }
    public void SetAnimation(int value)
    {
        switch (value)
        {

            case 0:
                notification_manager.NotificationPrefab = Fade_N;
                break;

            case 1:
                notification_manager.NotificationPrefab = Pop_N;
                break;

            case 2:
                notification_manager.NotificationPrefab = Slide_N;
                break;
            case 3:
                notification_manager.NotificationPrefab = Shake_N;
                break;
        }
    }
    public void SetIcon(string name)
    {
        switch(name){

            case "BELL":
                notification_manager.Icon = Bell;
            break;

            case "EXAMPLE":
                notification_manager.Icon = Photo;
            break;

            case "COGS":
                notification_manager.Icon = Cogs;
            break;
        }
    }

}
