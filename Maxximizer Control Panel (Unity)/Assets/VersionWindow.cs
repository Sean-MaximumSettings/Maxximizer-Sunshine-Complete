using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VersionWindow : MonoBehaviour
{

    public Text textUI;

    public List<VersionData> versions;

    [System.Serializable]
    public class VersionData
    {
        public float versionNumber;
        public string[] changes;
    }

    private void OnEnable()
    {
        string output = "";

        for (int i = versions.Count; i-- > 0;)
        {
            output += "<size=22><b> Release notes (v" + versions[i].versionNumber.ToString() + ")</b></size>\n";
            foreach(string change in versions[i].changes)
            {
                output += "<b>●</b> " + change + "\n";
            }
            output += "\n";
        }

        textUI.text = output;
    }
}
