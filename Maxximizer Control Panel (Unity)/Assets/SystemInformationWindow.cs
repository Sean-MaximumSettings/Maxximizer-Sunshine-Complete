using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SystemInformationWindow : MonoBehaviour
{

    public TMP_Text textBox;
    bool isLinux = false;
    public GameObject unsupportedHelper;
    // Start is called before the first frame update
    void Awake()
    {

#if UNITY_STANDALONE_LINUX
        isLinux = true;
        unsupportedHelper.SetActive(true);
#endif

        string gpuName = SystemInfo.graphicsDeviceName.ToUpper();
        string encoderType = "";
        if (gpuName.Contains("NVIDIA") || gpuName.Contains("GTX") || gpuName.Contains("QUADRO"))
        {
            encoderType = "NVENC";
        }
        else
        {
            encoderType = "AMF";
        }

        string data = "";

        if (isLinux)
        {
            if(SystemInfo.operatingSystem.Contains("17"))
            data += "OS: " + SystemInfo.operatingSystem + "\n\n";
            else data += "<color=red>Unsupported OS:</color> " + SystemInfo.operatingSystem + "\n\n";
        }
        else
        {
            data += "OS: " + SystemInfo.operatingSystem + "\n\n";
        }
        data += "GPU: " + SystemInfo.graphicsDeviceName + "\n";
        data += "GPU MEMORY: " + (int)((float)SystemInfo.graphicsMemorySize/1000) + "GB\n\n";
        data += "CPU: " + SystemInfo.processorType+"\n";
        data += "SYSTEM MEMORY: " + SystemInfo.systemMemorySize+"MB\n\n";
        data += "DETECTED HW ENCODER: " + encoderType;

        textBox.text = data;
    }


}

