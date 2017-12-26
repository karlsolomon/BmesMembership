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
    class attendanceWriter
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "BmesMembership";
        static string spreadsheetID = "1Zhj5GS0QPuYCeOLYWV8SgKJOc9D3b1Jyz6ZZqNKEQoQ";
        static SheetsService service = getService();

        public static SheetsService getService()
        {
            UserCredential credential;

            using (var stream = new FileStream("client_secret_new.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }


        public static int getNumColumns()
        {

            var service = getService();
            string range = "Sheet1";

            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetID, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            var columns = values[0].Count;
            return columns;
        }
        
        public static void markAttendedUnique(String unique)
        {
            var rowOfUnique = getRowOfUserUnique(unique).ToString();
            var service = getService();
            var eventHex = getNumColumns() + 64;
            char character = (char)eventHex;
            string eventCol = character.ToString();
            string range = eventCol + rowOfUnique;

            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";

            var attendMark = new List<object>() { "1" };
            valueRange.Values = new List<IList<object>> { attendMark };

            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, spreadsheetID, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse updateResponse = update.Execute();
        }

        public static void markAttendedEID(String eid)
        {
            var rowOfEID = getRowOfUserEID(eid).ToString();
            var service = getService();
            var eventHex = getNumColumns() + 64;
            char character = (char)eventHex;
            string eventCol = character.ToString();
            string range = eventCol + rowOfEID;

            Console.WriteLine(eventCol);
            Console.WriteLine(rowOfEID);

            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";

            var attendMark = new List<object>() { "1" };
            valueRange.Values = new List<IList<object>> { attendMark };

            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, spreadsheetID, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse updateResponse = update.Execute();
        }

        public static void addNewEvent(String eventName, String eventDate)
        {
            var columns = getNumColumns();

            int hex = columns + 65;
            char character = (char)hex;
            string column = character.ToString();
            string range = column + "1";

            var service = getService();

            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";

            var colName = new List<object>() { eventName + " " + eventDate };
            valueRange.Values = new List<IList<object>> { colName };

            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, spreadsheetID, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse updateResponse = update.Execute();
        }

        public static void addNewMember(String uniqueID, String eid, String first, String last, String email, String phone)
        {
            string range = "Sheet1";
            var service = getService();

            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetID, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            var rows = values.Count+1;

            Console.WriteLine(Convert.ToString(rows));

            string updateRange = "Sheet1!A" + Convert.ToString(rows) + ":F" + Convert.ToString(rows);
            Console.WriteLine("range:" + updateRange);
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";

            var studentInfo = new List<object>() { uniqueID, eid, last, first, email, phone };
            valueRange.Values = new List<IList<object>> { studentInfo };

            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, spreadsheetID, updateRange);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse updateResponse = update.Execute();

        }

        public static int getRowOfUserEID(String eid)
        {
            var service = getService();
            string range = "Sheet1!B2:B";

            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetID, range);
            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;
            var rows = values.Count;
            var indexNoHeader = -1;
 
            for (int i = 0; i < values.Count; i++) {
                foreach (var item in values[i])
                {

                    if (item.ToString() == eid)
                    {
                        indexNoHeader = i;
                        break;
                    }
                }
                    
            }
            var sheetRowNum = indexNoHeader + 2;
            if (indexNoHeader == -1) {
                return -1;
            }else{
                return sheetRowNum;
            } 
        }

        public static int getRowOfUserUnique(String unique)
        {
            var service = getService();
            string range = "Sheet1!A2:A";

            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetID, range);
            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;
            var rows = values.Count;
            var indexNoHeader = -1;

            for(var i = 0; i < values.Count; i++)
            {
                foreach (var item in values[i])
                {
                    if (item.ToString() == unique)
                    {
                        indexNoHeader = i;
                        break;
                    }
                }
                
            }
            var sheetRowNum = indexNoHeader + 2;
            if (indexNoHeader == -1)
            {
                return -1;
            }
            else
            {
                return sheetRowNum;
            }
        }

        public static void addUnique(String unique, String eid)
        {
            var service = getService();

            var row = getRowOfUserEID(eid).ToString();
            string range = "A" + row;
            //Console.WriteLine(range);

            

            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";

            var ammendUnique = new List<object>() { unique };
            valueRange.Values = new List<IList<object>> { ammendUnique };

            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, spreadsheetID, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse updateRespones = update.Execute();
        }
    }
}
