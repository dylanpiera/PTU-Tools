using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Google_Sheets_Integration
{
    public static class GoogleSheetServiceFactory
    {
        public static void AddGoogleSheetsAPI(this IServiceCollection service) => service.AddSingleton(GetSheetsService());

        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string GoogleCredentialsFileName = "google-credentials.json";
        private static SheetsService GetSheetsService()
        {
            using var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read);
            var serviceInitializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
            };
            return new SheetsService(serviceInitializer);
        }
    }
}