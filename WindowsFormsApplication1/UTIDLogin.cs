using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Threading;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;

namespace WindowsFormsApplication1
{
    public partial class UTIDLogin : Form
    {
        String eventName;
        String date;
        public UTIDLogin(String eventName, String date)
        {
            InitializeComponent();
            this.eventName = eventName;
            this.date = date;
        }

        private void utID_Send(object sender, EventArgs e)
        {
            if(this.utIDTextBox.Text.Contains("?"))     // ? is the last character
            {
                String utIDNumber = this.utIDTextBox.Text.Substring(1);
                utIDNumber = utIDNumber.Substring(0,this.utIDTextBox.Text.IndexOf("=") - 1);
                Console.Write(utIDNumber);
                if(userExists(utIDNumber))
                {
                    //TODO: Write to Google Docs Sheet
                    markMemberAttended(utIDNumber);
                }
                else
                {
                    //TODO: OPEN NEW FORM & GET EID, FIRST, LAST
                    UserNotFound noID = new UserNotFound(utIDNumber);
                    noID.Show();
                }
                this.utIDTextBox.Clear();
            }
        }

        private void noID_Click(object sender, EventArgs e)
        {
            UserNotFound noID = new UserNotFound();
            noID.Show();
        }

        private bool userExists(String utIDNumber)
        {
            //Query Google Doc, if the user exists then true, else false
            return false;
        }


        private async void markMemberAttended(String utIDNumber)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read), new[] { DriveService.Scope.Drive }, "VPRelations", CancellationToken.None);
            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "membership",
            };

            var service = new DriveService(initializer);
            var list = await service.Files.List().ExecuteAsync();
            foreach (var file in list.Items)
            {
                // You can get data from the file (using file.Title for example)
                // and append it to a TextBox, List, etc.
            }
        }
    }
}
