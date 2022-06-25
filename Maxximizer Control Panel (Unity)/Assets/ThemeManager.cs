using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ThemeManager : MonoBehaviour
{

    public Dropdown themeDropdown;

    public List<theme> themes;

    [System.Serializable]
    public class theme
    { 
    public Color backgroundColor;
    public Color particleColor;
    public Color forcedColor;
    public Color forced2Color;
    public Color forced3Color;
    public Color boxColor;
    public Color box2Color;

    public Color textColor;
    public Color text2Color;

    public Color dropdownColor;
    }

    // Start is called before the first frame update
    void Awake()
    {
       int themeID = PlayerPrefs.GetInt("Theme");
        if (themeID != 0)
        {
            ChangeNow(themeID - 1);
            themeDropdown.value = themeID;
        }
    }

    public void ChangeTheme()
    {
        int value = themeDropdown.value;
        PlayerPrefs.SetInt("Theme",value);
        if (value <= 0) Application.LoadLevel(0);
        else ChangeNow(value-1);
    }

    // Update is called once per frame
    void ChangeNow(int id)
    {
        Camera.main.backgroundColor = themes[id].backgroundColor;
        GameObject.Find("Particles").GetComponent<Renderer>().material.color = themes[id].particleColor;
        Transform[] allChildren = transform.parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
           if(child.name == "Text1")
            {
                if (child.GetComponent<TMP_Text>())
                    child.GetComponent<TMP_Text>().color = themes[id].textColor;
                if (child.GetComponent<Text>())
                    child.GetComponent<Text>().color = themes[id].textColor;
            }else if (child.name == "Text2")
            {
                if (child.GetComponent<TMP_Text>())
                    child.GetComponent<TMP_Text>().color = themes[id].text2Color;
                if (child.GetComponent<Text>())
                    child.GetComponent<Text>().color = themes[id].text2Color;
            }else if (child.name.Contains("Box1"))
            {
                if (child.GetComponent<Image>())
                    child.GetComponent<Image>().color = themes[id].boxColor;
            }
            else if (child.name.Contains("Box2"))
            {
                if (child.GetComponent<Image>())
                    child.GetComponent<Image>().color = themes[id].box2Color;
            }
            else if (child.name.Contains("Force1"))
            {
                if (child.GetComponent<Image>())
                    child.GetComponent<Image>().color = themes[id].forcedColor;
            }
            else if (child.name.Contains("Force2"))
            {
                if (child.GetComponent<Image>())
                    child.GetComponent<Image>().color = themes[id].forced2Color;
            }
            else if (child.name.Contains("Force3"))
            {
                if (child.GetComponent<Image>())
                    child.GetComponent<Image>().color = themes[id].forced3Color;
            }
            else if (child.name.Contains("Dropdown1"))
            {
                if (child.GetComponent<Button>())
                {
                    ColorBlock colors = child.GetComponent<Button>().colors;
                    colors.normalColor = themes[id].dropdownColor;
                    child.GetComponent<Button>().colors = colors;
                }
                if (child.GetComponent<TMP_Dropdown>())
                {
                    ColorBlock colors = child.GetComponent<TMP_Dropdown>().colors;
                    colors.normalColor = themes[id].dropdownColor;
                    child.GetComponent<TMP_Dropdown>().colors = colors;
                }
            }
        }

    }
}
