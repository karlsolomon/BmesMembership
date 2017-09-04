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

            String first = this.first.Text;
            String last = this.last.Text;
            String eid = this.eid.Text;
            String email = this.email.Text;
            String phone = this.phone.Text;
            if(validEntry(first, last, eid, email, phone))
            {
                //TODO: POPULATE TABLE
                Console.Write(first + " " + last + " " + eid);
                this.Close();
            }
        }

        private bool validEntry(String first, String last, String eid, String email, String phone)
        {
            var emailFormat = new Regex(@"[a-zA-Z_0-9@.]*");
            var phoneToNumber = new Regex(@"[^\d]+");
            var phoneFormat = new Regex(@"[0-9]");
            var alphanumeric = new Regex(@"[a-zA-Z0-9]*");
            bool valid = true;
            phone = phoneToNumber.Replace(phone, ""); 
            if(!emailFormat.IsMatch(email) || !email.Contains("@"))
            {
                MessageBox.Show("Must enter valid email", "Invalid Entry", MessageBoxButtons.OKCancel);
                this.email.Text = "";
                valid = false;
            }
            else if(!phoneFormat.IsMatch(phone))
            {
                MessageBox.Show("Must enter valid phone number. Enter as numeric only", "Invalid Entry", MessageBoxButtons.OKCancel);
                this.phone.Text = "";
                valid = false;
            }
            else if(!alphanumeric.IsMatch(eid))
            {
                MessageBox.Show("EID must be alphanumeric.", "Invalid Entry", MessageBoxButtons.OKCancel);
                this.eid.Text = "";
                valid = false;
            }
            return valid;
        }
    }
}
