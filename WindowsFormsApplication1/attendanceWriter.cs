using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class attendanceWriter
    {
        FileStream attendance = new FileStream("attendance.txt", FileMode.Open, FileAccess.ReadWrite);


        public static void markAttendedUnique(String Unique)
        {
            
        }

        public static void markAttendedEID(String eid)
        {

        }

        public static void addNewEvent(String eventName, String eventDate)
        {

        }

        public static void addNewMember(String uniqueID, String eid, String first, String last, String email, String phone)
        {

        }

        public static int getRowOfUserEID(String eid)
        {
            return -1;
        }
    }
}
