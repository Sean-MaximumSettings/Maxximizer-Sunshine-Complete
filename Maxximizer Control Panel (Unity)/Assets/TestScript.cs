using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 15;
        print(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/test.txt");
        print("Test!");
        File.WriteAllText("/test.txt", "test!");
        print("Test2");
    }

}
