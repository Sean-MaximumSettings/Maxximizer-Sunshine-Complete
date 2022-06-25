using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{   
    //Choose or edit the notification prefabs under Prefabs/Notifications folder
    public GameObject NotificationPrefab;

    public string Title, Description;

    [SerialisedField] public Sprite Icon;

    private Notification Notification;
    public void SpawnNotification()
    {
        GameObject SpawnedNotification = Instantiate(NotificationPrefab, this.transform);
        Notification = SpawnedNotification.GetComponent<Notification>();
        Notification.Title.text = Title;
        Notification.Description.text = Description;
        if (Icon == null)
        {
            return;
        }
        else
        {
            Notification.Icon.sprite = Icon;
        }
        SpawnedNotification.GetComponent<Animator>().SetTrigger("open");
        Destroy(SpawnedNotification, 5f);
    }
}

internal class SerialisedFieldAttribute : Attribute
{
}