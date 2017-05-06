using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                //TODO: SEND TO SPREADSHEET
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
