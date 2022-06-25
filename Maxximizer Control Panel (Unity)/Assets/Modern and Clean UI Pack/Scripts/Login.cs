using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [Header("Input fields")]
    public TextMeshProUGUI Input_Username;
    public TextMeshProUGUI Input_Password;

    public Animator containerAnimator;

    //page to load after the login
    public GameObject AfterLogin;

    //loading object
    public GameObject Loading;

    //Object to hide
    public GameObject Hide;

    public float FLoading = 5.0f;

    public void LoginFunction()
    {
        //takes the input values
        var username = Input_Username.text;
        var password = Input_Password.text;

        Loading.SetActive(true);
        Loading.GetComponent<Animator>().SetTrigger("In");
        Hide.SetActive(false);

        GameObject obj = GetChildWithName(Loading, "WelcomeText");
        obj.GetComponent<TextMeshProUGUI>().text = "Welcome back " + username;
        StartCoroutine(AfterLoginTimer());
    }
    IEnumerator AfterLoginTimer()
    {
        yield return new WaitForSeconds(FLoading);
        Loading.GetComponent<Animator>().ResetTrigger("In");
        Loading.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(1);
        Loading.SetActive(false);
        AfterLogin.SetActive(true);
        AfterLogin.GetComponent<Animator>().SetTrigger("In");
        containerAnimator.SetBool("open", false);
        yield return new WaitForSeconds(1.5f);
        Hide.SetActive(true);
    }
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
