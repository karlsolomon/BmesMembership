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
using System.Text.RegularExpressions;

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

                Console.WriteLine(userExists(uniqueID));

                if (userExists(uniqueID))
                {
                    attendanceWriter.markAttendedUnique(uniqueID);
                }
                else
                {
                    String entry = this.utIDTextBox.Text;
                    
                    if (entry.Contains("%A"))
                    {
                        int startIndexEID = "%A".Length;
                        int endIndexEID = entry.IndexOf(' ');
                        this.eid = entry.Substring(startIndexEID, endIndexEID - startIndexEID);  // got EID
                        eid = lowercase(eid);
                        Console.WriteLine(eid);
                        if (attendanceWriter.getRowOfUserEID(eid).Equals(-1))
                        {
                            UserNotFound newMember = new UserNotFound(uniqueID, eid);
                            newMember.Show();
                            
                        }
                        else
                        {
                            attendanceWriter.addUnique(uniqueID, eid);
                            attendanceWriter.markAttendedEID(eid);

                            //check for empty data cells and present new window to fix
                        }
                    }
                    else
                    {
                        EID newMember = new EID(uniqueID);
                        newMember.Show();
                    }
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
            Console.WriteLine(utIDNumber);
            if (attendanceWriter.getRowOfUserUnique(utIDNumber).Equals(-1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private String lowercase(String eid)
        {
            String letters = "";
            String nums = "";

            foreach (var letter in eid)
            {
                if (letter >= 'A' && letter <= 'Z')
                {
                    letters = letters + letter;
                } else
                {
                    nums = nums + letter;
                }
            }

            letters = letters.ToLower();
            String newEID = letters + nums;

            return newEID;
        }


        /*
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
        */
    }
}
