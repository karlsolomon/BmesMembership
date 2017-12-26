using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UserNotFound : Form
    {
        String utID;
        Boolean startup = true;

        public UserNotFound(String utID)
        {
            InitializeComponent();
            this.utID = utID;
        }
        
        public UserNotFound(String utID, String eid)
        {
            InitializeComponent();
            this.utID = utID;
            this.eid.Text = eid;
        }

        public UserNotFound()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            /*
            if(startup)
            {
                startup = false;
                Console.WriteLine(startup);
                return;
            }
            */
            Console.WriteLine(validEntry());
            Console.WriteLine(attendanceWriter.getRowOfUserEID(eid.Text).Equals("-1"));

            if (validEntry())
            {
                Console.Write(first.Text + " " + last.Text + " " + eid.Text + " " + email1.Text + " " + phone1.Text);
                if (attendanceWriter.getRowOfUserEID(eid.Text).Equals(-1))
                {
                    attendanceWriter.addNewMember(utID, eid.Text, first.Text, last.Text, email1.Text, phone1.Text);
                    attendanceWriter.markAttendedEID(eid.Text);
                }
                else
                {
                    attendanceWriter.markAttendedEID(eid.Text);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Must put values for: EID, First & Last", "Invalid Entry", MessageBoxButtons.OKCancel);
            }
        }

        private bool validEntry()
        {
            var emailFormat = new Regex(@"[a-zA-Z_0-9@.]*");
            var phoneToNumber = new Regex(@"[^\d]+");
            var phoneFormat = new Regex(@"[0-9]");
            var alphanumeric = new Regex(@"[a-zA-Z0-9]*");
            bool valid = true;
            phone1.Text = phoneToNumber.Replace(phone1.Text, "");
            if (!emailFormat.IsMatch(email1.Text) || !email1.Text.Contains("@"))
            {
                MessageBox.Show("Must enter valid email", "Invalid Entry", MessageBoxButtons.OKCancel);
                email1.Text = "";
                valid = false;
            }
            else if (!phoneFormat.IsMatch(phone1.Text))
            {
                MessageBox.Show("Must enter valid phone number. Enter as numeric only", "Invalid Entry", MessageBoxButtons.OKCancel);
                phone1.Text = "";
                valid = false;
            }
            else if (!alphanumeric.IsMatch(eid.Text))
            {
                MessageBox.Show("EID must be alphanumeric.", "Invalid Entry", MessageBoxButtons.OKCancel);
                eid.Text = "";
                valid = false;
            }
            return valid;
        }
    }
}
