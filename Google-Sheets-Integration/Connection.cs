using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Google_Sheets_Integration
{
    public class Connection
    {
        string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        string ApplicationName = "Quick Sheet test";

        IList<IList<Object>> GetRange(string spreadsheetId, string range)
        {
            //String spreadsheetId = "1ZdXLD-jF0HIQvqkptuU1YhC3jnb3APrdpP8NDCFxH64";
            //String range = "Trainer!A12:F29";
            SheetsService service = GetService();

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Print columns A and F, which correspond to indices 0 and 5.
                    Console.WriteLine("{0}, {1}", row[0], row[5]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            return values;
        }

        SheetsService GetService()
        {
            UserCredential credential;

            using (FileStream stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                                Scopes,
                                "user",
                                CancellationToken.None,
                                new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
    }
}