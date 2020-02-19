using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Google_Sheets_Integration
{
    public class SpreadsheetTools
    {
        public string Id { get => Spreadsheet.SpreadsheetId;}
        public Spreadsheet Spreadsheet { get; }
        public IList<Sheet> Sheets { get => Spreadsheet.Sheets; }

        public SheetsService Api { get; }

        public SpreadsheetTools(Spreadsheet spreadsheet)
        {
            Spreadsheet = spreadsheet;
        }
        public SpreadsheetTools(Spreadsheet spreadsheet, SheetsService api)
        {
            Spreadsheet = spreadsheet;
            Api = api;
        }

        public static implicit operator SpreadsheetTools(Spreadsheet s) => new SpreadsheetTools(s);
    }
}