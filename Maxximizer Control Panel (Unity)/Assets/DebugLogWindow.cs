using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Diagnostics;
using SFB;
public class DebugLogWindow : MonoBehaviour
{

    public Text outputText;

    void OnEnable()
    {
        RefreshOutput();
    }

    float timer;
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if(timer >= 5)
        {
            RefreshOutput();
            timer = 0;
        }
    }

    void RefreshOutput()
    {
        string outputResult = "";

        outputResult += "[SYSTEM INFORMATION] \n" + "os: " + SystemInfo.operatingSystem + "\n";
        outputResult += "gpu: " + SystemInfo.graphicsDeviceName + "\n";
        outputResult += "cpu: " + SystemInfo.processorType + "\n";
        outputResult += "system_memory: " + SystemInfo.systemMemorySize + "MB\n\n";

        string sunshineStatus = "Inactive";
        if (ProcessRunning("sunshine")) sunshineStatus = "Active";
        outputResult += "[SUNSHINE STATUS] \n" + sunshineStatus + "\n\n";

        #if UNITY_STANDALONE_LINUX

        if (File.Exists("/tmp/xrandr-output"))
            outputResult += "[XRANDR] \n" + File.ReadAllText("/tmp/xrandr-output") + "\n";

        if (File.Exists("/tmp/mxs-services-output"))
            outputResult += "[MAXIMUM SERVICES OUTPUT] \n" + File.ReadAllText("/tmp/mxs-services-output") + "\n\n";

        if (File.Exists("/tmp/mxs-assistant-output"))
            outputResult += "[MAXXIMIZER ASSISTANT OUTPUT] \n" + File.ReadAllText("/tmp/mxs-assistant-output");
#endif

        outputText.text = outputResult;

    }

    bool ProcessRunning(string processName)
    {
        bool result = false;
        Process[] running = Process.GetProcesses();
        foreach (Process process in running)
        {
            if (process.ProcessName.ToLower().Contains(processName.ToLower()))
                result = true;
        }
        return result;
    }

    public void SaveOutput()
    {
       string location = StandaloneFileBrowser.SaveFilePanel("Debug Log Output", "", "DebugLogOutput.txt", "");
        if(location != "")
        {
            File.WriteAllText(location, outputText.text);
        }
    }
}
