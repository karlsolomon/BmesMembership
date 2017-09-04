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
        String eid;
        String uniqueID;
        public UTIDLogin(String eventName, String date)
        {
            InitializeComponent();
            this.eventName = eventName;
            this.date = date;
        }

        private void utID_Send(object sender, EventArgs e)
        {
            if(this.utIDTextBox.Text.Contains("11111200000000000000?"))     // ? is the last character
            {
                uniqueID = this.utIDTextBox.Text;
                
                int startIndexUniqueID = uniqueID.IndexOf(';') + 1;
                int endIndexUniqueID = uniqueID.IndexOf('=');
                uniqueID = uniqueID.Substring(startIndexUniqueID, endIndexUniqueID - startIndexUniqueID);
                Console.WriteLine(uniqueID);

                if(userExists(uniqueID))
                {
                    //TODO: Write to Google Docs Sheet
                    markMemberAttended(uniqueID);
                }
                else
                {
                    //TODO: OPEN NEW FORM & GET EID, FIRST, LAST
                    UserNotFound noID;
                    String entry = this.utIDTextBox.Text;
                    if (entry.Contains("%A"))
                    {
                        int startIndexEID = "%A".Length;
                        int endIndexEID = entry.IndexOf(' ');
                        this.eid = entry.Substring(startIndexEID, endIndexEID - startIndexEID);  // got EID
                        Console.WriteLine(eid);
                        noID = new UserNotFound(uniqueID, eid.ToUpper());
                    }
                    else
                    {
                        noID = new UserNotFound(uniqueID);
                    }
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
