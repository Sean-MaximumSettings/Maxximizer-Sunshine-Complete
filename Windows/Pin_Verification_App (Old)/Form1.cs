using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;

namespace Mxs_Pin_Verifcation
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                //  MessageBox.Show("Another instance of this program is already running. Cannot proceed further.", "Warning!");
                return;
            }

            InitializeComponent();
            OnLoad();
        }

        ChromiumWebBrowser webBrowser;
        void OnLoad()
        {

            textBox1.KeyUp += TextBoxKeyUp;

            
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("ignore-certificate-errors", string.Empty);
            Cef.Initialize(settings);
            webBrowser = new ChromiumWebBrowser("");
            panel2.Controls.Add(webBrowser);

        }

        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do something
                button1_Click(null, null);
                e.Handled = true;
            }
        }

        bool hasPressed;
        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser.LoadUrl("https://localhost:47990/autopin?pin=" + textBox1.Text);
            // webBrowser1.Navigate("https://localhost:47990/autopin?pin="+textBox1.Text);
            hasPressed = true;
            panel1.Visible = false;
        }

        int total = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hasPressed)
            {
                string totalDots = ".";
                if (total == 1) totalDots = "..";
                if (total == 2) totalDots = "...";
                label3.Text = "Checking pin"+totalDots;
                total += 1;
                if (total > 2) total = 0;

                if (webBrowser != null && !webBrowser.IsLoading)
                {
                    hasPressed = false;
                    panel1.Visible = true;

                    string EvaluateJavaScriptResult = "";
                    var frame = webBrowser.GetMainFrame();
                    var task = frame.EvaluateScriptAsync("(function() { return document.getElementById('result').innerHTML; })();", null);

                    task.ContinueWith(t =>
                    {
                        if (!t.IsFaulted)
                        {
                            var response = t.Result;
                            EvaluateJavaScriptResult = response.Success ? (response.Result.ToString() ?? "null") : response.Message;
                            if (EvaluateJavaScriptResult.Contains("SUCCESS"))
                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            //  MessageBox.Show("Pin has been accepted, you can now connect through Moonlight!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else MessageBox.Show("An error occured when attempting to accept your pin, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());

                     

               }
            }
        }

    }
}
