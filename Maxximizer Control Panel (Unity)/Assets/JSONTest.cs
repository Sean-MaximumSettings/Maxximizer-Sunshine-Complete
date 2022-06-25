using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class JSONTest : MonoBehaviour
{
    public string jsonString;

    public List<AppData> apps;

    [System.Serializable]
    public class AppData
    {
        public string name;
        public string cmd;
        public string detached;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadJSONFile();
    }

    void LoadJSONFile()
    {

        jsonString = File.ReadAllText(@"C:\Users\ZeoWo\Documents\Sunshine\assets\apps_windows.json");

        apps = new List<AppData>();

        string[] rawApps = Regex.Split(jsonString, "\"apps\":")[1].Split("\n"[0]);//jsonString.Split("\"apps\": ["[0])[1];

        int curListID = -1;
        for (int i = 0; i < rawApps.Length; i++)
        {
            if (rawApps[i].Contains("\"name\"")){
                curListID += 1;
                apps.Add(new AppData());
                apps[curListID].name = rawApps[i].Split("\""[0])[3];
            }
            if (rawApps[i].Contains("\"cmd\"")) apps[curListID].cmd = rawApps[i].Split("\""[0])[3].Replace("/", "\\").Replace("\\\\", "\\");
            if (rawApps[i].Contains("\"detached\"")) apps[curListID].detached = rawApps[i+1].Split("\""[0])[1];
        }

        SaveJSONFile();
    }

    void SaveJSONFile()
    {
        TextAsset template = Resources.Load<TextAsset>("app-template");

        string newApp = "";
        for (int i = 0; i < apps.Count; i++)
        {
            newApp = newApp + "{ \n \"name\":\" " + apps[i].name+"\",";
            if (apps[i].detached != null &&apps[i].detached != "" && apps[i].detached.Length>2) newApp = newApp + "\n \"detached\": [ \"" + apps[i].detached + "\" ],";
            newApp = newApp + "\n \"cmd\":\" " + apps[i].cmd.Replace("\\", "\\\\") + "\" \n }";
            // newApp = newApp + "\n \"output\":\"" + "" + "\" \n }";

            if (i != apps.Count - 1) newApp = newApp + ", \n";

        }

        string result = template.text.Replace("#",newApp);
        print(result);
    }

}
