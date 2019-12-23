using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;

namespace Google_Sheets_Integration_Test
{
    public class UnitTestSheetConnection
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Quick Sheet test";

        [Fact]
        public void TestCedentials()
        {
            Assert.NotNull(Credentials());
        }

        [Fact]
        public void CreateAPITest()
        {
            Assert.NotNull(CreateSheetsAPIService());
        }

        UserCredential Credentials()
        {
            UserCredential credential;

            using (var stream =
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

            return credential;
        }

        SheetsService CreateSheetsAPIService()
        {
            UserCredential credential = Credentials();

            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        bool PrintSkills()
        {
            UserCredential credential = Credentials();

            // Create Google Sheets API service.
            var service = CreateSheetsAPIService();

            // Define request parameters.
            String spreadsheetId = "1ZdXLD-jF0HIQvqkptuU1YhC3jnb3APrdpP8NDCFxH64";
            String range = "Trainer!A12:F29";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the skills and their rolls in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1ZdXLD-jF0HIQvqkptuU1YhC3jnb3APrdpP8NDCFxH64/edit
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

            return true;
        }
    }
}
