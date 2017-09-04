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

        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "BmesMembership";

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
                // Google Sheets
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

                // TODO: save student data to sheet

                Data.Spreadsheet requestBody = new Data.Spreadsheet();
                SpreadsheetsResource.CreateRequest request = service.Spreadsheets.Create(requestBody);
                Data.Spreadsheet response = request.Execute();
                var spreadsheetID = response.SpreadsheetId;

                Console.WriteLine(response);
                Console.WriteLine(spreadsheetID);

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
