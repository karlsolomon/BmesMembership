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
                        Console.WriteLine(eid);
                        if(!attendanceWriter.getRowOfUserEID(eid).Equals(-1))
                        {
                            attendanceWriter.markAttendedEID(eid);
                        }
                        else
                        {
                            UserNotFound newMember = new UserNotFound(uniqueID, eid);
                            newMember.Show();
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
        }

        private bool userExists(String utIDNumber)
        {
            //Query Google Doc, if the user exists then true, else false
            return false;
        }
    }
}
