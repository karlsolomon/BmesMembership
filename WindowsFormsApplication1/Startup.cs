using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Data = Google.Apis.Sheets.v4.Data;

namespace WindowsFormsApplication1
{
    public partial class Startup : Form
    {

        public Startup()
        {
            InitializeComponent();
        }
        private void goToSignIn(object s, EventArgs e)
        {
            String name = eventName.Text.Trim();
            String date = DateTime.UtcNow.Date.ToString("dd/MM/yyy");
            if(name != "")
            {
                attendanceWriter.addNewEvent(name, date);
                this.Visible = false;
                UTIDLogin signInForm = new UTIDLogin(name, date);
                signInForm.Show();
            } else
            {
                MessageBox.Show("Must put non-nil Name", "Invalid Entry", MessageBoxButtons.OKCancel);

            }
        }
    }
}
