using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Security.Cryptography.X509Certificates;
public class Main : MonoBehaviour
{
    public Transform inputParent;
    public Transform checkingPinParent;
    public InputField pinInputField;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitPin()
    {
        if(!isSubmitting)
        StartCoroutine(SubmittingPin());
    }

    private static readonly HttpClient client = new HttpClient();

    bool isSubmitting = false;
    IEnumerator SubmittingPin()
    {

        isSubmitting = true;
        inputParent.gameObject.SetActive(false);
        checkingPinParent.gameObject.SetActive(true);

        checkingPinParent.GetChild(0).gameObject.SetActive(true);
        checkingPinParent.GetChild(1).gameObject.SetActive(false);
        checkingPinParent.GetChild(2).gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:47990/api/pin");

        var data = Encoding.ASCII.GetBytes( "{ \"pin\": "+pinInputField.text+" }");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }
        var response = (HttpWebResponse)request.GetResponse();
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        print(responseString);
        yield return new WaitForEndOfFrame();

        if (responseString.Contains("true"))
        {
            checkingPinParent.GetChild(0).gameObject.SetActive(false);
            checkingPinParent.GetChild(1).gameObject.SetActive(false);
            checkingPinParent.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(4);
        }
        else
        {
            checkingPinParent.GetChild(0).gameObject.SetActive(false);
            checkingPinParent.GetChild(1).gameObject.SetActive(true);
            checkingPinParent.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(5);
        }

        inputParent.gameObject.SetActive(true);
        checkingPinParent.gameObject.SetActive(false);

        isSubmitting = false;

    }

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        // all Certificates are accepted
        return true;
    }
}
