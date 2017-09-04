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
    public partial class EID : Form
    {
        String uniqueID;
        public EID(String uniqueID)
        {
            InitializeComponent();
            this.uniqueID = uniqueID;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if(!userExists(this.eid.Text))
            {
                UserNotFound newMember = new UserNotFound(uniqueID, this.eid.Text);
                newMember.Show();
            }
            attendanceWriter.markAttendedEID(this.eid.Text);                
        }
        private bool userExists(String EID)
        {
            return !attendanceWriter.getRowOfUserEID(EID).Equals(-1);
        }
    }
}
