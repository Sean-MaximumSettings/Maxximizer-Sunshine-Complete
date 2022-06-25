using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppListObject : MonoBehaviour
{
    public ApplicationWindow appWindow;
    public int appID;
    // Start is called before the first frame update
    public void DeleteApp()
    {
        appWindow.DeleteApp(appID);
    }

    public void EditApp() {
        appWindow.SwitchToEditApp(appID);
    }
}
