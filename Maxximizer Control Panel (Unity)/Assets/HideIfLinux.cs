using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfLinux : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
#if UNITY_STANDALONE_LINUX
       // this.gameObject.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
