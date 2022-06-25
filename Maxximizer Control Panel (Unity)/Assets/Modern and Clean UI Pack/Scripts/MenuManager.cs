using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public List<GameObject> pages = new List<GameObject>();
    public List<GameObject> Navbar_buttons = new List<GameObject>();

    public int currentPanel = 0;
    private int currentButton = 0;

    void Start()
    {
        Navbar_buttons[currentPanel].GetComponent<Animator>().Play("open");
        pages[currentPanel].GetComponent<Animator>().Play("open");

    }

    public void PanelAnim(int newPanel)
    {
        if (newPanel != currentPanel)
        {
            pages[currentPanel].GetComponent<Animator>().Play("close");
            pages[newPanel].GetComponent<Animator>().Play("open");

            Navbar_buttons[currentButton].GetComponent<Animator>().Play("close");
            Navbar_buttons[newPanel].GetComponent<Animator>().Play("open");

            currentButton = newPanel;
            currentPanel = newPanel;

        }    
    }
}
