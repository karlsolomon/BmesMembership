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
    public partial class UserNotFound : Form
    {
        String utID;
        Boolean startup = true;

        public UserNotFound(String utID)
        {
            InitializeComponent();
            this.utID = utID;
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
            if(first != "" && last != "" && eid != "")
            {
                //TODO: POPULATE TABLE
                Console.Write(first + " " + last + " " + eid);
                this.Close();
            } else
            {
                MessageBox.Show("Must put values for: EID, First & Last", "Invalid Entry", MessageBoxButtons.OKCancel);
            }
        }
    }
}
