using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalChoose : MonoBehaviour
{
    public GameObject ButtonRight,ButtonLeft;
    public TextMeshProUGUI text, indicator;
    private int p_index = 0;
    public int index {
        get
        {
            return p_index;
        }   
        set
        {
            p_index = value;
            StartCoroutine(DelayTextUpdate());
            return;
        }
    }
    IEnumerator DelayTextUpdate()
    {
        yield return new WaitForSeconds(0.3f);
        text.text = System.Text.RegularExpressions.Regex.Replace(objs[p_index], ".{1}", "$0").ToUpper();
        indicator.text = (p_index+1).ToString() + "/" + objs.Count;
    }


    //You can access this property in different scripts
    //Example
    //public GameObject obj;
    //obj.GetComponent<HorizontalChoose>().value;
    public string value
    {
        get
        {
            return objs[p_index];
        }
    }

    public int DefaultValueIndex;
    public List<string> objs = new List<string>();
    void Start()
    {
        index = DefaultValueIndex;
        ButtonRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            if ((index + 1) >= objs.Count)
            {
                index = 0;
            }
            else
            {

                index++;
            }
            return;
        });
        ButtonLeft.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (index == 0)
            {
                index = objs.Count - 1;
            }
            else
            {

                index--;
            }
            return;
        });
    }
}
