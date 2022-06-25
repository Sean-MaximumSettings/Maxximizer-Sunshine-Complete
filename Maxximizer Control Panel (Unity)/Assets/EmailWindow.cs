using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Net.Mail;

public class EmailWindow : MonoBehaviour
{

    public InputField reportField;
    public TMP_Dropdown reportType;
    public TMP_InputField subjectTitle;
    public Transform successObj;
    public Button submitButton;
    public Button sendingButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    float timer;
    bool isSending;
    private void Update()
    {
        if (isSending)
        {
            timer += 1 * Time.deltaTime;
            if (timer > 10)
            {
                timer = 0;
                isSending = false;
            }
        }
    }

    public void SendEmail()
    {
        if (isSending) return;
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.maxximizer.ca", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "bugs@maxximizer.ca",
            "zTM9MT0z8r");
        client.EnableSsl = false;
        client.Host = "maxximizer.ca";

        

        // in the display name.
        MailAddress from = new MailAddress(
            "bugs@maxximizer.ca",
            "MAXXIMIZER",
            System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("bugs@maxximizer.ca");

        string completeMessage = reportField.text;
        completeMessage = completeMessage + "\n\n\nSystem Information: \n" + SystemInfo.graphicsDeviceName + "\n"+SystemInfo.processorType+"\nMemory size "+SystemInfo.systemMemorySize+"mb \n"+SystemInfo.operatingSystem;
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = completeMessage;
        message.BodyEncoding = System.Text.Encoding.UTF8;

        string reportTypeText = "[BUG]";
        if (reportType.value == 1) reportTypeText = "[FEEDBACK]";

        message.Subject = reportTypeText + " " + subjectTitle.text;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "message";
        client.SendAsync(message, userState);

        submitButton.gameObject.SetActive(false);
        sendingButton.gameObject.SetActive(true);
        isSending = true;
    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {

        submitButton.gameObject.SetActive(true);
        sendingButton.gameObject.SetActive(false);

        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled " + token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
        }
        else
        {
            GameObject newObj = Instantiate(successObj, successObj.parent).gameObject;
            newObj.SetActive(true);
            GameObject.Destroy(newObj, 5);
            Debug.Log("Message sent.");
        }
    }
}
