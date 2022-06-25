using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject appListWindow;
    public GameObject settingsWindow;
    public GameObject restartWindow;
    public GameObject reportWindow;
    public GameObject systemInfoWindow;
    public GameObject debugLogWindow;
    public GameObject versionWindow;
    public GameObject reportButton;
    public GameObject themeDropdown;
    public GameObject systemInfoButton;
    public GameObject debugOutputButton;
    public GameObject versionButton;

    bool isLinux = false;
    private void Awake()
    {
        Screen.SetResolution(1280, 720,false);
#if UNITY_STANDALONE_LINUX
        // isLinux = true;
        isLinux = false;
#else
        isLinux = false;
#endif
        //transform.Find("ApplicationsButton (White)").gameObject.SetActive(!isLinux);
        if (isLinux)
        {
            Vector3 appButtonPos = transform.Find("ApplicationsButton (White)").transform.position;
            Vector3 hostButtonPos = transform.Find("HostSettingsButton (White)").transform.position;
            transform.Find("ApplicationsButton (White)").position = hostButtonPos;
            transform.Find("HostSettingsButton (White)").position = appButtonPos;
            transform.Find("ApplicationsButton (White)").GetComponent<UnityEngine.UI.Button>().interactable = false;
            transform.Find("ApplicationsButton (White)/Normal/Text").GetComponent<TMPro.TMP_Text>().text = "APPLICATIONS (Unavailable)";
            transform.Find("ApplicationsButton (White)/Normal/Text").GetComponent<TMPro.TMP_Text>().fontSize = 18;
            SelectSettingsMenu();
        }
    }
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }

    public void SelectAppList()
    {
        versionWindow.SetActive(false);
        debugLogWindow.SetActive(false);
        systemInfoWindow.SetActive(false);
        systemInfoButton.SetActive(false);
        reportButton.SetActive(false);
        appListWindow.SetActive(true);
        settingsWindow.SetActive(false);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(false);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }

    public void SelectMainMenu()
    {
        if (isLinux) { SelectSettingsMenu(); return; }
        versionWindow.SetActive(false);
        debugLogWindow.SetActive(false);
        systemInfoWindow.SetActive(false);
        systemInfoButton.SetActive(true);
        this.gameObject.SetActive(true);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(false);
        restartWindow.SetActive(false);
        reportButton.SetActive(true);
        reportWindow.gameObject.SetActive(false);
        themeDropdown.SetActive(true);
        debugOutputButton.SetActive(true);
        versionButton.SetActive(true);
    }

    public void SelectReportWindow()
    {
        versionWindow.SetActive(false);
        debugLogWindow.SetActive(false);
        systemInfoWindow.SetActive(false);
        systemInfoButton.SetActive(false);
        reportButton.SetActive(false);
        reportWindow.gameObject.SetActive(true);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(false);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(false);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }

    public void SelectSettingsMenu()
    {
        versionWindow.SetActive(false);
        debugLogWindow.SetActive(false);
        systemInfoWindow.SetActive(false);
        systemInfoButton.SetActive(false);
        reportButton.SetActive(false);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(true);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(false);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }

    public void SelectRestartMenu()
    {

        systemInfoWindow.SetActive(false);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(false);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(true);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }

    public void SelectSystemInfoWindow()
    {
        versionWindow.SetActive(false);
        debugLogWindow.SetActive(false);
        systemInfoWindow.SetActive(true);
        systemInfoButton.SetActive(false);
        reportButton.SetActive(false);
        reportWindow.gameObject.SetActive(false);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(false);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(false);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }

    public void SelectDebugWindow()
    {
        versionWindow.SetActive(false);
        debugLogWindow.SetActive(true);
        systemInfoWindow.SetActive(false);
        systemInfoButton.SetActive(false);
        reportButton.SetActive(false);
        reportWindow.gameObject.SetActive(false);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(false);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(false);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }

    public void SelectVersionWindow()
    {
        versionWindow.SetActive(true);
        debugLogWindow.SetActive(false);
        systemInfoWindow.SetActive(false);
        systemInfoButton.SetActive(false);
        reportButton.SetActive(false);
        reportWindow.gameObject.SetActive(false);
        appListWindow.SetActive(false);
        settingsWindow.SetActive(false);
        this.gameObject.SetActive(false);
        restartWindow.SetActive(false);
        debugOutputButton.SetActive(false);
        versionButton.SetActive(false);
    }


    public void RestartServices()
    {
        Process[] pname2 = Process.GetProcessesByName("sunshine");
        if (pname2.Length != 0) pname2[0].Kill();

        pname2 = Process.GetProcessesByName("sunshine.exe");
        if (pname2.Length != 0) pname2[0].Kill();

        SelectMainMenu();
        return;

        string cmdPath = @"C:\Windows\system32\cmd.exe";
        ProcessStartInfo gamepadInfo = new ProcessStartInfo();
        gamepadInfo.FileName = cmdPath;
        gamepadInfo.WorkingDirectory = @"C:\Users\MaximumSettings\Services";
        gamepadInfo.Arguments = "/C " + "stopservice.bat";
        gamepadInfo.Verb = "runas";
        gamepadInfo.CreateNoWindow = true;
        gamepadInfo.WindowStyle = ProcessWindowStyle.Hidden;
        Process gamepadInstall = new Process();
        gamepadInstall.StartInfo = gamepadInfo;
        gamepadInstall.Start();

        SelectMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        themeDropdown.SetActive(false);
        systemInfoButton.SetActive(false);
        debugOutputButton.SetActive(false);
    }
}
