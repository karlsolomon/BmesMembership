using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            if(startup)
            {
                startup = false;
                return;
            }
            if(validEntry())
            {
                //TODO: POPULATE TABLE
                Console.Write(first.Text + " " + last.Text + " " + eid.Text + " " + email.Text + " " + phone.Text);
                attendanceWriter.addNewMember(utID, eid.Text, first.Text, last.Text, email.Text, phone.Text);
                this.Close();
            }
        }

        private bool validEntry()
        {            
            var emailFormat = new Regex(@"[a-zA-Z_0-9@.]*");
            var phoneToNumber = new Regex(@"[^\d]+");
            var phoneFormat = new Regex(@"[0-9]");
            var alphanumeric = new Regex(@"[a-zA-Z0-9]*");
            bool valid = true;
            phone.Text = phoneToNumber.Replace(phone.Text, ""); 
            if(!emailFormat.IsMatch(email.Text) || !email.Text.Contains("@"))
            {
                MessageBox.Show("Must enter valid email", "Invalid Entry", MessageBoxButtons.OKCancel);
                email.Text = "";
                valid = false;
            }
            else if(!phoneFormat.IsMatch(phone.Text))
            {
                MessageBox.Show("Must enter valid phone number. Enter as numeric only", "Invalid Entry", MessageBoxButtons.OKCancel);
                phone.Text = "";
                valid = false;
            }
            else if(!alphanumeric.IsMatch(eid.Text))
            {
                MessageBox.Show("EID must be alphanumeric.", "Invalid Entry", MessageBoxButtons.OKCancel);
                eid.Text = "";
                valid = false;
            }
            return valid;
        }
    }
}
