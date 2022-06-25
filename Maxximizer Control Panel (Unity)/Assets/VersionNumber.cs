using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class VersionNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_STANDALONE_LINUX
        if (File.Exists("/mxs/Services/version-linux"))
        {
            GetComponent<Text>().text = "MAXIMUM SETTINGS - www.maximumsettings.com - v" + ((float)int.Parse(File.ReadAllText("/mxs/Services/version-linux"))/100).ToString();
        }
#endif
#if UNITY_STANDALONE_WIN
        if (File.Exists(@"C:\Windows\MXS\Services\version"))
        {
            GetComponent<Text>().text = "MAXIMUM SETTINGS - www.maximumsettings.com - v" + ((float)int.Parse(File.ReadAllText(@"C:\Windows\MXS\Services\version")) / 100).ToString();
        }
#endif
    }

}
